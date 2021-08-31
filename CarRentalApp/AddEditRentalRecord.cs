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
    public partial class AddEditRentalRecord : Form
    {
        private readonly CarRentalEntities carRentalEntities = new CarRentalEntities();
        private ManageRentalRecords _refresh;
        private bool isEditMode;
        public AddEditRentalRecord(ManageRentalRecords refresh = null)
        {
            InitializeComponent();
            lbTitle.Text = "Add Rental Record";
            isEditMode = false;
            _refresh = refresh;
        }

        public AddEditRentalRecord(CarRentalRecord recordEdit, ManageRentalRecords refresh = null)
        {
            InitializeComponent();
            lbTitle.Text = "Edit Rental Record";
            isEditMode = true;
            PopulateFields(recordEdit);
            _refresh = refresh;
        }

        private void PopulateFields(CarRentalRecord recordEdit)
        {
            tbCustomerName.Text = recordEdit.Costumer;
            tbCost.Text = recordEdit.Cost.ToString();
            dtpDateRented.Value = (DateTime)recordEdit.DateRented;
            dtpDateReturn.Value = (DateTime)recordEdit.DateReturn;
            lbid.Text = recordEdit.id.ToString();
            lbid.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
              if (isEditMode)
              {
                    var isValid = true;
                    var errorMessage = "";

                    if (string.IsNullOrWhiteSpace(tbCustomerName.Text))
                    {
                        isValid = false;
                        errorMessage += "Please enter missing data.\n\r";

                       
                    }
                    if (dtpDateReturn.Value < dtpDateRented.Value)
                    {
                        isValid = false;
                        errorMessage += "Illigal date selection.\n\r";
                    }
                    if (isValid)
                    {
                        var id = int.Parse(lbid.Text);
                        var rentalEdit = carRentalEntities.CarRentalRecords.FirstOrDefault(q => q.id == id);
                        rentalEdit.Costumer = tbCustomerName.Text;
                        rentalEdit.DateRented = dtpDateRented.Value;
                        rentalEdit.DateReturn = dtpDateReturn.Value;
                        rentalEdit.Cost = decimal.Parse(tbCost.Text);
                        rentalEdit.CarTypeid = (int)cbCarType.SelectedValue;

                        carRentalEntities.SaveChanges();

                        _refresh.PopulateGrid();

                        this.Close();

                    }
                    else
                    {
                        MessageBox.Show(errorMessage);
                    }

                }
              else
              {
                
                    //Storing "values" from the interface to variables
                    string customerName = tbCustomerName.Text;
                    var dateOut = dtpDateRented.Value;
                    var dateIn = dtpDateReturn.Value;
                    var carType = cbCarType.Text;
                    double cost = Convert.ToDouble(tbCost.Text);
                    //Conditioning variable
                    var isValid = true;
                    //Error variable
                    var errorMessage = "";

                    if (string.IsNullOrWhiteSpace(customerName))
                    {
                        isValid = false;
                        errorMessage += "Please enter missing data.\n\r";

                    }
                    if (dateIn < dateOut)
                    {
                        isValid = false;
                        errorMessage += "Illigal date selection.\n\r";
                    }

                    if (isValid)
                    {
                        //Create an object of the class who contains the properties of the table
                        CarRentalRecord rentalRecord = new CarRentalRecord();
                        //Store the datta for the table, using the properties from the class(*table*)
                        rentalRecord.Costumer = customerName;
                        rentalRecord.DateRented = dateOut;
                        rentalRecord.DateReturn = dateIn;
                        rentalRecord.Cost = (decimal)cost;
                        rentalRecord.CarTypeid = (int)cbCarType.SelectedValue;
                        //Add the datta to the table and store it in the data base
                        carRentalEntities.CarRentalRecords.Add(rentalRecord);
                        //Save changes stored in the data base
                        carRentalEntities.SaveChanges();

                        _refresh.PopulateGrid();

                        this.Close();

                        //Throw a mesage to the user with the data
                        MessageBox.Show($"Customer Name: {customerName}\n\r" +
                            $"Date Rented: {dateOut}\n\r" +
                            $"Date Returned: {dateIn}\n\r" +
                            $"Cost: {cost}\n\r" +
                            $"Car Type: {carType}\n\r" +
                            $"Thank You");
                        
                    }
                    else
                    {
                        MessageBox.Show(errorMessage);
                    }
               
              }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //throw;
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            
            
            //Select from CarTypes
            //var cars = carRentalEntities.CarTypes.ToList();
            var cars = carRentalEntities.CarTypes.Select(q => new { Id = q.Id, Name = q.Make + " " + q.Model }).ToList();
            //Indicate the origin of the data used
            cbCarType.DataSource = cars;
            //Show in the Combo Box the "names" of the cars
            cbCarType.DisplayMember = "Name";
            //Get the id from cars names
            cbCarType.ValueMember = "Id";
            
        }

      
    }
}
