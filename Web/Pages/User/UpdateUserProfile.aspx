<%@ Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true"
    CodeBehind="UpdateUserProfile.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.User.UpdateUserProfile"
    meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
    <div align="center">
        <asp:Label ID="lblUpdateUserProfileTitle" runat="server" Text="<%$ Resources:, lblUpdateUserProfileTitleRes %>" Font-Bold="false" Font-Size="Large"></asp:Label>
        <br />
        <br />
        <asp:HyperLink ID="lnkGoBackToMyProfile" runat="server"
            NavigateUrl="~/Pages/User/MyProfile.aspx"
            Text="<%$ Resources:, lnkGoBackToMyProfileRes %>" ForeColor="#0033CC" />
        <br />
        <br />
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div id="form">
        <form id="UpdateUserProfileForm" method="post" runat="server">
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclFirstName" runat="server" Text="<%$ Resources:, lclFirstNameRes %>" />
                </span>
                <span class="entry">
                    <asp:TextBox ID="txtFirstName" runat="server" Width="100px" Columns="16"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName"
                        Display="Dynamic" Text="<%$ Resources:Common, mandatoryFieldRes %>" />
                </span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclLastName" runat="server" Text="<%$ Resources:, lclLastNameRes %>" />
                </span>
                <span class="entry">
                    <asp:TextBox ID="txtLastName" runat="server" Width="100px" Columns="16"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName"
                        Display="Dynamic" Text="<%$ Resources:Common, mandatoryFieldRes %>" />
                </span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclEmail" runat="server" Text="<%$ Resources:, lclEmailRes %>" />
                </span>
                <span class="entry">
                    <asp:TextBox ID="txtEmail" runat="server" Width="100px" Columns="16"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                        Display="Dynamic" Text="<%$ Resources:Common, mandatoryFieldRes %>" />
                    <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Text=" Resources:, revEmail"></asp:RegularExpressionValidator>
                </span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclPostalAddress" runat="server" Text="<%$ Resources:, lclPostalAddress %>" />
                </span>
                <span class="entry">
                    <asp:TextBox ID="txtPostalAddress" runat="server" Width="100px" Columns="16"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvPostalAddress" runat="server" ControlToValidate="txtPostalAddress"
                        Display="Dynamic" Text="<%$ Resources:Common, mandatoryFieldRes %>" />
                </span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclLanguage" runat="server" Text="<%$ Resources:, lclLanguage %>" />
                </span>
                <span class="entry">
                    <asp:DropDownList ID="comboLanguage" runat="server" AutoPostBack="true" Width="100px"
                        onselectedindexchanged="ComboLanguageSelectedIndexChanged"></asp:DropDownList>
                </span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclCountry" runat="server" Text="<%$ Resources:, lclCountry %>" />
                </span>
                <span class="entry">
                    <asp:DropDownList ID="comboCountry" runat="server" Width="100px"></asp:DropDownList>
                </span>
            </div>
             <div class="field" align="center">
                 <asp:Label ID="txtAdminTitle" Font-Bold="true" runat="server" Text="<%$ Resources:, txtAdminRes %>"></asp:Label>
                 <asp:Label ID="txtAdmin" runat="server"></asp:Label>
                 <br />
                 <br />
             </div>
            <div class="button">
                <asp:Button ID="btnUpdate" runat="server" OnClick="BtnUpdateClick" Text="<%$ Resources:, btnUpdate %>" />
                <br />
                <br />
            </div>
        </form>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderFoot" runat="server"></asp:Content>