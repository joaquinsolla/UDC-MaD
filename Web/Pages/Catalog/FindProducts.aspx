<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="FindProducts.aspx.cs" 
    Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.FindProducts" meta:resourcekey="Page" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <br />
    <div id="form">
        <form id="FindForm" method="post" runat="server">
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclIdentifier" runat="server" Text="<%$ Resources:, lclIdentifierRes %>" />
                </span>
                <span class="entry">
                    <asp:TextBox ID="txtIdentifier" runat="server" Width="200px" Columns="16" />
                    <asp:Label CssClass="errorMessage" ID="lblIdentifierError" runat="server" meta:resourcekey="lblIdentifierError" />
                </span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclCategory" runat="server" Text="<%$ Resources:, lclCategoryRes %>" />
                </span>

                <span class="entry">
                    <asp:DropDownList ID="ddlCategory" runat="server" Width="200px" AppendDataBoundItems="true" />
                </span>
            </div>
            <div class="button">
                <asp:Button ID="btnFind" runat="server" meta:resourcekey="btnFind" OnClick="BtnFindClick" Text="<%$ Resources:, search %>"/>
            </div>
        </form>
    </div>
    <br />
    <br />
</asp:Content>

