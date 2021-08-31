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
    public partial class AddEditUser : Form
    {
        private readonly CarRentalEntities _db = new CarRentalEntities();
        private bool isEditMode;
        private ManageUsers _refresh;
        public AddEditUser(ManageUsers refresh)
        {
            InitializeComponent();
            lbTitle.Text = "Add User";
            isEditMode = false;
            _refresh = refresh;
        }
        public AddEditUser(User user, ManageUsers refresh)
        {
            InitializeComponent();
            var _user = user;
            isEditMode = true;
            _refresh = refresh;

            tbUsername.Text = _user.username;
            lbTitle.Text = "Edit User";
            lbId.Text = user.id.ToString();
            lbId.Visible = false;
        }

        private void AddUser_Load(object sender, EventArgs e)
        {
            //Select from Roles
            var roles = _db.Roles.ToList();
            //Indicate the origin of the data used
            cbRole.DataSource = roles;
            //Show in the Combo Box the "names" of the users
            cbRole.DisplayMember = "name";
            //Get the id from users names
            cbRole.ValueMember = "id";
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (isEditMode)
                {
                    
                    var id = int.Parse(lbId.Text);
                    var userEdit = _db.Users.FirstOrDefault(q => q.id == id);

                    var username = tbUsername.Text;
                    var roleId = (int)cbRole.SelectedValue;
                    var password = Utils.HashPassword(tbPassword.Text);

                    userEdit.username = username;
                    userEdit.password = password;

                    var userRole = _db.UserRoles.FirstOrDefault(q => q.userid == id);
                    userRole.roleid = roleId;

                    _db.SaveChanges();

                    MessageBox.Show($"{userEdit.username}'s Has been changed");
                }
                else
                {
                    var name = _db.Users.FirstOrDefault(q => q.username == tbUsername.Text);
                    if (name != null)
                    {
                        MessageBox.Show("User alredy Register");
                    }
                    else
                    {
                        var username = tbUsername.Text;
                        var roleId = (int)cbRole.SelectedValue;
                        var password = Utils.HashPassword(tbPassword.Text);
                        var user = new User
                        {
                            username = username,
                            password = password,
                            isActive = true
                        };
                        _db.Users.Add(user);
                        _db.SaveChanges();

                        var userId = user.id;
                        var userRole = new UserRole
                        {
                            roleid = roleId,
                            userid = userId
                        };
                        _db.UserRoles.Add(userRole);
                        _db.SaveChanges();


                        MessageBox.Show($"{user.username}'s Has been added");
                    }
                }

                _refresh.PopulateGrid();

                Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
