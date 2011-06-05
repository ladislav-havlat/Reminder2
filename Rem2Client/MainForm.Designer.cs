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
            this.label1 = new System.Windows.Forms.Label();
            this.serverBaseTextBox = new System.Windows.Forms.TextBox();
            this.tasksListBox = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.getTasksButton = new System.Windows.Forms.Button();
            this.userNameTextBox = new System.Windows.Forms.TextBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
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
            this.serverBaseTextBox.Text = "http://localhost:60000/";
            // 
            // tasksListBox
            // 
            this.tasksListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tasksListBox.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.tasksListBox.Location = new System.Drawing.Point(12, 64);
            this.tasksListBox.Name = "tasksListBox";
            this.tasksListBox.Size = new System.Drawing.Size(646, 282);
            this.tasksListBox.TabIndex = 2;
            this.tasksListBox.UseCompatibleStateImageBehavior = false;
            this.tasksListBox.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Message";
            this.columnHeader1.Width = 400;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "DateTime";
            this.columnHeader2.Width = 150;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Checked";
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(670, 387);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.userNameTextBox);
            this.Controls.Add(this.getTasksButton);
            this.Controls.Add(this.tasksListBox);
            this.Controls.Add(this.serverBaseTextBox);
            this.Controls.Add(this.label1);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox serverBaseTextBox;
        private System.Windows.Forms.ListView tasksListBox;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button getTasksButton;
        private System.Windows.Forms.TextBox userNameTextBox;
        private System.Windows.Forms.TextBox passwordTextBox;
    }
}