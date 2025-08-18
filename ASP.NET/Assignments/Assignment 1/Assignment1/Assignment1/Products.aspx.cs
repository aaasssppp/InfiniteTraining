using System;
using System.Collections.Generic;

namespace Assignment1
{
    public partial class Products : System.Web.UI.Page
    {
        Dictionary<string, (string ImageUrl, decimal Price)> products = new Dictionary<string, (string, decimal)>()
        {
            {"Laptop", ("~/Images/laptop.webp", 55000m)},
            {"Mobile", ("~/Images/mobile.webp", 25000m)},
            {"Headphones", ("~/Images/headphones.webp", 3000m)},
            {"Smartwatch", ("~/Images/smartwatch.webp", 8000m)}
        };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlProducts.DataSource = products.Keys;
                ddlProducts.DataBind();
                ddlProducts.Items.Insert(0, "--Select Product--");
            }
        }

        protected void ddlProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProducts.SelectedIndex > 0)
            {
                var product = products[ddlProducts.SelectedValue];
                imgProduct.ImageUrl = product.ImageUrl;
                lblPrice.Text = "";
            }
            else
            {
                imgProduct.ImageUrl = "";
                lblPrice.Text = "";
            }
        }

        protected void btnPrice_Click(object sender, EventArgs e)
        {
            if (ddlProducts.SelectedIndex > 0)
            {
                var product = products[ddlProducts.SelectedValue];
                lblPrice.Text = "Price: ₹" + product.Price.ToString("N2");
            }
            else
            {
                lblPrice.Text = "Please select a product.";
            }
        }
    }
}