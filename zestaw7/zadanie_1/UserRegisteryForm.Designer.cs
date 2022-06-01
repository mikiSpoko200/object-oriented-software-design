
namespace zadanie_1
{
    partial class UserRegisteryForm
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
    private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TabPage workingRegionTabUserData;
            this.userModifyButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateOfBirthTextBox = new System.Windows.Forms.TextBox();
            this.adresTextBox = new System.Windows.Forms.TextBox();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.surnameTextBox = new System.Windows.Forms.TextBox();
            this.userPanelTreeView = new System.Windows.Forms.Panel();
            this.userTreeView = new System.Windows.Forms.TreeView();
            this.workingRegionPanel = new System.Windows.Forms.Panel();
            this.workingRegionTabControl = new System.Windows.Forms.TabControl();
            this.workingRegionTabUserList = new System.Windows.Forms.TabPage();
            this.userAddButton = new System.Windows.Forms.Button();
            this.userListView = new System.Windows.Forms.ListView();
            workingRegionTabUserData = new System.Windows.Forms.TabPage();
            workingRegionTabUserData.SuspendLayout();
            this.userPanelTreeView.SuspendLayout();
            this.workingRegionPanel.SuspendLayout();
            this.workingRegionTabControl.SuspendLayout();
            this.workingRegionTabUserList.SuspendLayout();
            this.SuspendLayout();
            // 
            // workingRegionTabUserData
            // 
            workingRegionTabUserData.Controls.Add(this.userModifyButton);
            workingRegionTabUserData.Controls.Add(this.label4);
            workingRegionTabUserData.Controls.Add(this.label3);
            workingRegionTabUserData.Controls.Add(this.label2);
            workingRegionTabUserData.Controls.Add(this.label1);
            workingRegionTabUserData.Controls.Add(this.dateOfBirthTextBox);
            workingRegionTabUserData.Controls.Add(this.adresTextBox);
            workingRegionTabUserData.Controls.Add(this.nameTextBox);
            workingRegionTabUserData.Controls.Add(this.surnameTextBox);
            workingRegionTabUserData.Location = new System.Drawing.Point(4, 34);
            workingRegionTabUserData.Name = "workingRegionTabUserData";
            workingRegionTabUserData.Padding = new System.Windows.Forms.Padding(3);
            workingRegionTabUserData.Size = new System.Drawing.Size(552, 385);
            workingRegionTabUserData.TabIndex = 1;
            workingRegionTabUserData.Text = "tabPage2";
            workingRegionTabUserData.UseVisualStyleBackColor = true;
            // 
            // userModifyButton
            // 
            this.userModifyButton.Location = new System.Drawing.Point(274, 259);
            this.userModifyButton.Name = "userModifyButton";
            this.userModifyButton.Size = new System.Drawing.Size(75, 23);
            this.userModifyButton.TabIndex = 8;
            this.userModifyButton.Text = "Modyfikuj";
            this.userModifyButton.UseVisualStyleBackColor = true;
            this.userModifyButton.Click += new System.EventHandler(this.userModifyButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(153, 225);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Data urodzenia";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(192, 195);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Adres";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(173, 165);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Nazwisko";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(200, 136);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Imię";
            // 
            // dateOfBirthTextBox
            // 
            this.dateOfBirthTextBox.Location = new System.Drawing.Point(232, 222);
            this.dateOfBirthTextBox.Name = "dateOfBirthTextBox";
            this.dateOfBirthTextBox.ReadOnly = true;
            this.dateOfBirthTextBox.Size = new System.Drawing.Size(159, 20);
            this.dateOfBirthTextBox.TabIndex = 3;
            // 
            // adresTextBox
            // 
            this.adresTextBox.Location = new System.Drawing.Point(232, 192);
            this.adresTextBox.Name = "adresTextBox";
            this.adresTextBox.ReadOnly = true;
            this.adresTextBox.Size = new System.Drawing.Size(159, 20);
            this.adresTextBox.TabIndex = 2;
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(232, 133);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.ReadOnly = true;
            this.nameTextBox.Size = new System.Drawing.Size(159, 20);
            this.nameTextBox.TabIndex = 1;
            // 
            // surnameTextBox
            // 
            this.surnameTextBox.Location = new System.Drawing.Point(232, 162);
            this.surnameTextBox.Name = "surnameTextBox";
            this.surnameTextBox.ReadOnly = true;
            this.surnameTextBox.Size = new System.Drawing.Size(159, 20);
            this.surnameTextBox.TabIndex = 0;
            // 
            // userPanelTreeView
            // 
            this.userPanelTreeView.Controls.Add(this.userTreeView);
            this.userPanelTreeView.Dock = System.Windows.Forms.DockStyle.Left;
            this.userPanelTreeView.Location = new System.Drawing.Point(0, 0);
            this.userPanelTreeView.Name = "userPanelTreeView";
            this.userPanelTreeView.Size = new System.Drawing.Size(205, 423);
            this.userPanelTreeView.TabIndex = 2;
            // 
            // userTreeView
            // 
            this.userTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userTreeView.Location = new System.Drawing.Point(0, 0);
            this.userTreeView.Name = "userTreeView";
            this.userTreeView.Size = new System.Drawing.Size(205, 423);
            this.userTreeView.TabIndex = 0;
            this.userTreeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.userTreeView_BeforeExpand);
            this.userTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.userTreeView_AfterSelect);
            // 
            // workingRegionPanel
            // 
            this.workingRegionPanel.Controls.Add(this.workingRegionTabControl);
            this.workingRegionPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.workingRegionPanel.Location = new System.Drawing.Point(205, 0);
            this.workingRegionPanel.Name = "workingRegionPanel";
            this.workingRegionPanel.Size = new System.Drawing.Size(560, 423);
            this.workingRegionPanel.TabIndex = 3;
            // 
            // workingRegionTabControl
            // 
            this.workingRegionTabControl.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.workingRegionTabControl.Controls.Add(this.workingRegionTabUserList);
            this.workingRegionTabControl.Controls.Add(workingRegionTabUserData);
            this.workingRegionTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.workingRegionTabControl.ItemSize = new System.Drawing.Size(60, 30);
            this.workingRegionTabControl.Location = new System.Drawing.Point(0, 0);
            this.workingRegionTabControl.Multiline = true;
            this.workingRegionTabControl.Name = "workingRegionTabControl";
            this.workingRegionTabControl.SelectedIndex = 0;
            this.workingRegionTabControl.Size = new System.Drawing.Size(560, 423);
            this.workingRegionTabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.workingRegionTabControl.TabIndex = 1;
            // 
            // workingRegionTabUserList
            // 
            this.workingRegionTabUserList.Controls.Add(this.userAddButton);
            this.workingRegionTabUserList.Controls.Add(this.userListView);
            this.workingRegionTabUserList.Location = new System.Drawing.Point(4, 34);
            this.workingRegionTabUserList.Name = "workingRegionTabUserList";
            this.workingRegionTabUserList.Padding = new System.Windows.Forms.Padding(3);
            this.workingRegionTabUserList.Size = new System.Drawing.Size(552, 385);
            this.workingRegionTabUserList.TabIndex = 0;
            this.workingRegionTabUserList.Text = "tabPage1";
            this.workingRegionTabUserList.UseVisualStyleBackColor = true;
            // 
            // userAddButton
            // 
            this.userAddButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.userAddButton.Location = new System.Drawing.Point(3, 351);
            this.userAddButton.Name = "userAddButton";
            this.userAddButton.Size = new System.Drawing.Size(546, 31);
            this.userAddButton.TabIndex = 1;
            this.userAddButton.Text = "Dodaj nowy";
            this.userAddButton.UseVisualStyleBackColor = true;
            this.userAddButton.Click += new System.EventHandler(this.userAddButton_Click);
            // 
            // userListView
            // 
            this.userListView.Dock = System.Windows.Forms.DockStyle.Top;
            this.userListView.GridLines = true;
            this.userListView.HideSelection = false;
            this.userListView.LabelWrap = false;
            this.userListView.Location = new System.Drawing.Point(3, 3);
            this.userListView.Name = "userListView";
            this.userListView.Size = new System.Drawing.Size(546, 342);
            this.userListView.TabIndex = 0;
            this.userListView.UseCompatibleStateImageBehavior = false;
            this.userListView.View = System.Windows.Forms.View.Details;
            // 
            // UserRegisteryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(765, 423);
            this.Controls.Add(this.workingRegionPanel);
            this.Controls.Add(this.userPanelTreeView);
            this.Name = "UserRegisteryForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UserRegisteryForm_FormClosing);
            this.Load += new System.EventHandler(this.UserRegisteryForm_Load);
            workingRegionTabUserData.ResumeLayout(false);
            workingRegionTabUserData.PerformLayout();
            this.userPanelTreeView.ResumeLayout(false);
            this.workingRegionPanel.ResumeLayout(false);
            this.workingRegionTabControl.ResumeLayout(false);
            this.workingRegionTabUserList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel userPanelTreeView;
        private System.Windows.Forms.TreeView userTreeView;
        private System.Windows.Forms.Panel workingRegionPanel;
        private System.Windows.Forms.TabControl workingRegionTabControl;
        private System.Windows.Forms.TabPage workingRegionTabUserList;
        private System.Windows.Forms.ListView userListView;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox dateOfBirthTextBox;
        private System.Windows.Forms.TextBox adresTextBox;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.TextBox surnameTextBox;
        private System.Windows.Forms.Button userModifyButton;
        private System.Windows.Forms.Button userAddButton;
    }
}

