namespace LH.Reminder2.Client
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.serverBaseTextBox = new System.Windows.Forms.TextBox();
            this.tasksContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.checkTaskMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uncheckTaskMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteTaskMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getTasksButton = new System.Windows.Forms.Button();
            this.userNameTextBox = new System.Windows.Forms.TextBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.tasksView = new LH.Reminder2.Client.Controls.TasksView();
            this.tasksContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server base URL:";
            // 
            // serverBaseTextBox
            // 
            this.serverBaseTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.serverBaseTextBox.Location = new System.Drawing.Point(120, 12);
            this.serverBaseTextBox.Name = "serverBaseTextBox";
            this.serverBaseTextBox.Size = new System.Drawing.Size(538, 20);
            this.serverBaseTextBox.TabIndex = 1;
            this.serverBaseTextBox.Text = "http://localhost:62237/";
            // 
            // tasksContextMenuStrip
            // 
            this.tasksContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkTaskMenuItem,
            this.uncheckTaskMenuItem,
            this.deleteTaskMenuItem});
            this.tasksContextMenuStrip.Name = "tasksContextMenuStrip";
            this.tasksContextMenuStrip.Size = new System.Drawing.Size(171, 70);
            // 
            // checkTaskMenuItem
            // 
            this.checkTaskMenuItem.Name = "checkTaskMenuItem";
            this.checkTaskMenuItem.Size = new System.Drawing.Size(170, 22);
            this.checkTaskMenuItem.Text = "CheckTask.ashx";
            this.checkTaskMenuItem.Click += new System.EventHandler(this.checkTaskMenuItem_Click);
            // 
            // uncheckTaskMenuItem
            // 
            this.uncheckTaskMenuItem.Name = "uncheckTaskMenuItem";
            this.uncheckTaskMenuItem.Size = new System.Drawing.Size(170, 22);
            this.uncheckTaskMenuItem.Text = "UncheckTask.ashx";
            this.uncheckTaskMenuItem.Click += new System.EventHandler(this.uncheckTaskMenuItem_Click);
            // 
            // deleteTaskMenuItem
            // 
            this.deleteTaskMenuItem.Name = "deleteTaskMenuItem";
            this.deleteTaskMenuItem.Size = new System.Drawing.Size(170, 22);
            this.deleteTaskMenuItem.Text = "DeleteTask.ashx";
            this.deleteTaskMenuItem.Click += new System.EventHandler(this.deleteTaskMenuItem_Click);
            // 
            // getTasksButton
            // 
            this.getTasksButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.getTasksButton.Location = new System.Drawing.Point(12, 352);
            this.getTasksButton.Name = "getTasksButton";
            this.getTasksButton.Size = new System.Drawing.Size(124, 23);
            this.getTasksButton.TabIndex = 3;
            this.getTasksButton.Text = "GetTasks.ashx";
            this.getTasksButton.UseVisualStyleBackColor = true;
            this.getTasksButton.Click += new System.EventHandler(this.getTasksButton_Click);
            // 
            // userNameTextBox
            // 
            this.userNameTextBox.Location = new System.Drawing.Point(120, 38);
            this.userNameTextBox.Name = "userNameTextBox";
            this.userNameTextBox.Size = new System.Drawing.Size(174, 20);
            this.userNameTextBox.TabIndex = 4;
            this.userNameTextBox.Text = "Ladislav";
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(300, 38);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.PasswordChar = '*';
            this.passwordTextBox.Size = new System.Drawing.Size(174, 20);
            this.passwordTextBox.TabIndex = 5;
            // 
            // tasksView
            // 
            this.tasksView.Data = null;
            this.tasksView.FullRowSelect = true;
            this.tasksView.Location = new System.Drawing.Point(12, 75);
            this.tasksView.Name = "tasksView";
            this.tasksView.Size = new System.Drawing.Size(646, 271);
            this.tasksView.TabIndex = 6;
            this.tasksView.UseCompatibleStateImageBehavior = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(670, 387);
            this.Controls.Add(this.tasksView);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.userNameTextBox);
            this.Controls.Add(this.getTasksButton);
            this.Controls.Add(this.serverBaseTextBox);
            this.Controls.Add(this.label1);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.tasksContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox serverBaseTextBox;
        private System.Windows.Forms.Button getTasksButton;
        private System.Windows.Forms.TextBox userNameTextBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.ContextMenuStrip tasksContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem checkTaskMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteTaskMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uncheckTaskMenuItem;
        private LH.Reminder2.Client.Controls.TasksView tasksView;
    }
}