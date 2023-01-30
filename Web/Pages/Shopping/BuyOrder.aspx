<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="BuyOrder.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Shopping.BuyOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">

    <div align="center">
        <asp:Label ID="lblBuyOrderTitle" runat="server" Text="<%$ Resources:, lblBuyOrderTitleRes %>" Font-Bold="False" Font-Size="Large"></asp:Label>
        <br />
        <br />
    </div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">

    <div align="center">
        <form runat="server">

            <asp:Label ID="lblBankCard" runat="server" Text="<%$ Resources:, lblBankCardRes %>" Font-Bold="True" Font-Size="Small" ForeColor="#333333"></asp:Label>
            <br />
            <br />
            <asp:Label ID="lblChooseOne1" runat="server" Text="<%$ Resources: Common, lblChooseOneRes %>" ></asp:Label>
            <asp:Label ID="spaces1" runat="server" >&nbsp&nbsp</asp:Label>
            <asp:DropDownList ID="ddlBankCards" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBankCards_SelectedIndexChanged" ></asp:DropDownList>
            <asp:Label ID="spaces2" runat="server" >&nbsp&nbsp&nbsp</asp:Label>
            <asp:Label ID="lblOr1" runat="server" Text="<%$ Resources: Common, lblOrRes %>"></asp:Label>
            <asp:Label ID="spaces3" runat="server" >&nbsp&nbsp&nbsp</asp:Label>
            <asp:HyperLink ID="lnkAddBankCard" runat="server"
                        NavigateUrl="~/Pages/Shopping/AddBankCard.aspx"
                        Text="<%$ Resources:, lnkAddBankCardRes %>" ForeColor="#0033CC" />
            <br />
            <br />
            <asp:Label ID="lblBankCardMandatory" runat="server" Visible="false" Text="<%$ Resources: , lblBankCardMandatoryRes %>"></asp:Label>
            <br />
            <hr />
            <asp:Label ID="lblAddress" runat="server" Text="<%$ Resources:, lblAddressRes %>" Font-Bold="True" Font-Size="Small" ForeColor="#333333"></asp:Label>
            <br />
            <br />
            <asp:Label ID="lblChooseOne2" runat="server" Text="<%$ Resources:, lblYourAddress %>" ></asp:Label>
            <asp:Label ID="lblUserAddress" runat="server" Font-Italic="true"></asp:Label>
            <br />
            <br />
            <asp:Label ID="lblOr2" runat="server" Text="<%$ Resources: Common, lblOrRes %>"></asp:Label>
            <br />
            <br />
            <asp:CheckBox ID="ckbCustomAddress" runat="server" Text="<%$ Resources:, ckbCustomAddressRes %>"/>
            <br />
            <asp:TextBox ID="txtCustomAddress" runat="server" ></asp:TextBox>
            <br />
            <asp:Label ID="lblCustomAddressMandatory" runat="server" Visible="false" Text="<%$ Resources: Common, mandatoryFieldIfPickedRes %>"></asp:Label>
            <br />
            <hr />
            <asp:Label ID="lblOrderDescription" runat="server" Text="<%$ Resources:, lblOrderDescriptionRes %>" Font-Bold="True" Font-Size="Small" ForeColor="#333333"></asp:Label>
            <br />
            <br />
            <asp:TextBox ID="txtOrderDescription" runat="server" ></asp:TextBox>
            <br />
            <asp:Label ID="lblOrderDescriptionMandatory" runat="server" Visible="false" Text="<%$ Resources: Common, mandatoryFieldRes %>"></asp:Label>
            <br />
            <hr />
            <br />
            <asp:Label ID="lblTotalToPay" runat="server" Text="<%$ Resources:, lblTotalToPayRes %>" ></asp:Label>
            <asp:Label ID="lblTotalPriceNumber" runat="server" Font-Bold="True"></asp:Label>
            <br />
            <asp:Label ID="lblInternalError" runat="server" Text="<%$ Resources:Common, lblInternalErrorRes %>" Font-Italic="True" ForeColor="Red" Visible="false" ></asp:Label>
            <br />
            <asp:Button ID="btnConfirmOrder" runat="server" Text="<%$ Resources:, btnConfirmOrderRes %>" BackColor="#1F7292" Font-Bold="True" ForeColor="White" OnClick="btnConfirmOrder_Click" />
            <br />
            <br />
        </form>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderFoot" runat="server">
</asp:Content>
