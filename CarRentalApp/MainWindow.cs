using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRentalApp
{
    public partial class MainWindow : Form
    {
        private Login _login;
        public string _roleName;
        public User _user;
       
        public MainWindow()
        {
            InitializeComponent();
        }
        public MainWindow(Login login, User user)
        {
            InitializeComponent();
            _login = login;
            _user = user;
            _roleName = _user.UserRoles.FirstOrDefault().Role.shortname;

        }

        private void manageVehicleListingToolStripMenuItem_Click(object sender, EventArgs e)
        {
           //Check if window is already open
            if (!Utils.FormIsOpen("ManageVehicleListing"))
            {
                var vehicleListing = new ManageVehicleListing();
                vehicleListing.MdiParent = this;
                vehicleListing.Show();
            }
        }

        private void manageRentalRecordsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //Check if window is already open
            if (!Utils.FormIsOpen("ManageRentalRecords"))
            {
                var rentalRecords = new ManageRentalRecords();
                rentalRecords.MdiParent = this;
                rentalRecords.Show();
            }
        }
        private void manageUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Check if window is already open
            if (!Utils.FormIsOpen("ManageUsers"))
            {

                var users = new ManageUsers();
                users.MdiParent = this;
                users.Show();
            }
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            _login.Close();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            if(_roleName != "admin")
            {
                manageUsersToolStripMenuItem.Visible = false;
            }
            tssLoginText.Text = "Loggin in as: " + _user.username;
        }
    }
}
