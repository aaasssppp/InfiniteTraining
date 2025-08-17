using System;

public class ElectricityBill
{
    private string consumerNumber;
    private string consumerName;
    private int unitsConsumed;
    private double billAmount;

    public string ConsumerNumber
    {
        get { return consumerNumber; }
        set
        {
            if (!value.StartsWith("EB") || value.Length != 7)
                throw new FormatException("Invalid Consumer Number");
            consumerNumber = value;
        }
    }

    public string ConsumerName { get => consumerName; set => consumerName = value; }

    public int UnitsConsumed
    {
        get { return unitsConsumed; }
        set
        {
            if (value < 0) throw new ArgumentException("Given units is invalid");
            unitsConsumed = value;
        }
    }

    public double BillAmount { get => billAmount; set => billAmount = value; }
}
