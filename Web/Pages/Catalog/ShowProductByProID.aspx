<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" 
    CodeBehind="ShowProductByProID.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.ShowProductByProID" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">

    <div align="center">
        <asp:Label ID="lblProductDetails" runat="server" Text="<%$ Resources:, lblProductDetailsRes %>" Font-Bold="False" Font-Size="Large"></asp:Label>
        <br />
        <br />
    </div>

</asp:Content>

<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div align="center">
        <form runat="server">
            <asp:Table CssClass="productDetails" ID="TableProductInfo" runat="server">
                <asp:TableRow runat="server">
                    <asp:TableHeaderCell ID="cellCaptionProductName" runat="server" Text="<%$ Resources:, proNameRes %>"></asp:TableHeaderCell>
                    <asp:TableCell ID="cellProductName" runat="server"></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableHeaderCell ID="cellCaptionCategoryName" runat="server" Text="<%$ Resources:, proCatNameRes %>"></asp:TableHeaderCell>
                    <asp:TableCell ID="cellCategoryName" runat="server"></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableHeaderCell ID="cellCaptionProductPrice" runat="server" Text="<%$ Resources:, proPriceRes %>"></asp:TableHeaderCell>
                    <asp:TableCell ID="cellProductPrice" runat="server"></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableHeaderCell ID="cellCaptionProductStock" runat="server" Text="<%$ Resources:, proStockRes %>"></asp:TableHeaderCell>
                    <asp:TableCell ID="cellProductStock" runat="server"></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableHeaderCell ID="cellCaptionProductReleaseDate" runat="server" Text="<%$ Resources:, proReleaseDateRes %>"></asp:TableHeaderCell>
                    <asp:TableCell ID="cellProductReleaseDate" runat="server"></asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <br />
            <asp:Table Visible ="false" CssClass="productDetails" ID="TableBookInfo" runat="server">
                <asp:TableRow runat="server">
                    <asp:TableHeaderCell ID="cellCaptionBookISBN" runat="server" Text="ISBN"></asp:TableHeaderCell>
                    <asp:TableCell ID="cellBookISBN" runat="server"></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableHeaderCell ID="cellCaptionBookEditorial" runat="server" Text="<%$ Resources:, proBookEditorialRes %>"></asp:TableHeaderCell>
                    <asp:TableCell ID="cellBookEditorial" runat="server"></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableHeaderCell ID="cellCaptionBookEdition" runat="server" Text="<%$ Resources:, proBookEditionRes %>"></asp:TableHeaderCell>
                    <asp:TableCell ID="cellBookEdition" runat="server"></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableHeaderCell ID="cellCaptionBookPages" runat="server" Text="<%$ Resources:, proBookPagesRes %>"></asp:TableHeaderCell>
                    <asp:TableCell ID="cellBookPages" runat="server"></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableHeaderCell ID="cellCaptionBookReleaseDate" runat="server" Text="<%$ Resources:, proPublicationDateRes %>"></asp:TableHeaderCell>
                    <asp:TableCell ID="cellBookReleaseDate" runat="server"></asp:TableCell>
                </asp:TableRow>
            </asp:Table>

            <asp:Table Visible ="false" CssClass="productDetails" ID="TableMusicInfo" runat="server">
                <asp:TableRow runat="server">
                    <asp:TableHeaderCell ID="cellCaptionMusicArtist" runat="server" Text="<%$ Resources:, proMusicArtistRes %>"></asp:TableHeaderCell>
                    <asp:TableCell ID="cellMusicArtist" runat="server"></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableHeaderCell ID="cellCaptionMusicAlbum" runat="server" Text="<%$ Resources:, proMusicAlbumRes %>"></asp:TableHeaderCell>
                    <asp:TableCell ID="cellMusicAlbum" runat="server"></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableHeaderCell ID="cellCaptionMusicSongs" runat="server" Text="<%$ Resources:, proMusicSongsRes %>"></asp:TableHeaderCell>
                    <asp:TableCell ID="cellMusicSongs" runat="server"></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableHeaderCell ID="cellCaptionMusicDurationMins" runat="server" Text="<%$ Resources:, proDurationMinsRes %>"></asp:TableHeaderCell>
                    <asp:TableCell ID="cellMusicDurationMins" runat="server"></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableHeaderCell ID="cellCaptionMusicReleaseDate" runat="server" Text="<%$ Resources:, proPublicationDateRes %>"></asp:TableHeaderCell>
                    <asp:TableCell ID="cellMusicReleaseDate" runat="server"></asp:TableCell>
                </asp:TableRow>
            </asp:Table>

            <asp:Table Visible ="false" CssClass="productDetails" ID="TableFilmInfo" runat="server">
                <asp:TableRow runat="server">
                    <asp:TableHeaderCell ID="cellCaptionFilmDirector" runat="server" Text="<%$ Resources:, proFilmDirectorRes %>"></asp:TableHeaderCell>
                    <asp:TableCell ID="cellFilmDirector" runat="server"></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableHeaderCell ID="cellCaptionFilmGenre" runat="server" Text="<%$ Resources:, proFilmGenreRes %>"></asp:TableHeaderCell>
                    <asp:TableCell ID="cellFilmGenre" runat="server"></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableHeaderCell ID="cellCaptionFilmRating" runat="server" Text="<%$ Resources:, proFilmRatingRes %>"></asp:TableHeaderCell>
                    <asp:TableCell ID="cellFilmRating" runat="server"></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableHeaderCell ID="cellCaptionFilmDurationMins" runat="server" Text="<%$ Resources:, proDurationMinsRes %>"></asp:TableHeaderCell>
                    <asp:TableCell ID="cellFilmDurationMins" runat="server"></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableHeaderCell ID="cellCaptionFilmReleaseDate" runat="server" Text="<%$ Resources:, proPublicationDateRes %>"></asp:TableHeaderCell>
                    <asp:TableCell ID="cellFilmReleaseDate" runat="server"></asp:TableCell>
                </asp:TableRow>
            </asp:Table>

            <br />
            <asp:Button ID="btnAddToCart" runat="server" Text="<%$ Resources:, btnAddToCartRes %>" OnClick="btnAddToCart_Click"/>
            <br />
            <asp:Label ID="lblProductOutOfStock" runat="server" Text="<%$ Resources: , lblProductOutOfStockRes %>" Font-Italic="True" Visible="false"></asp:Label>
            <br />
            <asp:Label ID="lblInternalError" runat="server" Text="<%$ Resources: Common, lblInternalErrorRes %>" Font-Italic="True" ForeColor="Red" Visible="false"></asp:Label>
            <br />
            <div class="productLinks">
                <asp:HyperLink ID="addCommentHyperLink" Text="<%$ Resources:, addComment %>" runat="server"/>
                &nbsp&nbsp&nbsp&nbsp
                <asp:HyperLink ID="findCommentsHyperLink" Text="<%$ Resources:, findComments %>" runat="server"/>
                &nbsp&nbsp&nbsp&nbsp
                <asp:HyperLink ID="updateHyperLink" Text="<%$ Resources:, update %>" runat="server" Visible="false" />
            </div>
        </form>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderFoot" runat="server">
    <br />
</asp:Content>