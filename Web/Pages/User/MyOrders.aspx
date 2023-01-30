<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="MyOrders.aspx.cs"
        Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.User.MyOrders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
    <div align="center">
        <asp:Label ID="lblMyOrdersTitle" runat="server" Text="<%$ Resources:, lblMyOrdersTitleRes %>" Font-Bold="false" Font-Size="Large"></asp:Label>
        <br />
        <br />
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div align="center">
        <form runat="server">

            <asp:Label ID="lblNoOrders" Text="<%$ Resources:, lblNoOrdersRes %>" runat="server"></asp:Label>
            <asp:GridView ID="gvOrders" runat="server" CssClass="orders" GridLines="None" AutoGenerateColumns="false" DataKeyNames="orderId">
                <Columns>
                    <asp:BoundField DataField="orderId" HeaderText="<%$ Resources:, orderIdRes %>" ItemStyle-HorizontalAlign="Left" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="orderDate" HeaderText="<%$ Resources:, orderDateRes %>" ItemStyle-HorizontalAlign="Center" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="orderValue" HeaderText="<%$ Resources:, orderValueRes %>" ItemStyle-HorizontalAlign="Right" DataFormatString='{0:#,##0.00 €;(#,##0.00 €);0 €}' />
                    <asp:TemplateField>
                        <ItemTemplate>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:HyperLinkField DataNavigateUrlFields="orderId"
                        DataNavigateUrlFormatString="~/Pages/User/OrderDetails.aspx?orderId={0}"
                        Text="<%$ Resources:, lnkOrderDetailsRes %>" runat="server" />
                </Columns>
            </asp:GridView>

        </form>
    </div>
    <br />
    <!-- "Previous" and "Next" links. -->
    <div class="previousNextLinks" align="center">
        <span class="previousLink">
            <asp:HyperLink ID="lnkPrevious" Text="<%$ Resources:Common, Previous %>" runat="server"
                Visible="False"></asp:HyperLink>
            &nbsp&nbsp
        </span><span class="nextLink">
            <asp:HyperLink ID="lnkNext" Text="<%$ Resources:Common, Next %>" runat="server" 
                Visible="False"></asp:HyperLink>
        </span>
    </div>
    <br />
    <br />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderFoot" runat="server"></asp:Content>