namespace slauncher {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.msgLabel = new System.Windows.Forms.ToolStripLabel();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.noteBtn = new System.Windows.Forms.ToolStripButton();
            this.configBtn = new System.Windows.Forms.ToolStripButton();
            this.buttonListPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.emptyMessage = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker1_RunWorkerCompleted);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msgLabel,
            this.progressBar,
            this.noteBtn,
            this.configBtn});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(3, 328);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(270, 32);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // msgLabel
            // 
            this.msgLabel.Margin = new System.Windows.Forms.Padding(0);
            this.msgLabel.Name = "msgLabel";
            this.msgLabel.Size = new System.Drawing.Size(67, 32);
            this.msgLabel.Text = "message";
            // 
            // progressBar
            // 
            this.progressBar.Margin = new System.Windows.Forms.Padding(1);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(67, 30);
            // 
            // noteBtn
            // 
            this.noteBtn.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.noteBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.noteBtn.Image = global::slauncher.Properties.Resources.FormatDocument_16x;
            this.noteBtn.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.noteBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.noteBtn.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.noteBtn.Name = "noteBtn";
            this.noteBtn.Size = new System.Drawing.Size(29, 31);
            this.noteBtn.Text = "Note";
            this.noteBtn.ToolTipText = "Open global note";
            this.noteBtn.Click += new System.EventHandler(this.NoteBtn_Click);
            // 
            // configBtn
            // 
            this.configBtn.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.configBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.configBtn.Image = global::slauncher.Properties.Resources.Edit_16x;
            this.configBtn.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.configBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.configBtn.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.configBtn.Name = "configBtn";
            this.configBtn.Size = new System.Drawing.Size(29, 31);
            this.configBtn.Text = "Configure launcher";
            this.configBtn.ToolTipText = "Configure this launcher";
            this.configBtn.Click += new System.EventHandler(this.EditBtn_Click);
            // 
            // buttonListPanel
            // 
            this.buttonListPanel.AutoScroll = true;
            this.buttonListPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonListPanel.Location = new System.Drawing.Point(3, 81);
            this.buttonListPanel.Margin = new System.Windows.Forms.Padding(0);
            this.buttonListPanel.Name = "buttonListPanel";
            this.buttonListPanel.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.buttonListPanel.Size = new System.Drawing.Size(270, 247);
            this.buttonListPanel.TabIndex = 0;
            this.buttonListPanel.WrapContents = false;
            // 
            // emptyMessage
            // 
            this.emptyMessage.AutoEllipsis = true;
            this.emptyMessage.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.emptyMessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.emptyMessage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.emptyMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.emptyMessage.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.emptyMessage.Location = new System.Drawing.Point(3, 2);
            this.emptyMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.emptyMessage.Name = "emptyMessage";
            this.emptyMessage.Size = new System.Drawing.Size(270, 79);
            this.emptyMessage.TabIndex = 0;
            this.emptyMessage.Text = "Drag file/folder and drop here";
            this.emptyMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(276, 362);
            this.Controls.Add(this.buttonListPanel);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.emptyMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Text = "SLauncher";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel msgLabel;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.ToolStripButton configBtn;
        private System.Windows.Forms.FlowLayoutPanel buttonListPanel;
        private System.Windows.Forms.Label emptyMessage;
        private System.Windows.Forms.ToolStripButton noteBtn;
    }
}

