<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="AddBankCard.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.User.AddBankCard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
    <div align="center">
        <asp:Label ID="lblAddBankCardTitle" runat="server" Text="<%$ Resources:, lblAddBankCardTitleRes %>" Font-Bold="False" Font-Size="Large"></asp:Label>
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
     <div align="center">
        <form runat="server" method="post">
    
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclCardType" runat="server" Text="<%$ Resources:, lclCardTypeRes %>" /></span><span
                        class="entry">
                        <asp:DropDownList ID="ddlCardType" runat="server" AutoPostBack="True"
                            Width="100px" OnSelectedIndexChanged="ddlCardType_SelectedIndexChanged">
                        </asp:DropDownList></span>
            </div>

            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclCardPAN" runat="server" Text="<%$ Resources:, lclCardPANRes %>" />
                </span><span
                        class="entry">
                        <asp:TextBox ID="txtCardPAN" runat="server" Width="120px" Columns="16"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvCardPAN" runat="server" ControlToValidate="txtCardPAN"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryFieldRes %>"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator runat="server" ID="revCardPANLength" Display = "Dynamic" 
                            ControlToValidate = "txtCardPAN" ValidationExpression = "^[\s\S]{16,16}$"
                            Text="<%$ Resources:, revCardPANLengthRes %>"></asp:RegularExpressionValidator>
                        <asp:Label ID="lblCardAlreadyOwned" runat="server" ForeColor="Red" Style="position: relative"
                            Visible="False" Text="<%$ Resources:, lblCardAlreadyOwnedRes %>"></asp:Label></span>
            </div>

            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclCardCVV" runat="server" Text="<%$ Resources:, lclCardCVVRes %>" />
                </span><span
                        class="entry">
                        <asp:TextBox ID="txtCardCVV" runat="server" Width="120" Columns="3"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvCardCVV" runat="server" ControlToValidate="txtCardCVV"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryFieldRes %>"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator runat="server" ID="revCardCVVLength" Display = "Dynamic" 
                            ControlToValidate = "txtCardCVV" ValidationExpression = "^[\s\S]{3,3}$"
                            Text="<%$ Resources:, revCardCVVLengthRes %>"></asp:RegularExpressionValidator></span>
            </div>

            <div class="field">

                <span class="label">
                    <asp:Localize ID="lclCardExpirationDate" runat="server" Text="<%$ Resources:, lclCardExpirationDateRes %>" />
                </span><span class="entry">
                    <div align="left">

                        <asp:Calendar ID="cdrCardExpirationDate" runat="server"></asp:Calendar>
                        <asp:Label ID="lblCardExpirationDateMandatory" runat="server" Style="position: relative"
                            Visible="False" Text="<%$ Resources:Common, mandatoryFieldRes %>"></asp:Label></div>
                       </span>

            </div>

            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclCardDefault" runat="server" Text="<%$ Resources:, lclCardDefaultRes %>" />
                </span><span
                        class="entry">                  
                                    <asp:CheckBox ID="ckbCardDefault" runat="server" Text="<%$ Resources:, ckbCardDefaultRes %>"/></span>
            </div>  

            <div class="field">
                <asp:Label ID="lblInternalError" runat="server" Text="<%$ Resources:Common, lblInternalErrorRes %>" Font-Italic="True" ForeColor="Red" Visible="false" ></asp:Label>
                <br />
                <asp:Button ID="btnAddCard" runat="server" Text="<%$ Resources:, btnAddCardRes %>" BackColor="#1F7292" Font-Bold="True" ForeColor="White" OnClick="btnAddCard_Click"/>
                <br />
                <br />
            </div>

        </form>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderFoot" runat="server"></asp:Content>