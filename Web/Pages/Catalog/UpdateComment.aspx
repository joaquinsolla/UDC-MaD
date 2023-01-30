<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="UpdateComment.aspx.cs" 
    Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Catalog.UpdateComment" meta:resourcekey="Page" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <br />
    <div align="center">
        <form runat="server" method="post">
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclCommentText" runat="server" Text="<%$ Resources:, lclCommentTextRes %>" />
                </span>
                <span class="entry">
                    <asp:TextBox ID="txtCommentText" runat="server" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvCommentText" runat="server" ControlToValidate="txtCommentText"
                        Display="Dynamic" Text="<%$ Resources:Common, mandatoryFieldRes %>" ></asp:RequiredFieldValidator>              
                </span>
            </div>

            <div class="field">
                <div class="field">
                    <span class="label">
                        <asp:Localize ID="lclNewTags" runat="server" Text="<%$ Resources:, lclNewTagsRes %>" />
                    </span>
                    <span class="entry">
                        <asp:TextBox ID="txtNewTag" runat="server" Width="200px" Columns="16" />
                    </span>
                </div> 
            </div>
 
            <div class="field">
                <asp:Label ID="lblInternalError" runat="server" Text="<%$ Resources:Common, lblInternalErrorRes %>" Font-Italic="True" ForeColor="Red" Visible="false" ></asp:Label>
                <br />
                <asp:Button ID="btnUpdateComment" runat="server" Text="<%$ Resources:, btnUpdateCommentRes %>" BackColor="#1F7292" Font-Bold="True" ForeColor="White" OnClick="btnUpdateComment_Click"/>
                <br />
                <br />
            </div>
        </form>
    </div>
    <br />
    <br />
</asp:Content>
