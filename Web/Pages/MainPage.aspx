<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true"
    CodeBehind="MainPage.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.MainPage" meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <br />
    <br />
    <br />
    <br />
    <div align="center">

        <asp:Label ID="lblNotAuthenticated" runat="server" Text="<%$ Resources:, lblNotAuthenticatedRes %>"></asp:Label>
        <asp:Label ID="lblAuthenticated" runat="server" Text="<%$ Resources:, lblAuthenticatedRes %>"></asp:Label>
        <asp:Label ID="lblFirstName" runat="server"  Font-Bold="True"></asp:Label>

    </div>
    <br />
    <br />
    <br />
    <br />
</asp:Content>