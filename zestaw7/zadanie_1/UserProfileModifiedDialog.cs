using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace zadanie_1
{
    public partial class UserProfileModifiedDialog : Form
    {
        public UserProfileModifiedDialog(Person old_profile)
        {
            InitializeComponent();
            if (old_profile != null)
            {
                this.UserNameTextBox.Text = old_profile.Name;
                this.UserSurnameTextBox.Text = old_profile.Surname;
                this.UserAddressTextBox.Text = old_profile.Address;
                this.UserDateOfBirthTextBox.Text = old_profile.DateOfBirth.ToString();
            }
        }

        public Person NewProfile { get; set; }

        private void userSaveProfileButton_Click(object sender, EventArgs e)
        {
            this.NewProfile = new Person(
                this.UserNameTextBox.Text,
                this.UserSurnameTextBox.Text,
                this.UserAddressTextBox.Text,
                DateTime.Parse(this.UserDateOfBirthTextBox.Text)
            );
        }
    }
}
