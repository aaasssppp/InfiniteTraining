using System;
using System.Collections.Generic;

public class ElectricityBoard
{
    public void CalculateBill(ElectricityBill ebill)
    {
        int units = ebill.UnitsConsumed;
        double bill = 0;

        if (units <= 100) bill = 0;
        else if (units <= 300) bill = (units - 100) * 1.5;
        else if (units <= 600) bill = 200 * 1.5 + (units - 300) * 3.5;
        else if (units <= 1000) bill = 200 * 1.5 + 300 * 3.5 + (units - 600) * 5.5;
        else bill = 200 * 1.5 + 300 * 3.5 + 400 * 5.5 + (units - 1000) * 7.5;

        ebill.BillAmount = bill;
    }

    public void AddBill(ElectricityBill ebill)
    {
        DBHandler.AddBillToDB(ebill);
    }

    public List<ElectricityBill> Generate_N_BillDetails(int n)
    {
        return DBHandler.GetLastNBills(n);
    }
}
