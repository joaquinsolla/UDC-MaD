<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="OrderDetails.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.User.OrderDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
    <div align="center">
        <asp:Label ID="lblUserOrderDetailsTitle" runat="server" Text="<%$ Resources:, lblUserOrderDetailsTitleRes %>" Font-Bold="false" Font-Size="Large"></asp:Label>
        <br />
        <br />
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">

    <div align="center">
        <form runat="server">
            <asp:Table CssClass="orderDetails" ID="TableOrderInfo" runat="server">
                <asp:TableRow runat="server">
                    <asp:TableHeaderCell ID="cellCaptionOrderId" runat="server" Text="<%$ Resources:, lblOrderIdTitleRes %>"></asp:TableHeaderCell>
                    <asp:TableCell ID="cellOrderId" runat="server"></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableHeaderCell ID="cellCaptionOrderDate" runat="server" Text="<%$ Resources:, lblOrderDateTitleRes %>"></asp:TableHeaderCell>
                    <asp:TableCell ID="cellOrderDate" runat="server"></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableHeaderCell ID="cellCaptionOrderBankCardPAN" runat="server" Text="<%$ Resources:, lblOrderBankCardPANTitleRes %>"></asp:TableHeaderCell>
                    <asp:TableCell ID="cellOrderBankCardPAN" runat="server"></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableHeaderCell ID="cellCaptionOrderPostalAddress" runat="server" Text="<%$ Resources:, lblOrderPostalAddressTitleRes %>"></asp:TableHeaderCell>
                    <asp:TableCell ID="cellOrderPostalAddress" runat="server"></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableHeaderCell ID="cellCaptionOrderValue" runat="server" Text="<%$ Resources:, lblOrderValueTitleRes %>"></asp:TableHeaderCell>
                    <asp:TableCell ID="cellOrderValue" runat="server"></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableHeaderCell ID="cellCaptionOrderDescription" runat="server" Text="<%$ Resources:, lblOrderDescriptionTitleRes %>"></asp:TableHeaderCell>
                    <asp:TableCell ID="cellOrderDescription" runat="server"></asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </form>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderFoot" runat="server">
    <br />
</asp:Content>