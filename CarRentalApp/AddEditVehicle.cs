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
    public partial class AddEditVehicle : Form
    {
        private readonly CarRentalEntities _dbVehicles = new CarRentalEntities();
        private bool isEditMode;
        private ManageVehicleListing _refresh;
        public AddEditVehicle(ManageVehicleListing refresh = null)
        {
            InitializeComponent();
            lbTitle.Text = "Add New Vehicle";
            isEditMode = false;
            _refresh = refresh;
        }


        public AddEditVehicle(CarType carToEdit, ManageVehicleListing refresh = null)
        {
            InitializeComponent();
            lbTitle.Text = "Edit Vehicle";
            isEditMode = true;
            //Pass the object to the method
            PopulateFields(carToEdit);
            _refresh = refresh;
        }

        private void PopulateFields(CarType carToEdit)
        {
            //Set string values in the interface texts to visualise before change it
            lbId.Text = carToEdit.Id.ToString();
            tbMake.Text = carToEdit.Make;
            tbModel.Text = carToEdit.Model;
            tbVIN.Text = carToEdit.VIN;
            tbYear.Text = carToEdit.Year.ToString();
            tbLicensePlate.Text = carToEdit.LicensePlate;
        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            try
            {
                if (isEditMode)
                {                              
                        //Edit Code Here
                        //Store the new values in the properties of this object class
                        var id = int.Parse(lbId.Text);
                        var carEdit = _dbVehicles.CarTypes.FirstOrDefault(q => q.Id == id);
                        carEdit.Make = tbMake.Text;
                        carEdit.Model = tbModel.Text;
                        carEdit.VIN = tbVIN.Text;
                        carEdit.Year = int.Parse(tbYear.Text);
                        carEdit.LicensePlate = tbLicensePlate.Text;
                    
                    if (string.IsNullOrEmpty(carEdit.Make) || string.IsNullOrEmpty(carEdit.Model))
                    {
                        MessageBox.Show("Please enter atleast Make, Model and Year");
                        
                    }
                    else
                    {
                        _dbVehicles.SaveChanges();

                        _refresh.PopulateGrid();

                        this.Close();
                        MessageBox.Show("Operation Complete, please Refresh");
                    }
                }
                else
                {

                    if (string.IsNullOrEmpty(tbMake.Text) || string.IsNullOrEmpty(tbModel.Text))
                    {
                        MessageBox.Show("Please enter atleast Make, Model and Year");
                        
                    }
                    else
                    {
                        //Add Code Here
                        //Creates new object of CarType class and store the values
                        var carAdd = new CarType
                        {
                            Make = tbMake.Text,
                            Model = tbModel.Text,
                            VIN = tbVIN.Text,
                            Year = int.Parse(tbYear.Text),
                            LicensePlate = tbLicensePlate.Text
                        };
                        //Adds that new object to the data base
                        _dbVehicles.CarTypes.Add(carAdd);
                        //Saving changes
                        _dbVehicles.SaveChanges();

                        _refresh.PopulateGrid();

                        this.Close();

                        MessageBox.Show("Operation Complete, please Refresh");
                    }

                }
                


            }
            catch(Exception)
            {
                MessageBox.Show("Please enter atleast Make, Model and Year");
            }
                
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //Close the actual form
            this.Close();
        }

       


    }
}
