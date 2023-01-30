<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true"
    CodeBehind="Register.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.User.Register"
    meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">

    <div align="center">
        <asp:Label ID="lblRegisterTitle" runat="server" Text="<%$ Resources:, lblRegisterTitleRes %>" Font-Bold="False" Font-Size="Large"></asp:Label>
        <br />
        <br />
    </div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain"
    runat="server">
    <div id="form">
        <form id="RegisterForm" method="post" runat="server">

            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclUserName" runat="server" Text="<%$ Resources:, lclUserNameRes %>" />
                </span><span
                        class="entry">
                        <asp:TextBox ID="txtLogin" runat="server" Width="100px" Columns="16"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txtLogin"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryFieldRes %>"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblUsernameNotAvaliable" runat="server" ForeColor="Red" Style="position: relative"
                            Visible="False" Text="<%$ Resources:, lblUsernameNotAvaliableRes %>"></asp:Label></span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclPassword" runat="server" Text="<%$ Resources:, lclPasswordRes %>" /></span><span
                        class="entry">
                        <asp:TextBox TextMode="Password" ID="txtPassword" runat="server" Width="100px" Columns="16"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryFieldRes %>"></asp:RequiredFieldValidator></span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclRetypePassword" runat="server" Text="<%$ Resources:, lclRetypePasswordRes %>" /></span><span
                        class="entry">
                        <asp:TextBox TextMode="Password" ID="txtRetypePassword" runat="server" Width="100px" Columns="16"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvRetypePassword" runat="server" ControlToValidate="txtRetypePassword"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryFieldRes %>"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="cvPasswordCheck" runat="server" ControlToCompare="txtPassword"
                            ControlToValidate="txtRetypePassword" Text="<%$ Resources:, cvPasswordCheckRes %>"></asp:CompareValidator></span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclFirstName" runat="server" Text="<%$ Resources:, lclFirstNameRes %>" /></span><span
                        class="entry">
                        <asp:TextBox ID="txtFirstName" runat="server" Width="100px" Columns="16"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryFieldRes %>"></asp:RequiredFieldValidator></span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclLastName" runat="server" Text="<%$ Resources:, lclLastNameRes %>"/></span><span
                        class="entry">
                        <asp:TextBox ID="txtLastName" runat="server" Width="100px" Columns="16"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvSurname" runat="server" ControlToValidate="txtLastName"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryFieldRes %>"></asp:RequiredFieldValidator></span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclEmail" runat="server" Text="<%$ Resources:, lclEmailRes %>" /></span><span
                        class="entry">
                        <asp:TextBox ID="txtEmail" runat="server" Width="100px" Columns="16"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryFieldRes %>"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                            Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            Text="<%$ Resources:, revEmailRes %>"></asp:RegularExpressionValidator></span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclPostalAddress" runat="server" Text="<%$ Resources:, lclPostalAddressRes %>"/></span><span
                        class="entry">
                        <asp:TextBox ID="txtPostalAddress" runat="server" Width="100px" Columns="16"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvPostalAddress" runat="server" ControlToValidate="txtPostalAddress"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryFieldRes %>"></asp:RequiredFieldValidator></span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclLanguage" runat="server" Text="<%$ Resources:, lclLanguageRes %>" /></span><span
                        class="entry">
                        <asp:DropDownList ID="comboLanguage" runat="server" AutoPostBack="True"
                            Width="100px"  OnSelectedIndexChanged="ComboLanguageSelectedIndexChanged">
                        </asp:DropDownList></span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclCountry" runat="server" Text="<%$ Resources:, lclCountryRes %>" /></span><span
                        class="entry">
                        <asp:DropDownList ID="comboCountry" runat="server" Width="100px">
                        </asp:DropDownList></span>
            </div>
            <div class="button">
                <asp:Button ID="btnRegister" runat="server" OnClick="BtnRegisterClick" Text="<%$ Resources:, btnRegisterRes %>" />
                <br />
                <br />
            </div>
        </form>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderFoot"
    runat="server">
</asp:Content>