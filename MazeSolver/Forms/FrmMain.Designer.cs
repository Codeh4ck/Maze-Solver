namespace MazeSolver.Forms
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.pbMaze = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmdManualEdit = new System.Windows.Forms.CheckBox();
            this.cmdSolveMaze = new System.Windows.Forms.Button();
            this.cmdClear = new System.Windows.Forms.Button();
            this.cmdLoadMaze = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtSolutionLogs = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbMaze)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbMaze
            // 
            this.pbMaze.Location = new System.Drawing.Point(15, 13);
            this.pbMaze.Name = "pbMaze";
            this.pbMaze.Size = new System.Drawing.Size(431, 317);
            this.pbMaze.TabIndex = 0;
            this.pbMaze.TabStop = false;
            this.pbMaze.Paint += new System.Windows.Forms.PaintEventHandler(this.pbMaze_Paint);
            this.pbMaze.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbMaze_MouseDown);
            this.pbMaze.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbMaze_MouseMove);
            this.pbMaze.Resize += new System.EventHandler(this.pbMaze_Resize);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cmdManualEdit);
            this.groupBox1.Controls.Add(this.cmdSolveMaze);
            this.groupBox1.Controls.Add(this.cmdClear);
            this.groupBox1.Controls.Add(this.cmdLoadMaze);
            this.groupBox1.Location = new System.Drawing.Point(452, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(269, 134);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Solver controls";
            // 
            // cmdManualEdit
            // 
            this.cmdManualEdit.Appearance = System.Windows.Forms.Appearance.Button;
            this.cmdManualEdit.Location = new System.Drawing.Point(15, 45);
            this.cmdManualEdit.Name = "cmdManualEdit";
            this.cmdManualEdit.Size = new System.Drawing.Size(248, 23);
            this.cmdManualEdit.TabIndex = 4;
            this.cmdManualEdit.Text = "Manually edit maze";
            this.cmdManualEdit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cmdManualEdit.UseVisualStyleBackColor = true;
            this.cmdManualEdit.CheckedChanged += new System.EventHandler(this.cmdManualEdit_CheckedChanged);
            // 
            // cmdSolveMaze
            // 
            this.cmdSolveMaze.Location = new System.Drawing.Point(15, 99);
            this.cmdSolveMaze.Name = "cmdSolveMaze";
            this.cmdSolveMaze.Size = new System.Drawing.Size(248, 23);
            this.cmdSolveMaze.TabIndex = 3;
            this.cmdSolveMaze.Text = "Solve maze";
            this.cmdSolveMaze.UseVisualStyleBackColor = true;
            this.cmdSolveMaze.Click += new System.EventHandler(this.cmdSolveMaze_Click);
            // 
            // cmdClear
            // 
            this.cmdClear.Location = new System.Drawing.Point(15, 72);
            this.cmdClear.Name = "cmdClear";
            this.cmdClear.Size = new System.Drawing.Size(248, 23);
            this.cmdClear.TabIndex = 2;
            this.cmdClear.Text = "Clear maze";
            this.cmdClear.UseVisualStyleBackColor = true;
            this.cmdClear.Click += new System.EventHandler(this.cmdClear_Click);
            // 
            // cmdLoadMaze
            // 
            this.cmdLoadMaze.Location = new System.Drawing.Point(15, 19);
            this.cmdLoadMaze.Name = "cmdLoadMaze";
            this.cmdLoadMaze.Size = new System.Drawing.Size(248, 23);
            this.cmdLoadMaze.TabIndex = 0;
            this.cmdLoadMaze.Text = "Load maze from file";
            this.cmdLoadMaze.UseVisualStyleBackColor = true;
            this.cmdLoadMaze.Click += new System.EventHandler(this.cmdLoadMaze_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.txtSolutionLogs);
            this.groupBox2.Location = new System.Drawing.Point(452, 167);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(269, 167);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Solution logs";
            // 
            // txtSolutionLogs
            // 
            this.txtSolutionLogs.BackColor = System.Drawing.Color.White;
            this.txtSolutionLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSolutionLogs.Location = new System.Drawing.Point(3, 17);
            this.txtSolutionLogs.Name = "txtSolutionLogs";
            this.txtSolutionLogs.ReadOnly = true;
            this.txtSolutionLogs.Size = new System.Drawing.Size(263, 147);
            this.txtSolutionLogs.TabIndex = 0;
            this.txtSolutionLogs.Text = "";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label1.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label1.Location = new System.Drawing.Point(452, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(257, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "The (?) button actually works and has information!";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 346);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pbMaze);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Maze Solver";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.FrmMain_HelpButtonClicked);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbMaze)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbMaze;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button cmdClear;
        private System.Windows.Forms.Button cmdLoadMaze;
        private System.Windows.Forms.CheckBox cmdManualEdit;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox txtSolutionLogs;
        private System.Windows.Forms.Button cmdSolveMaze;
        private System.Windows.Forms.Label label1;
    }
}

