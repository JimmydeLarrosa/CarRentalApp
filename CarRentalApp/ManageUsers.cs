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
    public partial class ManageUsers : Form
    {
        private readonly CarRentalEntities _db;
        public ManageUsers()
        {
            InitializeComponent();
            _db = new CarRentalEntities();
        }

        private void ManageUsers_Load(object sender, EventArgs e)
        {
            PopulateGrid();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            if (!Utils.FormIsOpen("AddUser"))
            {
                var addUsers = new AddEditUser(this);
                addUsers.MdiParent = this.MdiParent;
                addUsers.Show();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                //Get user of selected row
                var name = gvUserList.SelectedRows[0].Cells["user"].Value.ToString();

                //Query database for record
                var user = _db.Users.FirstOrDefault(q => q.username == name);
                var editUser = new AddEditUser(user, this);
                editUser.Show();
                _db.SaveChanges();

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnDeactivateUser_Click(object sender, EventArgs e)
        {

            try
            {
                //Get Id of selected row
                var name = gvUserList.SelectedRows[0].Cells["user"].Value.ToString();

                //Query database for record
                var user = _db.Users.FirstOrDefault(q => q.username == name);
                //if(user.isActive == true) ==>> user.isActive = false
                //else user.isActive = flase
                user.isActive = user.isActive == true ? false : true;
                _db.SaveChanges();

                MessageBox.Show($"{user.username} Active status has changed");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            PopulateGrid();
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            PopulateGrid();
        }

        public void PopulateGrid()
        {
            var userList = _db.UserRoles.Select(q => new
            {
                id = q.id,
                user = q.User.username,
                role = q.Role.name,
                active = q.User.isActive

            }).ToList();

            gvUserList.DataSource = userList;

            gvUserList.Columns[0].Visible = false;
        }

        
    }
}
