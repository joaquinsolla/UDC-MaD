<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true"
    CodeBehind="ChangePassword.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.User.ChangePassword"
    meta:resourcekey="Page"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">

    <div align="center">
        <asp:Label ID="lblChangePasswordTitle" runat="server" Text="<%$ Resources:, lblChangePasswordTitleRes %>" Font-Bold="false" Font-Size="Large"></asp:Label>
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

        <form id="ChangePasswordForm" method="post" runat="server">

            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclOldPassword" runat="server" Text="<%$ Resources:, lclOldPasswordRes %>" />
                </span>
                <span class="entry">
                        <asp:TextBox ID="txtOldPassword" TextMode="Password" runat="server" Width="100px" Columns="16"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvOldPassword" runat="server" ControlToValidate="txtOldPassword"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryFieldRes %>"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblOldPasswordError" runat="server" ForeColor="Red" Visible="false" Text="<%$ Resources:, lblOldPasswordError %>">
                        </asp:Label>
                </span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclNewPassword" runat="server" Text="<%$ Resources:, lclNewPasswordRes %>" />
                </span>
                <span class="entry">
                        <asp:TextBox ID="txtNewPassword" TextMode="Password" runat="server" Width="100px" Columns="16"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvNewPassword" runat="server" ControlToValidate="txtNewPassword"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryFieldRes %>"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="cvCreateNewPassword" runat="server" ControlToCompare="txtOldPassword"
                            ControlToValidate="txtNewPassword" Operator="NotEqual" Text="<%$ Resources:, cvCreateNewPassword %>"></asp:CompareValidator>
                </span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclRetypePassword" runat="server" Text="<%$ Resources:, lclRetypePasswordRes %>" />
                </span>
                <span class="entry">
                        <asp:TextBox ID="txtRetypePassword" TextMode="Password" runat="server" Width="100px" Columns="16"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvRetypePassword" runat="server" ControlToValidate="txtRetypePassword"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryFieldRes %>"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="cvPasswordCheck" runat="server" ControlToCompare="txtNewPassword"
                            ControlToValidate="txtRetypePassword" Text="<%$ Resources:, cvPasswordCheck %>"></asp:CompareValidator>
                </span>
            </div>
            <br />
            <br />
            <div class="button">
                <asp:Button ID="btnChangePassword" runat="server" OnClick="BtnChangePasswordClick"
                    Text="<%$ Resources:, btnChangePassword %>" />
                <br />
                <br />
            </div>
        </form>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderFoot" runat="server"></asp:Content>