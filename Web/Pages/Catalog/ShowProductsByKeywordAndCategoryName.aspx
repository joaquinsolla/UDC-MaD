<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" 
    CodeBehind="ShowProductsByKeywordAndCategoryName.aspx.cs" meta:resourcekey="Page" 
    Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Catalog.ShowProductsByKeywordAndCategoryName" 
    EnableEventValidation="false" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div align="center">
        <form runat="server">
            <asp:Label ID="lblNoProducts" Text="<%$ Resources:, lblNoProductsRes %>" runat="server"></asp:Label>
            <asp:GridView ID="gvProducts" runat="server" CssClass="products" GridLines="None"
                AutoGenerateColumns="False" DataKeyNames="proId">
                <Columns>
                    <asp:BoundField DataField="proName" HeaderText="<%$ Resources:, proNameRes %>"/>
                    <asp:TemplateField>
                        <ItemTemplate>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="proCatName" HeaderText="<%$ Resources:, proCatNameRes %>" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="proReleaseDate" HeaderText="<%$ Resources:, proReleaseDateRes %>" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="proPrice" HeaderText="<%$ Resources:, proPriceRes %>" DataFormatString='{0:#,##0.00 €;(#,##0.00 €);0 €}'/>
                    <asp:TemplateField>
                        <ItemTemplate>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:HyperLinkField DataNavigateUrlFields="proId" 
                        DataNavigateUrlFormatString="~/Pages/Catalog/ShowProductByProID.aspx?productId={0}" 
                        Text="<%$ Resources:, lnkProductDetailsRes %>" runat="server"/>
                    <asp:TemplateField>
                        <ItemTemplate>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            <asp:Button ID="btnAddToCart" runat="server" Text="<%$ Resources:, btnAddToCartRes %>" OnClick="gvProducts_btnAddToCartClicked"
                                CommandArgument='<%#Eval("proId" )%>'/>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <br />
            <asp:Label ID="lblProductAdded" runat="server" Text="<%$ Resources: , lblProductAddedRes %>" Font-Italic="True" Visible="false"></asp:Label>
            <asp:Label ID="lblProductAddedName" runat="server" Font-Bold="True" Visible="false"></asp:Label>
            <br />
            <asp:Label ID="lblOutOfStock" runat="server" Text="<%$ Resources: , lblOutOfStockRes %>" Font-Italic="True" ForeColor="Red" Visible="false"></asp:Label>
            <br />
            <asp:Label ID="lblInternalError" runat="server" Text="<%$ Resources: Common, lblInternalErrorRes %>" Font-Italic="True" ForeColor="Red" Visible="false"></asp:Label>
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

