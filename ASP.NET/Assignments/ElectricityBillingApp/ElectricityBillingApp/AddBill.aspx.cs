using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ElectricityBillingApp
{
    public partial class AddBill : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                ElectricityBill eb = new ElectricityBill();
                eb.ConsumerNumber = txtConsumerNo.Text.Trim();
                eb.ConsumerName = txtConsumerName.Text.Trim();
                eb.UnitsConsumed = Convert.ToInt32(txtUnits.Text);

                ElectricityBoard board = new ElectricityBoard();
                board.CalculateBill(eb);
                board.AddBill(eb);

                lblResult.Text = $"Bill Added Successfully! Amount: {eb.BillAmount}";
            }
            catch (FormatException ex)
            {
                lblResult.ForeColor = System.Drawing.Color.Red;
                lblResult.Text = ex.Message;
            }
            catch (ArgumentException ex)
            {
                lblResult.ForeColor = System.Drawing.Color.Red;
                lblResult.Text = ex.Message;
            }
            catch (Exception ex)
            {
                lblResult.ForeColor = System.Drawing.Color.Red;
                lblResult.Text = "Error: " + ex.Message;
            }
        }
    }
}