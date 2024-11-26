namespace My_First_Full_Real_Project
{
    partial class UserInfo
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
            this.ucUserDetails1 = new My_First_Full_Real_Project.UCUserDetails();
            this.SuspendLayout();
            // 
            // ucUserDetails1
            // 
            this.ucUserDetails1.Location = new System.Drawing.Point(13, 41);
            this.ucUserDetails1.Name = "ucUserDetails1";
            this.ucUserDetails1.Size = new System.Drawing.Size(965, 481);
            this.ucUserDetails1.TabIndex = 0;
            this.ucUserDetails1.Load += new System.EventHandler(this.ucUserDetails1_Load);
            // 
            // UserInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 517);
            this.Controls.Add(this.ucUserDetails1);
            this.Name = "UserInfo";
            this.Text = "UserInfo";
            this.ResumeLayout(false);

        }

        #endregion

        private UCUserDetails ucUserDetails1;
    }
}