using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.Services.CatalogService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Web.Properties;
using System;
using System.Web;
using Es.Udc.DotNet.PracticaMaD.Model.Services.ShoppingService;
using Es.Udc.DotNet.PracticaMaD.Model.Exceptions;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages
{
    public partial class ShowProductByProID : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            ICatalogService catalogService = iocManager.Resolve<ICatalogService>();

            UserSession session = SessionManager.GetUserSession(Context);
            bool userAdmin = false;
            try
            {
                userAdmin = session.Admin;
            }
            catch (NullReferenceException)
            {
                userAdmin = false;
            }

            long productId = Convert.ToInt32(Request.Params.Get("productId"));

            ProductDetails product = catalogService.FindProductDetails(productId);

            cellProductName.Text = product.ProName;
            cellProductPrice.Text = product.ProPrice.ToString() + " €";
            cellProductReleaseDate.Text = product.ProReleaseDate.ToString();
            cellCategoryName.Text = product.ProCatName;
            cellProductStock.Text = product.ProStock.ToString();

            updateHyperLink.Visible = userAdmin;

            if (product is MusicDetails)
            {
                MusicDetails music = product as MusicDetails;

                TableMusicInfo.Visible = true;
                cellMusicAlbum.Text = music.MusicAlbum;
                cellMusicArtist.Text = music.MusicArtist;
                cellMusicDurationMins.Text = music.MusicDurationMins.ToString();
                cellMusicReleaseDate.Text = music.MusicReleaseDate.ToString();
                cellMusicSongs.Text = music.MusicSongs.ToString();
            }
            else if (product is BookDetails)
            {
                BookDetails book = product as BookDetails;

                TableBookInfo.Visible = true;
                cellBookISBN.Text = book.BookISBN;
                cellBookEdition.Text = book.BookEdition;
                cellBookEditorial.Text = book.BookEditorial;
                cellBookPages.Text = book.BookPages.ToString();
                cellBookReleaseDate.Text = book.BookReleaseDate.ToString();
            }
            else if (product is FilmDetails)
            {
                FilmDetails film = product as FilmDetails;

                TableFilmInfo.Visible = true;
                cellFilmDirector.Text = film.FilmDirector;
                cellFilmGenre.Text = film.FilmGenre;
                cellFilmRating.Text = film.FilmRating.ToString();
                cellFilmDurationMins.Text = film.FilmDurationMins.ToString();
                cellFilmReleaseDate.Text = film.FilmReleaseDate.ToString();
            }

            this.updateHyperLink.NavigateUrl = String.Format("~/Pages/Catalog/UpdateProduct.aspx?proId={0}", productId);
            this.addCommentHyperLink.NavigateUrl = String.Format("~/Pages/Catalog/AddComment.aspx?proId={0}", productId);
            this.findCommentsHyperLink.NavigateUrl = String.Format("~/Pages/Catalog/ShowProComments.aspx?proId={0}", productId);

            if (product.ProStock < 1) 
            {
                btnAddToCart.Visible = false;
                lblProductOutOfStock.Visible = true;
            }

        }

        protected void btnAddToCart_Click(object sender, EventArgs e)
        {

            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IShoppingService shoppingService = iocManager.Resolve<IShoppingService>();

            ShoppingCart cart = (ShoppingCart)SessionManager.GetShoppingCart(Context);

            long productId = Convert.ToInt32(Request.Params.Get("productId"));

            try
            {
                shoppingService.AddProductToShoppingCart(productId, cart);

                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }
            catch (ProductOutOfStockException)
            {
                lblInternalError.Visible = true;
            }
            catch (InstanceNotFoundException)
            {
                lblInternalError.Visible = true;
            }

        }
    }
}