using System;
using System.Text.RegularExpressions;

namespace Assignment1
{
    public partial class Validator : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCheck_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string family = txtFamilyName.Text.Trim();
            string address = txtAddress.Text.Trim();
            string city = txtCity.Text.Trim();
            string zip = txtZip.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string email = txtEmail.Text.Trim();

            if (name.Equals(family, StringComparison.OrdinalIgnoreCase))
            {
                lblMessage.Text = "Name must be different from Family Name.";
                return;
            }
            if (address.Length < 2)
            {
                lblMessage.Text = "Address must have at least 2 letters.";
                return;
            }
            if (city.Length < 2)
            {
                lblMessage.Text = "City must have at least 2 letters.";
                return;
            }
            if (!Regex.IsMatch(zip, @"^\d{6}$"))
            {
                lblMessage.Text = "Zip Code must be exactly 6 digits.";
                return;
            }
            if (!Regex.IsMatch(phone, @"^\d{10}$"))
            {
                lblMessage.Text = "Phone format must be exactly 10 digits.";
                return;
            }
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                lblMessage.Text = "Invalid email address.";
                return;
            }

            lblMessage.ForeColor = System.Drawing.Color.Green;
            lblMessage.Text = "All inputs are valid!";
            
        }
    }

}