<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true"
    CodeBehind="TagCloud.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Catalog.TagCloud" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <form runat="server">
        <br />
        <br />
        <div class="field">
            <span class="label">
                <asp:Localize ID="lclSelectTag" runat="server" Text="<%$ Resources:, lclSelectTagRes %>" />
            </span>
            <span class="entry">
                <asp:DropDownList ID="ddlTags" runat="server" Width="200px" AppendDataBoundItems="true" />
            </span>
        </div>
        <br />
        <div class="button">
        <asp:Button ID="btnFind" runat="server" meta:resourcekey="btnFind" OnClick="BtnFindClick" 
            Text="<%$ Resources:, btnSearchRes %>"/>
        </div>
        <br />
        <br />
        <br />
    </form> 
</asp:Content>

