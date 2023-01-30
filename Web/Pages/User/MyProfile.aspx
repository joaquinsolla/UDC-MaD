<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="MyProfile.aspx.cs"
    Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.User.MyProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
    <div align="center">
        <asp:Label ID="lblMyProfileTitle" runat="server" Text="<%$ Resources:, lblMyProfileTitleRes %>" Font-Bold="false" Font-Size="Large"></asp:Label>
        <br />
        <br />
        <asp:HyperLink ID="lnkUpdateUserProfile" runat="server"
                NavigateUrl="~/Pages/User/UpdateUserProfile.aspx"
                Text="<%$ Resources:, lnkUpdateUserProfileRes %>" ForeColor="#0033CC" />
        &nbsp&nbsp&nbsp&nbsp
        <asp:HyperLink ID="lnkChangePassword" runat="server"
                NavigateUrl="~/Pages/User/ChangePassword.aspx"
                Text="<%$ Resources:, lnkChangePasswordRes %>" ForeColor="#0033CC" />
        &nbsp&nbsp&nbsp&nbsp
        <asp:HyperLink ID="lnkAddBankCard" runat="server"
            NavigateUrl="~/Pages/User/AddBankCard.aspx"
            Text="<%$ Resources:, lnkAddBankCardRes %>" ForeColor="#0033CC" />
        <br /><br />
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div align="center">
        <form runat="server">

            <asp:Table CssClass="productDetails" ID="TableProductInfo" runat="server">
            <asp:TableRow runat="server">
                <asp:TableHeaderCell ID="cellCaptionFirstName" runat="server" Text="<%$ Resources:, lblFirstNameRes %>"></asp:TableHeaderCell>
                <asp:TableCell ID="cellFirstName" runat="server"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableHeaderCell ID="cellCaptionLastName" runat="server" Text="<%$ Resources:, lblLastNameRes %>"></asp:TableHeaderCell>
                <asp:TableCell ID="cellLastName" runat="server"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableHeaderCell ID="cellCaptionEmail" runat="server" Text="<%$ Resources:, lblEmailRes %>"></asp:TableHeaderCell>
                <asp:TableCell ID="cellEmail" runat="server"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableHeaderCell ID="cellCaptionPostalAddress" runat="server" Text="<%$ Resources:, lblPostalAddressRes %>"></asp:TableHeaderCell>
                <asp:TableCell ID="cellPostalAddress" runat="server"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableHeaderCell ID="cellCaptionLanguage" runat="server" Text="<%$ Resources:, lblLanguageRes %>"></asp:TableHeaderCell>
                <asp:TableCell ID="cellLanguage" runat="server"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableHeaderCell ID="cellCaptionCountry" runat="server" Text="<%$ Resources:, lblCountryRes %>"></asp:TableHeaderCell>
                <asp:TableCell ID="cellCountry" runat="server"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableHeaderCell ID="cellCaptionAdmin" runat="server" Text="<%$ Resources:, lblAdminRes %>"></asp:TableHeaderCell>
                <asp:TableCell ID="cellAdmin" runat="server"></asp:TableCell>
            </asp:TableRow>
            </asp:Table>
            <br />
        </form>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderFoot" runat="server"></asp:Content>