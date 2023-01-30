using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.Services.CatalogService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages
{
    public partial class UpdateProduct : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                ICatalogService catalogService = iocManager.Resolve<ICatalogService>();

                long proId = Int32.Parse(Request.Params.Get("proId"));

                ProductDetails product = catalogService.FindProductDetails(proId);

                txtProductName.Text = product.ProName;
                txtProductPrice.Text = product.ProPrice.ToString();
                txtProductStock.Text = product.ProStock.ToString();
                txtCategory.Text = product.ProCatName;
                cdrProductReleaseDate.SelectedDate = product.ProReleaseDate;

                if (product is MusicDetails)
                {
                    lblMusicInformation.Visible = true;

                    MusicDetails music = product as MusicDetails;

                    UpdateMusicPanel.Visible = true;
                    txtMusicAlbum.Text = music.MusicAlbum;
                    txtMusicArtist.Text = music.MusicArtist;
                    txtMusicDurationMins.Text = music.MusicDurationMins.ToString();
                    cdrMusicReleaseDate.SelectedDate = music.MusicReleaseDate;
                    txtMusicSongs.Text = music.MusicSongs.ToString();
                }
                else if (product is BookDetails)
                {
                    lblBookInformation.Visible = true;

                    BookDetails book = product as BookDetails;

                    UpdateBookPanel.Visible = true;
                    txtBookISBN.Text = book.BookISBN;
                    txtBookEdition.Text = book.BookEdition;
                    txtBookEditorial.Text = book.BookEditorial;
                    txtBookPages.Text = book.BookPages.ToString();
                    cdrBookReleaseDate.SelectedDate = book.BookReleaseDate;
                }
                else if (product is FilmDetails)
                {
                    lblFilmInformation.Visible = true;

                    FilmDetails film = product as FilmDetails;

                    UpdateFilmPanel.Visible = true;
                    txtFilmDirector.Text = film.FilmDirector;
                    txtFilmGenre.Text = film.FilmGenre;
                    txtFilmRating.Text = film.FilmRating.ToString();
                    txtFilmDurationMins.Text = film.FilmDurationMins.ToString();
                    cdrFilmReleaseDate.SelectedDate = film.FilmReleaseDate;
                }
            }
        }

        protected void BtnUpdateClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                long proId = Int32.Parse(Request.Params.Get("proId"));

                if (txtProductName.Text.Trim() == "no") { lblInternalError.Visible = true; return; }

                ProductDetails productDetails = null;

                if (UpdateMusicPanel.Visible)
                {
                    productDetails = new MusicDetails(proId, 
                        txtProductName.Text.Trim(),
                        decimal.Parse(txtProductPrice.Text.Trim()), 
                        cdrProductReleaseDate.SelectedDate,
                        Int32.Parse(txtProductStock.Text.Trim()),
                        "Books",
                        txtMusicArtist.Text.Trim(), 
                        txtMusicAlbum.Text.Trim(), 
                        Int32.Parse(txtMusicSongs.Text.Trim()),
                        Int32.Parse(txtMusicDurationMins.Text.Trim()),
                        cdrMusicReleaseDate.SelectedDate);
                }
                else if (UpdateFilmPanel.Visible)
                {
                    productDetails = new FilmDetails(proId, 
                        txtProductName.Text.Trim(),
                        decimal.Parse(txtProductPrice.Text.Trim()),
                        cdrProductReleaseDate.SelectedDate,
                        Int32.Parse(txtProductStock.Text.Trim()),
                        "Films",
                        txtFilmDirector.Text.Trim(), 
                        txtFilmGenre.Text.Trim(), 
                        Int32.Parse(txtFilmRating.Text.Trim()),
                        Int32.Parse(txtFilmDurationMins.Text.Trim()),
                        cdrFilmReleaseDate.SelectedDate);
                }
                else if (UpdateBookPanel.Visible)
                {
                    productDetails = new BookDetails(proId, 
                        txtProductName.Text.Trim(),
                        decimal.Parse(txtProductPrice.Text.Trim()),
                        cdrProductReleaseDate.SelectedDate,
                        Int32.Parse(txtProductStock.Text.Trim()),
                        "Books",
                        txtBookISBN.Text.Trim(),
                        txtBookEditorial.Text.Trim(), 
                        txtBookEdition.Text.Trim(),
                        Int32.Parse(txtBookPages.Text.Trim()),
                        cdrBookReleaseDate.SelectedDate);
                }
                else
                {
                    lblInternalError.Visible = true;
                }

                UserSession user = (UserSession)SessionManager.GetUserSession(Context);

                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                ICatalogService catalogService = iocManager.Resolve<ICatalogService>();

                try
                {
                    catalogService.UpdateProductDetails(productDetails, user.UserProfileId);

                    String url = "~/Pages/Catalog/ShowProductByProID.aspx" + "?productId=" + proId;

                    Response.Redirect(Response.ApplyAppPathModifier(url));
                }
                catch (InstanceNotFoundException) 
                {
                    lblInternalError.Visible = true;
                }
                catch (InputValidationException)
                {
                    lblInternalError.Visible = true;
                }
                catch (UserIsNotAdminException)
                {
                    lblInternalError.Visible = true;
                }
                catch (Exception)
                {
                    lblInternalError.Visible = true;
                }

            }
        }
    }
}