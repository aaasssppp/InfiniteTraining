<%@ Page Title="Add Bill" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="AddBill.aspx.cs" Inherits="ElectricityBillingAppEnhanced.AddBill" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="col-md-6 mx-auto">
        <h3 class="text-center">Add Electricity Bills</h3>
        <div class="mb-3">
            <asp:Label Text="How many bills to add?" runat="server" CssClass="form-label" />
            <asp:TextBox ID="txtCount" runat="server" CssClass="form-control" />
        </div>
        <asp:Button ID="btnStart" runat="server" Text="Start Adding" CssClass="btn btn-primary" OnClick="btnStart_Click" />

        <asp:Panel ID="pnlBillForm" runat="server" Visible="false" CssClass="mt-4">
            <div class="mb-3">
                <asp:Label Text="Consumer Number" runat="server" />
                <asp:TextBox ID="txtConsumerNo" runat="server" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <asp:Label Text="Consumer Name" runat="server" />
                <asp:TextBox ID="txtConsumerName" runat="server" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <asp:Label Text="Units Consumed" runat="server" />
                <asp:TextBox ID="txtUnits" runat="server" CssClass="form-control" />
            </div>
            <asp:Button ID="btnNext" runat="server" Text="Add Next Bill" CssClass="btn btn-success" OnClick="btnNext_Click" />
            <br />
            <asp:Label ID="lblResult" runat="server" CssClass="text-success mt-2 d-block" />
        </asp:Panel>
    </div>
</asp:Content>
