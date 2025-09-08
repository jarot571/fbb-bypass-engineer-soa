namespace Window_App
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            runToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripMenuItem();
            deviceToolStripMenuItem = new ToolStripMenuItem();
            viewToolStripMenuItem = new ToolStripMenuItem();
            connectionToolStripMenuItem = new ToolStripMenuItem();
            createToolStripMenuItem = new ToolStripMenuItem();
            GridViewDisplay = new DataGridView();
            label1 = new Label();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)GridViewDisplay).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, viewToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 28);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.BackColor = SystemColors.Control;
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { runToolStripMenuItem, openToolStripMenuItem, toolStripMenuItem1 });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(46, 24);
            fileToolStripMenuItem.Text = "File";
            // 
            // runToolStripMenuItem
            // 
            runToolStripMenuItem.Name = "runToolStripMenuItem";
            runToolStripMenuItem.Size = new Size(141, 26);
            runToolStripMenuItem.Text = "Run";
            runToolStripMenuItem.Click += runToolStripMenuItem_Click;
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(141, 26);
            openToolStripMenuItem.Text = "Open";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { deviceToolStripMenuItem });
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(141, 26);
            toolStripMenuItem1.Text = "Save as";
            // 
            // deviceToolStripMenuItem
            // 
            deviceToolStripMenuItem.Name = "deviceToolStripMenuItem";
            deviceToolStripMenuItem.Size = new Size(137, 26);
            deviceToolStripMenuItem.Text = "Device";
            deviceToolStripMenuItem.Click += deviceToolStripMenuItem_Click;
            // 
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.AccessibleRole = AccessibleRole.Border;
            viewToolStripMenuItem.BackColor = SystemColors.Control;
            viewToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { connectionToolStripMenuItem });
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new Size(70, 24);
            viewToolStripMenuItem.Text = "Setting";
            // 
            // connectionToolStripMenuItem
            // 
            connectionToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { createToolStripMenuItem });
            connectionToolStripMenuItem.Name = "connectionToolStripMenuItem";
            connectionToolStripMenuItem.Size = new Size(136, 26);
            connectionToolStripMenuItem.Text = "Config";
            // 
            // createToolStripMenuItem
            // 
            createToolStripMenuItem.Name = "createToolStripMenuItem";
            createToolStripMenuItem.Size = new Size(135, 26);
            createToolStripMenuItem.Text = "Create";
            createToolStripMenuItem.Click += createToolStripMenuItem_Click;
            // 
            // GridViewDisplay
            // 
            GridViewDisplay.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            GridViewDisplay.Location = new Point(12, 31);
            GridViewDisplay.Name = "GridViewDisplay";
            GridViewDisplay.RowHeadersWidth = 51;
            GridViewDisplay.Size = new Size(776, 379);
            GridViewDisplay.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(734, 421);
            label1.Name = "label1";
            label1.Size = new Size(54, 20);
            label1.TabIndex = 3;
            label1.Text = "v1.0.01";
            // 
            // Main
            // 
            AccessibleRole = AccessibleRole.TitleBar;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(800, 450);
            Controls.Add(label1);
            Controls.Add(GridViewDisplay);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Name = "Main";
            Text = "FBB Tester";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)GridViewDisplay).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem runToolStripMenuItem;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem connectionToolStripMenuItem;
        private DataGridView GridViewDisplay;
        private Label label1;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem createToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem deviceToolStripMenuItem;
    }
}

