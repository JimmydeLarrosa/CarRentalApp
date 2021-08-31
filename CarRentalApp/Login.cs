using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRentalApp
{
    public partial class Login : Form
    {
        private readonly CarRentalEntities _db = new CarRentalEntities();
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                SHA256 sha = SHA256.Create();

                var username = tbUsername.Text.Trim();
                var password = tbPassword.Text;

                var hashed_password = Utils.HashPassword(password);

                //Check for matching username, password and active flag
                var user = _db.Users.FirstOrDefault(q => q.username == username && q.password == hashed_password && q.isActive == true);
                if(user == null)
                {
                    MessageBox.Show("Please introduce valid data");
                }
                else
                {
                    
                    var main = new MainWindow(this, user);
                    main.Show();
                    Hide();
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Something went wrong. Try again \n {ex.Message}");
            }

        }
    }
}
