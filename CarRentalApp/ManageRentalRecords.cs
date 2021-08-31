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
    public partial class ManageRentalRecords : Form
    {
        private readonly CarRentalEntities _dbRental = new CarRentalEntities();
        public ManageRentalRecords()
        {
            InitializeComponent();
        }

        private void ManageRentalRecords_Load(object sender, EventArgs e)
        {
            PopulateGrid();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                var rentalAdd = new AddEditRentalRecord(this);
                rentalAdd.MdiParent = this.MdiParent;
                rentalAdd.Show();
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
                var id = (int)gvRentalList.SelectedRows[0].Cells["Id"].Value;

                //Query database for record
                var recordEdit = _dbRental.CarRentalRecords.FirstOrDefault(q => q.id == id);

                //Launch AddEditVehicle window with data
                var rentalEdit = new AddEditRentalRecord(recordEdit, this);
                rentalEdit.MdiParent = this.MdiParent;
                rentalEdit.Show();
            }
            catch (Exception ex)
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
            var rentalList = _dbRental.CarRentalRecords.Select(q => new
            {
                id = q.id,
                Costumer = q.Costumer,
                DateRented = q.DateRented,
                DateReturn = q.DateReturn,
                Cost = q.Cost,
                Car = q.CarType.Make + " " + q.CarType.Model

            }).ToList();

            gvRentalList.DataSource = rentalList;

            gvRentalList.Columns[0].Visible = false;
        }
        private void btnDelet_Click(object sender, EventArgs e)
        {
            try
            {
                //Get Id of selected row

                var id = (int)gvRentalList.SelectedRows[0].Cells["id"].Value;

                //Query database for record

                var rentalRecord = _dbRental.CarRentalRecords.FirstOrDefault(q => q.id == id);

                DialogResult dr = MessageBox.Show("Are you sure you want to delete this record?",
                   "Delete", MessageBoxButtons.YesNoCancel,
                   MessageBoxIcon.Warning);

                //Delete Vehicle from table
                if (dr == DialogResult.Yes)
                {
                    _dbRental.CarRentalRecords.Remove(rentalRecord);
                    _dbRental.SaveChanges();

                }

                PopulateGrid();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
