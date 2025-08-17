using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ElectricityBillingAppEnhanced
{
    public partial class ViewBills : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnGet_Click(object sender, EventArgs e)
        {
            try
            {
                int n = Convert.ToInt32(txtN.Text);
                ElectricityBoard board = new ElectricityBoard();
                List<ElectricityBill> bills = board.Generate_N_BillDetails(n);

                gvBills.DataSource = bills;
                gvBills.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }
        }
    }
}