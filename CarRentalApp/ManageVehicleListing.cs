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
    public partial class ManageVehicleListing : Form
    {
        private readonly CarRentalEntities _dbVehicles;
        public ManageVehicleListing()
        {
            InitializeComponent();
            _dbVehicles = new CarRentalEntities();
        }

        private void ManageVehicleListing_Load(object sender, EventArgs e)
        {
            //Arrow function to select these columns and make a list with it
            //Provide data to my list of cars
            PopulateGrid();

            //Change the title from these columns[index]
            gvVehicleList.Columns[0].Visible = false;
            gvVehicleList.Columns[1].HeaderText = "MAKE";
            gvVehicleList.Columns[2].HeaderText = "MODEL";
            gvVehicleList.Columns[3].HeaderText = "VIN";
            gvVehicleList.Columns[4].HeaderText = "LICENSE PLATE";
            gvVehicleList.Columns[5].HeaderText = "YEAR";

        }

           
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                var carAdd = new AddEditVehicle(this);
                carAdd.MdiParent = this.MdiParent;
                carAdd.Show();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                //Get Id of selected row
                var id = (int)gvVehicleList.SelectedRows[0].Cells["Id"].Value;

                //Query database for record
                var car = _dbVehicles.CarTypes.FirstOrDefault(q => q.Id == id);

                //Launch AddEditVehicle window with data
                var carEdit = new AddEditVehicle(car, this);
                carEdit.MdiParent = this.MdiParent;
                carEdit.Show();
            }
            catch( Exception )
            {
                MessageBox.Show("Choose correctly the row");
            }

        }

        private void btnDelet_Click(object sender, EventArgs e)
        {
            try
            {
                //Get Id of selected row
                var id = (int)gvVehicleList.SelectedRows[0].Cells["Id"].Value;

                //Query database for record
                var car = _dbVehicles.CarTypes.FirstOrDefault(q => q.Id == id);

                DialogResult dr = MessageBox.Show("Are you sure you want to delete this record?",
                    "Delete", MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    //Delete Vehicle from table
                    _dbVehicles.CarTypes.Remove(car);
                    _dbVehicles.SaveChanges();

                }

                PopulateGrid();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            PopulateGrid();
        }
        public void PopulateGrid()
        {
            //Arrow function to select these columns and make a list with it
            var carList = _dbVehicles.CarTypes.Select(q => new
            {
                Id = q.Id,
                Make = q.Make,
                Model = q.Model,
                VIN = q.VIN,
                License = q.LicensePlate,
                Year = q.Year
            }).ToList();
            //Provide data to my list of cars
            gvVehicleList.DataSource = carList;

            gvVehicleList.Columns[0].Visible = false;
        }

      
    }
}
