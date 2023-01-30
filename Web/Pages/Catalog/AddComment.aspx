<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="AddComment.aspx.cs" 
    Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Catalog.AddComment" meta:resourcekey="Page" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
    <div align="center">
        <asp:Label ID="lblAddCommentTitle" runat="server" Text="<%$ Resources:, lblCommentTitleRes %>" Font-Bold="False" Font-Size="Large"></asp:Label>
        <br />
        <br />
    </div>
</asp:Content>


<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
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
                        <asp:Localize ID="lclAddTags" runat="server" Text="<%$ Resources:, lclAddTagsRes %>" />
                    </span>
                    <span class="entry">
                        <asp:TextBox ID="txtAddTag" runat="server" Width="200px" Columns="16" />
                    </span>
                </div> 

            </div>
 
            <div class="field">
                <asp:Label ID="lblInternalError" runat="server" Text="<%$ Resources:Common, lblInternalErrorRes %>" Font-Italic="True" ForeColor="Red" Visible="false" ></asp:Label>
                <br />
                <asp:Button ID="btnAddComment" runat="server" Text="<%$ Resources:, btnAddCommentRes %>" BackColor="#1F7292" Font-Bold="True" ForeColor="White" OnClick="btnAddComment_Click"/>
                <br />
                <br />
            </div>

        </form>
    </div>
</asp:Content>
