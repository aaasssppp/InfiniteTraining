<%@ Page Title="Login" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Login.aspx.cs" Inherits="ElectricityBillingAppEnhanced.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row justify-content-center">
        <div class="col-md-4">
            <h3 class="text-center">Admin Login</h3>
            <div class="mb-3">
                <asp:Label Text="Username" runat="server" CssClass="form-label" />
                <asp:TextBox ID="txtUser" runat="server" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <asp:Label Text="Password" runat="server" CssClass="form-label" />
                <asp:TextBox ID="txtPass" runat="server" TextMode="Password" CssClass="form-control" />
            </div>
            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary w-100" OnClick="btnLogin_Click" />
            <asp:Label ID="lblMsg" runat="server" CssClass="text-danger d-block mt-2" />
        </div>
    </div>
</asp:Content>
