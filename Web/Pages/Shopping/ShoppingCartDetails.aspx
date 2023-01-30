<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" 
    CodeBehind="ShoppingCartDetails.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Shopping.ShoppingCartDetails" meta:resourcekey="Page" 
    EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">

    <div align="center">
        <asp:Label ID="lblShoppingCartTitle" runat="server" Text="<%$ Resources:, lblShoppingCartTitleRes %>" Font-Bold="False" Font-Size="Large"></asp:Label>
        <br />
        <br />
    </div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">

    <div align="center">
        <form runat="server">
            <asp:GridView ID="gvwShoppingCartDetails" runat="server" AutoGenerateColumns="False" GridLines="Horizontal" >
                <Columns>

                    <asp:BoundField runat="server" DataField="product.proName" HeaderText="<%$ Resources:, bfdProductRes %>" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"/>
                    
                    <asp:TemplateField>
                        <ItemTemplate>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            <asp:Button ID="btnRemoveOne" runat="server" Text="-" OnClick="gvwShoppingCartDetails_btnRemoveOneClicked"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField runat="server" DataField="quantity" HeaderText="<%$ Resources:, bfdQuantityRes %>" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"/>
                    
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnAddOne" runat="server" Text="+"  OnClick="gvwShoppingCartDetails_btnAddOneClicked"/>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                        </ItemTemplate>
                    </asp:TemplateField>

                   <asp:TemplateField HeaderText="<%$ Resources:, ckbGiftRes %>" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Button ID="btnGift" runat="server" OnClick="gvwShoppingCartDetails_btnGiftClicked"/>              
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField>
                        <ItemTemplate>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField runat="server" DataField="linePrice" HeaderText="<%$ Resources:, bfdLinePriceRes %>" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" DataFormatString='{0:#,##0.00 €;(#,##0.00 €);0 €}' />

                    <asp:TemplateField>
                        <ItemTemplate>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            <asp:Button ID="btnRemove" runat="server" Text="<%$ Resources:, btnRemoveRes %>" OnClick="gvwShoppingCartDetails_btnRemoveClicked"/>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>

            <asp:Label ID="lblInternalError" runat="server" Text="<%$ Resources:Common, lblInternalErrorRes %>" Font-Italic="True" ForeColor="Red" Visible="false" ></asp:Label>
            <br />
            <br />
            <asp:Label ID="lblTotalPriceTitle" runat="server" Text="<%$ Resources:, lblTotalPriceRes %>" ></asp:Label>
            <asp:Label ID="lblTotalPriceNumber" runat="server" Font-Bold="True"></asp:Label>
            <asp:Label ID="lblEmptyShoppingCart" runat="server" Text="<%$ Resources:, lblEmptyShoppingCartRes %>" Font-Italic="True" ></asp:Label>
            <br />
            <br />
            <asp:Button ID="btnBuyOrder" runat="server" Text="<%$ Resources:, btnBuyOrderRes %>" BackColor="#1F7292" Font-Bold="True" ForeColor="White" OnClick="btnBuyOrder_Click" />
            <br />
            <br />
        </form>

    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderFoot" runat="server">

    <div align="center">

    </div>

</asp:Content>