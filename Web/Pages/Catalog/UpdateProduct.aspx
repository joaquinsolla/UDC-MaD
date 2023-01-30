<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="UpdateProduct.aspx.cs" 
    Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.UpdateProduct" meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">

    <div align="center">
        <asp:Label ID="lblUpdateProduct" runat="server" Text="<%$ Resources:, lblUpdateProductRes %>" Font-Bold="False" Font-Size="Large"></asp:Label>
        <br />
        <br />
    </div>

</asp:Content>

<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div id="form">
        <form id="UpdateProductDetailsForm" method="post" runat="server">
            <div style="float:left; width: 50%;">

                <div align="center">
                    <asp:Label ID="lblGeneralInformation" runat="server" Text="<%$ Resources:, lblGeneralInformationRes %>" Font-Bold="False" Font-Size="Medium"></asp:Label>
                    <br />
                    <br />
                </div>

                <div class="field">
                    <span class="label">
                        <asp:Localize ID="lclProductName" runat="server" Text="<%$ Resources:, lclProductNameRes %>" />
                    </span>
                    <span class="entry">
                        <asp:TextBox ID="txtProductName" runat="server" Width="100px" Columns="16"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvProductName" runat="server" ControlToValidate="txtProductName"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryFieldRes %>" />
                    </span>
                </div>
                <div class="field">
                    <span class="label">
                        <asp:Localize ID="lclProductPrice" runat="server" Text="<%$ Resources:, lclProductPriceRes %>" />
                    </span>
                    <span class="entry">
                        <asp:TextBox ID="txtProductPrice" runat="server" Width="100px" Columns="16"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvProductPrice" runat="server" ControlToValidate="txtProductPrice"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryFieldRes %>" />
                    </span>
                </div>
                <div class="field">
                    <span class="label">
                        <asp:Localize ID="lclProductStock" runat="server" Text="<%$ Resources:, lclProductStockRes %>" />
                    </span>
                    <span class="entry">
                        <asp:TextBox ID="txtProductStock" runat="server" Width="100px" Columns="16"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvProductStock" runat="server" ControlToValidate="txtProductStock"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryFieldRes %>" />
                    </span>
                </div>

                <div class="field">
                    <span class="label">
                        <asp:Localize ID="lclProductCategory" runat="server" Text="<%$ Resources:, lclProductCategoryRes %>" />
                    </span>
                    <span class="entry">
                        <asp:TextBox ID="txtCategory" runat="server" Width="100px" Columns="16" Enabled="false" ></asp:TextBox>
                    </span>
                </div>

                <div class="field">
                    <span class="label">
                        <asp:Localize ID="lclProductReleaseDate" runat="server" Text="<%$ Resources:, lclProductReleaseDateRes %>" />
                    </span><span class="entry">
                        <div align="left">

                            <asp:Calendar ID="cdrProductReleaseDate" runat="server"></asp:Calendar>
                            </div></span>
                </div>
            </div>

            <div style="float:right; width: 50%;">

            <div align="center">
                <asp:Label ID="lblBookInformation" runat="server" Text="<%$ Resources:, lblBookInformationRes %>" Font-Bold="False" Font-Size="Medium" Visible="false" ></asp:Label>
                <asp:Label ID="lblMusicInformation" runat="server" Text="<%$ Resources:, lblMusicInformationRes %>" Font-Bold="False" Font-Size="Medium" Visible="false"></asp:Label>
                <asp:Label ID="lblFilmInformation" runat="server" Text="<%$ Resources:, lblFilmInformationRes %>" Font-Bold="False" Font-Size="Medium" Visible="false"></asp:Label>
                <br />
                <br />
            </div>
                
            <asp:Panel Visible="false" ID="UpdateBookPanel" runat="server">
                <div class="field">
                    <span class="label">
                        <asp:Localize ID="lclBookISBN" runat="server" Text="ISBN" />
                    </span>
                    <span class="entry">
                        <asp:TextBox ID="txtBookISBN" runat="server" Width="120px" Columns="16"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvBookISBN" runat="server" ControlToValidate="txtBookISBN"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryFieldRes %>" />
                    </span>
                </div>
                <div class="field">
                    <span class="label">
                        <asp:Localize ID="lclBookEditorial" runat="server" Text="<%$ Resources:, lclBookEditorialRes %>" />
                    </span>
                    <span class="entry">
                        <asp:TextBox ID="txtBookEditorial" runat="server" Width="100px" Columns="16"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvBookEditorial" runat="server" ControlToValidate="txtBookEditorial"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryFieldRes %>" />
                    </span>
                </div>
                <div class="field">
                    <span class="label">
                        <asp:Localize ID="lclBookEdition" runat="server" Text="<%$ Resources:, lclBookEditionRes %>" />
                    </span>
                    <span class="entry">
                        <asp:TextBox ID="txtBookEdition" runat="server" Width="100px" Columns="16"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvBookEdition" runat="server" ControlToValidate="txtBookEdition"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryFieldRes %>" />
                    </span>
                </div>
                <div class="field">
                    <span class="label">
                        <asp:Localize ID="lclBookPages" runat="server" Text="<%$ Resources:, lclBookPagesRes %>" />
                    </span>
                    <span class="entry">
                        <asp:TextBox ID="txtBookPages" runat="server" Width="100px" Columns="16"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvBookPages" runat="server" ControlToValidate="txtBookPages"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryFieldRes %>" />
                    </span>
                </div>

                <div class="field">
                    <span class="label">
                        <asp:Localize ID="lclBookReleaseDate" runat="server" Text="<%$ Resources:, lclPublicationDateRes %>" />
                    </span><span class="entry">
                        <div align="left">
                            <asp:Calendar ID="cdrBookReleaseDate" runat="server"></asp:Calendar>
                        </div></span>
                </div>
            </asp:Panel>

            <asp:Panel Visible="false" ID="UpdateMusicPanel" runat="server">
                <div class="field">
                    <span class="label">
                        <asp:Localize ID="lclMusicArtist" runat="server" Text="<%$ Resources:, lclMusicArtistRes %>" />
                    </span>
                    <span class="entry">
                        <asp:TextBox ID="txtMusicArtist" runat="server" Width="100px" Columns="16"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvMusicArtist" runat="server" ControlToValidate="txtMusicArtist"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryFieldRes %>" />
                    </span>
                </div>
                <div class="field">
                    <span class="label">
                        <asp:Localize ID="lclMusicAlbum" runat="server" Text="<%$ Resources:, lclMusicAlbumRes %>" />
                    </span>
                    <span class="entry">
                        <asp:TextBox ID="txtMusicAlbum" runat="server" Width="100px" Columns="16"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvMusicAlbum" runat="server" ControlToValidate="txtMusicAlbum"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryFieldRes %>" />
                    </span>
                </div>
                <div class="field">
                    <span class="label">
                        <asp:Localize ID="lclMusicDurationMins" runat="server" Text="<%$ Resources:, lclDurationMinsRes %>" />
                    </span>
                    <span class="entry">
                        <asp:TextBox ID="txtMusicDurationMins" runat="server" Width="100px" Columns="16"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvMusicDurationMins" runat="server" ControlToValidate="txtMusicDurationMins"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryFieldRes %>" />
                    </span>
                </div>
                <div class="field">
                    <span class="label">
                        <asp:Localize ID="lclMusicSongs" runat="server" Text="<%$ Resources:, lclMusicSongsRes %>" />
                    </span>
                    <span class="entry">
                        <asp:TextBox ID="txtMusicSongs" runat="server" Width="100px" Columns="16"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvMusicSongs" runat="server" ControlToValidate="txtMusicSongs"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryFieldRes %>" />
                    </span>
                </div>
                
                <div class="field">
                    <span class="label">
                        <asp:Localize ID="lclMusicReleaseDate" runat="server" Text="<%$ Resources:, lclPublicationDateRes %>" />
                    </span><span class="entry">
                        <div align="left">

                            <asp:Calendar ID="cdrMusicReleaseDate" runat="server"></asp:Calendar>
                            </div></span>
                </div>
            </asp:Panel>

            <asp:Panel Visible="false" ID="UpdateFilmPanel" runat="server">
                <div class="field">
                    <span class="label">
                        <asp:Localize ID="lclFilmDirector" runat="server" Text="<%$ Resources:, lclFilmDirectorRes %>" />
                    </span>
                    <span class="entry">
                        <asp:TextBox ID="txtFilmDirector" runat="server" Width="100px" Columns="16"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvFilmDirector" runat="server" ControlToValidate="txtFilmDirector"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryFieldRes %>" />
                    </span>
                </div>
                <div class="field">
                    <span class="label">
                        <asp:Localize ID="lclFilmGenre" runat="server" Text="<%$ Resources:, lclFilmGenreRes %>" />
                    </span>
                    <span class="entry">
                        <asp:TextBox ID="txtFilmGenre" runat="server" Width="100px" Columns="16"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvFilmGenre" runat="server" ControlToValidate="txtFilmGenre"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryFieldRes %>" />
                    </span>
                </div>
                <div class="field">
                    <span class="label">
                        <asp:Localize ID="lclFilmDurationMins" runat="server" Text="<%$ Resources:, lclDurationMinsRes %>" />
                    </span>
                    <span class="entry">
                        <asp:TextBox ID="txtFilmDurationMins" runat="server" Width="100px" Columns="16"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvFilmDurationMins" runat="server" ControlToValidate="txtFilmDurationMins"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryFieldRes %>" />
                    </span>
                </div>
                <div class="field">
                    <span class="label">
                        <asp:Localize ID="lclFilmRating" runat="server" Text="<%$ Resources:, lclFilmRatingRes %>" />
                    </span>
                    <span class="entry">
                        <asp:TextBox ID="txtFilmRating" runat="server" Width="100px" Columns="16"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvFilmRating" runat="server" ControlToValidate="txtFilmRating"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryFieldRes %>" />
                    </span>
                </div>

                <div class="field">
                    <span class="label">
                        <asp:Localize ID="lclFilmReleaseDate" runat="server" Text="<%$ Resources:, lclPublicationDateRes %>" />
                    </span><span class="entry">
                        <div align="left">

                            <asp:Calendar ID="cdrFilmReleaseDate" runat="server"></asp:Calendar>
                            </div></span>
                </div>

            </asp:Panel>
            </div>


            <div class="button">
                <asp:Button ID="btnUpdate" runat="server" OnClick="BtnUpdateClick" Text="<%$ Resources:, btnUpdate %>" />
                <br />
                <br />
                <asp:Label ID="lblInternalError" runat="server" Text="<%$ Resources:Common, lblInternalErrorRes %>" Font-Italic="True" ForeColor="Red" Visible="false" ></asp:Label>
            </div>
        </form>
    </div>
    <br />
    <br />
</asp:Content>