using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
namespace slauncher {

    [FlagsAttribute]
    public enum EXECUTION_STATE : uint {
        ES_AWAYMODE_REQUIRED = 0x00000040,
        ES_CONTINUOUS = 0x80000000,
        ES_DISPLAY_REQUIRED = 0x00000002,
        ES_SYSTEM_REQUIRED = 0x00000001
        // Legacy flag, should not be used.
        // ES_USER_PRESENT = 0x00000004
    }

    public partial class Form1 : Form, IDisposable {

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

        private void PreventSleep(bool preventSleep = true) {
            if (preventSleep) {
                SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS | EXECUTION_STATE.ES_AWAYMODE_REQUIRED);
            } else {
                SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);
            }
        }

        public static string FirstCharToUpper(string s) {
            if (string.IsNullOrEmpty(s) || s.Length < 1) return s;
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        private void FixToolstrip(Message m) {
            const int MOUSEEVENTF_LEFTDOWN = 0x02;
            const int MOUSEEVENTF_LEFTUP = 0x04;
            const int WM_PARENTNOTIFY = 0x210;
            const int WM_LBUTTONDOWN = 0x201;
            if (m.Msg == WM_PARENTNOTIFY) {
                if (m.WParam.ToInt32() == WM_LBUTTONDOWN && ActiveForm != this) {
                    Point p = PointToClient(Cursor.Position);
                    if (GetChildAtPoint(p) is ToolStrip)
                        mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, (uint)p.X, (uint)p.Y, 0, 0);
                }
            }
        }

        protected override void WndProc(ref Message m) {
            //to fix toolstrip issue
            FixToolstrip(m);

            if (m.Msg == 100) {
                ShowMe();
            }
            if (m.Msg == MessageHelper.WM_COPYDATA) {
                LoadCmd(MessageHelper.ReadStr(m));
                ShowMe();
            }
            base.WndProc(ref m);
        }

        private void ShowMe() {
            if (WindowState == FormWindowState.Minimized) {
                WindowState = FormWindowState.Normal;
            }
            bool top = TopMost;
            TopMost = true;
            TopMost = top;
        }

        private readonly string GlobaNotePath;
        private readonly Dictionary<string, List<string>> Commands = new Dictionary<string, List<string>>();
        private readonly Dictionary<string, List<string>> Configs = new Dictionary<string, List<string>>();
        public string FilePath = "";

        public Form1() {
            InitializeComponent();

            buttonListPanel.AutoScroll = true;
            buttonListPanel.FlowDirection = FlowDirection.TopDown;
            buttonListPanel.WrapContents = false;

            progressBar.Visible = false;
            msgLabel.Visible = false;

            toolStrip1.CreateControl();
            toolStrip1.Renderer = new MyRenderer();

            var loadCmd = new Debouncer(TimeSpan.FromSeconds(.2), LoadCmd);
            Watcher.Changed += (object source, FileSystemEventArgs evt) => loadCmd.Invoke();

            GlobaNotePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                , "slauncherNote.txt");

            if (!File.Exists(GlobaNotePath)) {
                string[] lines = { "A global note.",
                    "For keeping information you use often.",
                    "-config:console,async"
                };
                File.WriteAllLines(GlobaNotePath, lines);
            }
        }

        private class MyRenderer : ToolStripProfessionalRenderer {
            public MyRenderer() {
                RoundedEdges = false;
            }
            protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e) {
                // Do nothing
            }
        }

        public new void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void LoadCmd(string path) {
            FilePath = path;
            LoadCmd();
        }

        private bool IsUNC(string path) {
            return Uri.TryCreate(path, UriKind.Absolute, out Uri uri) && uri.IsUnc;
        }

        private Bitmap GetIcon(string cmd) {
            cmd = cmd.ToLower();
            Bitmap icon = Properties.Resources.file;
            if (cmd.StartsWith("http")) {
                icon = Properties.Resources.web;
            } else if (cmd.StartsWith("code")) {
                icon = Properties.Resources.vscode;
            } else if (cmd.StartsWith("notepad")) {
                icon = Properties.Resources.text;
            } else if (cmd.Contains(".bat")) {
                icon = Properties.Resources.bat;
            } else if (IsUNC(cmd)) {
                icon = Properties.Resources.sharedFolder;
            } else if (Directory.Exists(cmd)) {
                icon = Properties.Resources.folder;
            } else {
                try {
                    var testPath = cmd.Trim();
                    if (testPath.StartsWith("\"")) {
                        var result = from Match match in Regex.Matches(testPath, "\"([^\"]*)\"")
                                     select match.ToString();
                        testPath = result.First().Replace("\"", "");
                    }
                    var iconForFile = Icon.ExtractAssociatedIcon(testPath);
                    if (iconForFile != null) icon = new Bitmap(iconForFile.ToBitmap(), new Size(24, 24));
                } catch (Exception) { }
            }
            return icon;
        }

        private void LoadCmd() {
            if (string.IsNullOrEmpty(FilePath)) FilePath = Properties.Settings.Default.LastFile;

            if (!File.Exists(FilePath)) {
                var dlg = new SaveFileDialog {
                    Filter = "Launcher Configure|*.sl",
                    Title = "Create a new launcher",
                    OverwritePrompt = false
                };

                if (Directory.Exists(FilePath)) {
                    dlg.FileName = "NewLauncher.sl";
                    dlg.InitialDirectory = FilePath;
                }

                if (dlg.ShowDialog() == DialogResult.OK) {
                    FilePath = dlg.FileName;
                    if (!File.Exists(FilePath)) File.WriteAllLines(FilePath, new string[0]);
                } else {
                    Close();
                    return;
                }
            }

            Properties.Settings.Default.LastFile = FilePath;

            string[] lines = null;
            try {
                lines = File.ReadAllLines(FilePath);
            } catch (Exception) {

            }
            if (lines == null) return;

            Text = Path.GetFileNameWithoutExtension(FilePath);

            var btnCmd = "";
            var btns = new List<Button>();

            foreach (var _line in lines) {
                var line = _line.Trim();
                if (line.StartsWith("#")) {
                    var parts = line.Substring(1).Trim().Split(new string[] { "-config:" }, StringSplitOptions.None);
                    btnCmd = FirstCharToUpper(parts[0].Trim());

                    Commands[btnCmd] = new List<string>();
                    Configs[btnCmd] = new List<string>();
                    if (parts.Length > 1) {
                        foreach (var p in parts[1].Split(',')) {
                            Configs[btnCmd].Add(p.Trim().ToLower());
                        }
                    }

                    var btn = new NoFocusCueButton() {
                        Text = btnCmd,
                        Margin = new Padding(5, 0, 5, 0),
                        Padding = new Padding(0),
                        Font = new Font("Arial", 14, FontStyle.Regular),
                        AutoSize = true,
                        TextAlign = ContentAlignment.MiddleLeft,
                        ImageAlign = ContentAlignment.MiddleRight,
                        FlatStyle = FlatStyle.Flat,
                        ForeColor = Color.Black
                    };
                    btn.Click += this.BtnClick;
                    btn.FlatAppearance.BorderColor = SystemColors.Control;
                    btn.FlatAppearance.MouseOverBackColor = Color.Silver;
                    btns.Add(btn);
                } else {
                    if (line.Length > 0 && btnCmd.Length > 0) {
                        Commands[btnCmd].Add(line);
                    } else {
                        btnCmd = "";//stop command
                    }
                }
            }

            btns.ForEach(btn => {
                var cmds = Commands[btn.Text];
                Bitmap icon = Properties.Resources.file;
                if (cmds.Count == 1) icon = GetIcon(cmds[0]);
                btn.Image = icon;
            });

            BeginInvoke((Action)(() => {
                emptyMessage.Visible = btns.Count == 0;
                buttonListPanel.Visible = false;
                buttonListPanel.Controls.Clear();
                if (btns.Any()) {
                    buttonListPanel.Controls.AddRange(btns.ToArray());
                    var width = btns.Max(btn => btn.Width) + 20;
                    btns.ForEach(btn => btn.Width = width);
                    var height = btns.Last().Bottom + toolStrip1.Height + 48;
                    height = height / 2 * 2;

                    Rectangle window = Screen.FromControl(this).WorkingArea;
                    var maxHeight = window.Height - 50;
                    Width = Math.Min(width + 30, window.Width / 2);
                    Height = Math.Max(200, Math.Min(height, maxHeight));
                    //when too many items
                    var overflow = height > maxHeight;
                    if (overflow) Width += 20;
                } else {
                    Height = 200;
                    Width = 200;
                }
                Thread.Sleep(1);
                buttonListPanel.Visible = true;
                WatchFile();
                BringToFront();
                TopMost = true;
                Thread.Sleep(1);
                TopMost = false;
                FixLocation();
            }));
        }

        private readonly FileSystemWatcher Watcher = new FileSystemWatcher() {
            NotifyFilter = NotifyFilters.LastWrite
        };

        private void WatchFile() {
            try {
                Watcher.EnableRaisingEvents = false;
                Watcher.Path = Path.GetDirectoryName(FilePath);
                Watcher.Filter = Path.GetFileName(FilePath);
                Watcher.EnableRaisingEvents = true;
            } catch (Exception e) {
                MessageBox.Show("watch error:" + e.ToString());
            }
        }

        private void Form1_Load(object sender, EventArgs e) {
            AllowDrop = true;
            Location = Properties.Settings.Default.Location;
            LoadCmd();
            PreventSleep(true);
        }

        readonly string BatFileName = Path.Combine(Path.GetTempPath(), "_temp.bat");
        private string Enhance(string cmd, List<string> config) {
            var cmd_low = cmd.ToLower();
            if (cmd_low.StartsWith("http") || cmd_low.StartsWith(@"\\")
                || cmd_low.IndexOf(@":\") == 1) {
                cmd = string.Format("start \"\" \"{0}\"", cmd);
            }

            if (cmd_low.StartsWith("notepad ")) {
                cmd = String.Format("\"{0}\" {1}", NotePadExe,
                    cmd_low.Replace("notepad ", ""));
                if (!config.Contains("async")) config.Add("async");
            }

            return cmd;
        }

        string Command = "";

        private void BtnClick(object sender, System.EventArgs e) {
            try {
                if (!backgroundWorker1.IsBusy) {
                    Command = (sender as Button).Text;
                    progressBar.Style = ProgressBarStyle.Marquee;
                    progressBar.Visible = true;
                    msgLabel.Text = Command;
                    msgLabel.Visible = true;

                    backgroundWorker1.RunWorkerAsync();
                    (sender as Button).Select();
                }
            } catch (Exception) {
                LoadCmd();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            if (WindowState == FormWindowState.Maximized) {
                Properties.Settings.Default.Location = RestoreBounds.Location;
                Properties.Settings.Default.Size = RestoreBounds.Size;
                Properties.Settings.Default.Maximised = true;
                Properties.Settings.Default.Minimised = false;
            } else if (WindowState == FormWindowState.Normal) {
                Properties.Settings.Default.Location = Location;
                Properties.Settings.Default.Size = Size;
                Properties.Settings.Default.Maximised = false;
                Properties.Settings.Default.Minimised = false;
            } else {
                Properties.Settings.Default.Location = RestoreBounds.Location;
                Properties.Settings.Default.Size = RestoreBounds.Size;
                Properties.Settings.Default.Maximised = false;
                Properties.Settings.Default.Minimised = true;
            }
            Properties.Settings.Default.Save();

            PreventSleep(false);

            File.Delete(BatFileName);
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e) {
            var config = Configs[Command];
            var cmds = Commands[Command].Select(cmd => Enhance(cmd, config));
            File.WriteAllLines(BatFileName, cmds);
            Process process = new Process();
            var workingDir = Path.GetDirectoryName(FilePath);

            var showConsole = true;
            //hide console smartly
            if (cmds.Count() == 1 && !cmds.First().Contains(".bat")) showConsole = false;
            if (config.Contains("console")) showConsole = true;

            if (showConsole) {
                process.StartInfo = new ProcessStartInfo {
                    WorkingDirectory = workingDir,
                    FileName = BatFileName
                };
            } else //hide shell
              {
                process.StartInfo = new ProcessStartInfo {
                    WorkingDirectory = workingDir,
                    FileName = BatFileName,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true
                };
            }

            process.Start();

            if (config.Contains("async")) //async
            {
                Thread.Sleep(1000);
                process.Close();
                Log = string.Join(",", cmds);
            } else {
                process.WaitForExit();
                try {
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();
                    Log = output + error;
                } catch (Exception) { }
                process.Close();
            }
        }

        string Log = "";

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            progressBar.Visible = false;
            msgLabel.Visible = false;
            //infoBtn.Visible = true;

            foreach (var btn in buttonListPanel.Controls) {
                (btn as Button).Enabled = true;
            }
        }

        private void ToolStripDropDownButton1_Click_1(object sender, EventArgs e) {
            LoadCmd();
        }

        private void Form1_DragEnter(object sender, DragEventArgs e) {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e) {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (var file in files) {
                var name = Path.GetFileNameWithoutExtension(file);
                var ret = InputBox("New shortcut", file, ref name);
                if (ret == DialogResult.OK) {
                    using (StreamWriter w = File.AppendText(FilePath)) {
                        w.WriteLine(" ");
                        w.WriteLine('#' + name);
                        w.WriteLine(file);
                    }
                }
            }
        }


        public static DialogResult InputBox(string title, string promptText, ref string value) {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor |= AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }


        private void FixLocation() {
            var screen = Screen.FromHandle(Handle);
            if ((this.Left + this.Width) > screen.Bounds.Width)
                this.Left = screen.Bounds.Width - this.Width;

            if (this.Left < screen.Bounds.Left)
                this.Left = screen.Bounds.Left;

            if ((this.Top + this.Height) > screen.Bounds.Height)
                this.Top = screen.Bounds.Height - this.Height;

            if (this.Top < screen.Bounds.Top)
                this.Top = screen.Bounds.Top;
        }


        public string NotePadExe {
            get {
                string[] candidates = {
                    @"C:\Program Files (x86)\Notepad++\notepad++.exe",
                    @"C:\Program Files\Notepad++\notepad++.exe"
                };
                var editor = candidates.FirstOrDefault(c => File.Exists(c));
                if (editor == null) editor = "notepad";
                return editor;
            }
        }


        private void EditBtn_Click(object sender, EventArgs e) {
            Process.Start(NotePadExe, '"' + FilePath + '"');
        }

        private void NoteBtn_Click(object sender, EventArgs e) {
            Process.Start(NotePadExe, '"' + GlobaNotePath + '"');
        }

    }

}
