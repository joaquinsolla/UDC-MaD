<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true"
    CodeBehind="Authentication.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.User.Authentication"
    meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
    <div id="Register" align="center">
        <asp:Label ID="lblRegister" runat="server" Font-Bold="True" Text="<%$Resources: , lblRegisterRes %>"></asp:Label>
        &nbsp
        <asp:HyperLink ID="lnkRegister" runat="server" NavigateUrl="~/Pages/User/Register.aspx" Text="<%$ Resources:, lnkRegisterRes %>" ForeColor="#0033CC" />
        <br />
        <br />
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div id="form">
        <form id="AuthenticationForm" method="POST" runat="server">
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclLogin" runat="server" Text="<%$ Resources:, lclLoginRes %>" /></span><span
                        class="entry">
                        <asp:TextBox ID="txtLogin" runat="server" Width="100" Columns="16"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvLogin" runat="server"
                            ControlToValidate="txtLogin" Display="Dynamic" Text="<%$ Resources:Common, mandatoryFieldRes %>"/>
                        <asp:Label ID="lblUserNameError" runat="server" ForeColor="Red" Style="position: relative"
                            Visible="False" Text="<%$ Resources:, lblUserNameErrorRes %>">                        
                        </asp:Label>
                    </span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclPassword" runat="server" Text="<%$ Resources:, lclPasswordRes %>" /></span><span
                        class="entry">
                        <asp:TextBox TextMode="Password" ID="txtPassword" runat="server" Width="100" Columns="16"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvPassword" runat="server"
                            ControlToValidate="txtPassword" Display="Dynamic" Text="<%$ Resources:Common, mandatoryFieldRes %>"/>
                        <asp:Label ID="lblPasswordError" runat="server" ForeColor="Red" Style="position: relative"
                            Visible="False" Text="<%$ Resources:, lblPasswordErrorRes %>">       
                        </asp:Label>
                    </span>
            </div>
            <div class="checkbox">
                <asp:CheckBox ID="checkRememberPassword" runat="server" TextAlign="Left" Text="<%$ Resources:, chkRememeberPasswordRes %>" />
            </div>
            <div class="button">
                <asp:Button ID="btnLogin" runat="server" OnClick="BtnLoginClick" Text="<%$ Resources:, btnLoginRes %>" />
            </div>
            <div align="center">
                <asp:Label ID="lblLoginError" runat="server" ForeColor="Red" Style="position: relative"
                    Visible="False" Text="<%$ Resources:, lblLoginErrorRes %>">                        
                </asp:Label>
            </div>

        </form>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderFoot" runat="server">

        <div id="LoginAsGuest" align="center">
            <br />
            <asp:Label ID="lblLoginAsGuest" runat="server" Font-Italic="True" Text="<%$Resources: , lblLoginAsGuestRes %>"></asp:Label>
            &nbsp
            <asp:HyperLink ID="lnkLoginAsGuest" runat="server" NavigateUrl="~/Pages/MainPage.aspx" Text="<%$ Resources:, lnkLoginAsGuestRes %>" ForeColor="#0033CC" />
            <br />
            <br />
        </div>
</asp:Content>