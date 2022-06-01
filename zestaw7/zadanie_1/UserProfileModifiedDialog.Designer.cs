namespace zadanie_1
{
    partial class UserProfileModifiedDialog
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
            this.userSaveProfileButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.UserDateOfBirthTextBox = new System.Windows.Forms.TextBox();
            this.UserAddressTextBox = new System.Windows.Forms.TextBox();
            this.UserNameTextBox = new System.Windows.Forms.TextBox();
            this.UserSurnameTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // userSaveProfileButton
            // 
            this.userSaveProfileButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.userSaveProfileButton.Location = new System.Drawing.Point(150, 175);
            this.userSaveProfileButton.Name = "userSaveProfileButton";
            this.userSaveProfileButton.Size = new System.Drawing.Size(75, 23);
            this.userSaveProfileButton.TabIndex = 26;
            this.userSaveProfileButton.Text = "Zapisz";
            this.userSaveProfileButton.UseVisualStyleBackColor = true;
            this.userSaveProfileButton.Click += new System.EventHandler(this.userSaveProfileButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(65, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "Data urodzenia";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(104, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "Adres";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(85, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Nazwisko";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(112, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Imię";
            // 
            // UserDateOfBirthTextBox
            // 
            this.UserDateOfBirthTextBox.Location = new System.Drawing.Point(150, 134);
            this.UserDateOfBirthTextBox.Name = "UserDateOfBirthTextBox";
            this.UserDateOfBirthTextBox.Size = new System.Drawing.Size(159, 20);
            this.UserDateOfBirthTextBox.TabIndex = 21;
            // 
            // UserAddressTextBox
            // 
            this.UserAddressTextBox.Location = new System.Drawing.Point(150, 104);
            this.UserAddressTextBox.Name = "UserAddressTextBox";
            this.UserAddressTextBox.Size = new System.Drawing.Size(159, 20);
            this.UserAddressTextBox.TabIndex = 20;
            // 
            // UserNameTextBox
            // 
            this.UserNameTextBox.Location = new System.Drawing.Point(150, 45);
            this.UserNameTextBox.Name = "UserNameTextBox";
            this.UserNameTextBox.Size = new System.Drawing.Size(159, 20);
            this.UserNameTextBox.TabIndex = 19;
            // 
            // UserSurnameTextBox
            // 
            this.UserSurnameTextBox.Location = new System.Drawing.Point(150, 74);
            this.UserSurnameTextBox.Name = "UserSurnameTextBox";
            this.UserSurnameTextBox.Size = new System.Drawing.Size(159, 20);
            this.UserSurnameTextBox.TabIndex = 18;
            // 
            // UserProfileModifiedDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 226);
            this.Controls.Add(this.userSaveProfileButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.UserDateOfBirthTextBox);
            this.Controls.Add(this.UserAddressTextBox);
            this.Controls.Add(this.UserNameTextBox);
            this.Controls.Add(this.UserSurnameTextBox);
            this.Name = "UserProfileModifiedDialog";
            this.Text = "temp";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button userSaveProfileButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox UserDateOfBirthTextBox;
        private System.Windows.Forms.TextBox UserAddressTextBox;
        private System.Windows.Forms.TextBox UserNameTextBox;
        private System.Windows.Forms.TextBox UserSurnameTextBox;
    }
}