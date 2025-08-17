<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewBills.aspx.cs" Inherits="ElectricityBillingApp.ViewBills" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Electricity Bills</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Retrieve Last N Bills</h2>
            <asp:Label runat="server" Text="Enter N:" />
            <asp:TextBox ID="txtN" runat="server" />
            <asp:Button ID="btnGet" runat="server" Text="Get Bills" OnClick="btnGet_Click" />
            <br /><br />
            <asp:GridView ID="gvBills" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="ConsumerNumber" HeaderText="Consumer Number" />
                    <asp:BoundField DataField="ConsumerName" HeaderText="Consumer Name" />
                    <asp:BoundField DataField="UnitsConsumed" HeaderText="Units Consumed" />
                    <asp:BoundField DataField="BillAmount" HeaderText="Bill Amount" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
