using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ElectricityBillingAppEnhanced
{
    public partial class AddBill : System.Web.UI.Page
    {
        static int totalBills = 0;
        static int currentBill = 0;

        protected void btnStart_Click(object sender, EventArgs e)
        {
            totalBills = Convert.ToInt32(txtCount.Text);
            currentBill = 0;
            pnlBillForm.Visible = true;
        }

        protected void btnNext_Click(object sender, EventArgs e)
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

                currentBill++;
                lblResult.Text = $"Bill {currentBill}/{totalBills} Added! Amount: {eb.BillAmount}";

                txtConsumerNo.Text = txtConsumerName.Text = txtUnits.Text = "";

                if (currentBill >= totalBills)
                {
                    lblResult.Text += "<br/> All bills have been added!";
                    pnlBillForm.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblResult.CssClass = "text-danger";
                lblResult.Text = ex.Message;
            }
        }
    protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}