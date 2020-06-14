namespace slauncher
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.msgLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.buttonListPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.emptyMessage = new System.Windows.Forms.Label();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.configBtn = new System.Windows.Forms.ToolStripButton();
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
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msgLabel,
            this.toolStripProgressBar1,
            this.toolStripButton1,
            this.configBtn});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(3, 218);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(230, 31);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            // 
            // msgLabel
            // 
            this.msgLabel.Margin = new System.Windows.Forms.Padding(0);
            this.msgLabel.Name = "msgLabel";
            this.msgLabel.Size = new System.Drawing.Size(67, 31);
            this.msgLabel.Text = "message";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Margin = new System.Windows.Forms.Padding(1);
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(67, 29);
            // 
            // buttonListPanel
            // 
            this.buttonListPanel.AutoScroll = true;
            this.buttonListPanel.BackColor = System.Drawing.SystemColors.Control;
            this.buttonListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonListPanel.Location = new System.Drawing.Point(3, 81);
            this.buttonListPanel.Margin = new System.Windows.Forms.Padding(4);
            this.buttonListPanel.Name = "buttonListPanel";
            this.buttonListPanel.Padding = new System.Windows.Forms.Padding(0, 6, 13, 0);
            this.buttonListPanel.Size = new System.Drawing.Size(230, 137);
            this.buttonListPanel.TabIndex = 0;
            this.buttonListPanel.WrapContents = false;
            // 
            // emptyMessage
            // 
            this.emptyMessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.emptyMessage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.emptyMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.emptyMessage.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.emptyMessage.Location = new System.Drawing.Point(3, 2);
            this.emptyMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.emptyMessage.Name = "emptyMessage";
            this.emptyMessage.Size = new System.Drawing.Size(230, 79);
            this.emptyMessage.TabIndex = 0;
            this.emptyMessage.Text = "Drag file/folder and drop here";
            this.emptyMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::slauncher.Properties.Resources.FormatDocument_16x;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(29, 30);
            this.toolStripButton1.Text = "Note";
            this.toolStripButton1.ToolTipText = "Open global note";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // configBtn
            // 
            this.configBtn.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.configBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.configBtn.Image = global::slauncher.Properties.Resources.Edit_16x;
            this.configBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.configBtn.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.configBtn.Name = "configBtn";
            this.configBtn.Size = new System.Drawing.Size(29, 30);
            this.configBtn.Text = "Configure launcher";
            this.configBtn.ToolTipText = "Configure this launcher";
            this.configBtn.Click += new System.EventHandler(this.EditBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(236, 251);
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
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.LocationChanged += new System.EventHandler(this.Form1_LocationChanged);
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
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripButton configBtn;
        private System.Windows.Forms.FlowLayoutPanel buttonListPanel;
        private System.Windows.Forms.Label emptyMessage;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
    }
}

