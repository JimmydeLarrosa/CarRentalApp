using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CarRentalApp
{
    public static class Utils
    {
        public static bool FormIsOpen(string name)
        {
            //Obtain a "list" of the forms open at the moment
            var OpenForms = Application.OpenForms.Cast<Form>();

            //Find in that list the form and if it is open, assing "true" to the variable
            return OpenForms.Any(q => q.Name == name);
        }

        public static string HashPassword(string password)
        {
            SHA256 sha = SHA256.Create();

            //Convert the input string to a byte array and compute the hash
            byte[] data = sha.ComputeHash(Encoding.UTF8.GetBytes(password));

            //Create a new Stringbuilder to collect the bytes and create a string
            StringBuilder sBuilder = new StringBuilder();

            //Loop through each byte of the hashed data and format each one as a hexadecimal string
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            //Assing the hash password to a string variable
            return sBuilder.ToString();
        }

        

    }
}
