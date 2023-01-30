<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" meta:resourcekey="Page"
    CodeBehind="ShowTagComments.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Catalog.ShowTagComments" EnableEventValidation="false"%>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <br />
    <div align="center">
        <form runat="server">
            <p>
                <asp:Label ID="lblNoComments" runat="server" Text="<%$ Resources:, lblNoCommentsRes %>" Font-Italic="True" />
            </p>
            <asp:GridView ID="gvTagComments" runat="server" CssClass="products" GridLines="None"
                AutoGenerateColumns="False" DataKeyNames="commentId, usrId">
                <Columns>
                    <asp:BoundField DataField="usrId" HeaderText="<%$ Resources:, userRes %>" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="commentText" HeaderText="<%$ Resources:, commentTextRes %>" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="commentDate" HeaderText="<%$ Resources:, dateRes %>" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            <asp:Button ID="btnUpdateComment" runat="server" Text="<%$ Resources:, btnUpdateCommentRes %>" 
                                OnClick="gvProducts_btnUpdateCommentClicked"
                                CommandArgument='<%#Eval("commentId" )%>' Visible="false"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:HyperLinkField DataNavigateUrlFields="proId" 
                        DataNavigateUrlFormatString="~/Pages/Catalog/ShowProductByProID.aspx?productId={0}" 
                        Text="<%$ Resources:, lnkProductDetailsRes %>" runat="server"/>
                </Columns>
            </asp:GridView>
        </form>
        <br />
        <div class="previousNextLinks">
            <span class="previousLink">
                <asp:HyperLink ID="lnkPrevious" Text="<%$ Resources:Common, Previous %>" runat="server"
                    Visible="False"></asp:HyperLink>
            </span><span class="nextLink">
                <asp:HyperLink ID="lnkNext" Text="<%$ Resources:Common, Next %>" runat="server" Visible="False"></asp:HyperLink>
            </span>
        </div>
        <br />
        <br />
    </div>
</asp:Content>

