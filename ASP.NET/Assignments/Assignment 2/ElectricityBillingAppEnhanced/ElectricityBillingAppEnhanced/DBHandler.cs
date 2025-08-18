using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;

public static class DBHandler
{
    private static SqlConnection GetConnection()
    {
        string connStr = ConfigurationManager.ConnectionStrings["EBConn"].ConnectionString;
        return new SqlConnection(connStr);
    }

    public static void AddBillToDB(ElectricityBill ebill)
    {
        using (SqlConnection con = GetConnection())
        {
            string query = "INSERT INTO ElectricityBill VALUES (@num, @name, @units, @bill)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@num", ebill.ConsumerNumber);
            cmd.Parameters.AddWithValue("@name", ebill.ConsumerName);
            cmd.Parameters.AddWithValue("@units", ebill.UnitsConsumed);
            cmd.Parameters.AddWithValue("@bill", ebill.BillAmount);

            con.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public static List<ElectricityBill> GetLastNBills(int n)
    {
        List<ElectricityBill> list = new List<ElectricityBill>();
        using (SqlConnection con = GetConnection())
        {
            string query = $"SELECT TOP {n} * FROM ElectricityBill ORDER BY consumer_number DESC";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                ElectricityBill eb = new ElectricityBill
                {
                    ConsumerNumber = dr["consumer_number"].ToString(),
                    ConsumerName = dr["consumer_name"].ToString(),
                    UnitsConsumed = Convert.ToInt32(dr["units_consumed"]),
                    BillAmount = Convert.ToDouble(dr["bill_amount"])
                };
                list.Add(eb);
            }
        }
        return list;
    }
}
