using Es.Udc.DotNet.PracticaMaD.Model.DAOs.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.TagDao;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.BookDao;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.FilmDao;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.MusicDao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using Ninject;
using System;


namespace Es.Udc.DotNet.PracticaMaD.Model.Services.CatalogService
{
    public class CatalogService : ICatalogService
    {
        [Inject]
        public IProductDao ProductDao { get; set; }

        [Inject]
        public IMusicDao MusicDao { get; set; }

        [Inject]
        public IBookDao BookDao { get; set; }

        [Inject]
        public IFilmDao FilmDao { get; set; }

        [Inject]
        public ICategoryDao CategoryDao { get; set; }

        [Inject]
        public ITagDao TagDao { get; set; }

        [Inject]
        public ICommentDao CommentDao { get; set; }

        [Inject]
        public IUserProfileDao UserProfileDao { get; set; }


        /// <exception cref="InputValidationException"/>
        private bool ValidateString(string stringToValidate)
        {
            if (stringToValidate == null || stringToValidate == "")
                return false;

            return true;
        }

        #region ICatalogService Members

        // ---------- FUN 3 - Actualizar información de producto ----------

        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="InputValidationException"/>
        /// <exception cref="UserIsNotAdminException"/>
        [Transactional]
        public void UpdateProductDetails(ProductDetails productDetails, long usrId)
        {
            if (!ValidateString(productDetails.ProName))
                throw new InputValidationException("Product name can't be null");
            if (productDetails.ProPrice <= 0)
                throw new InputValidationException("Product price has to be higher than 0");
            if (productDetails.ProStock < 0)
                throw new InputValidationException("Product stock can't be lower than 0");
            UserProfile user = UserProfileDao.Find(usrId);
            if (!user.admin)
                throw new UserIsNotAdminException(usrId);

            Product product = ProductDao.Find(productDetails.ProId);

            product.proName = productDetails.ProName;
            product.proPrice = productDetails.ProPrice;
            product.proStock = productDetails.ProStock;
            product.proReleaseDate = productDetails.ProReleaseDate;

            if (product is Book book && productDetails is BookDetails)
            {
                BookDetails bookDetails = productDetails as BookDetails;

                book.bookEdition = bookDetails.BookEdition;
                book.bookEditorial = bookDetails.BookEditorial;
                book.bookISBN = bookDetails.BookISBN;
                book.bookPages = bookDetails.BookPages;
                book.bookReleaseDate = bookDetails.BookReleaseDate;
            }

            else if (product is Music music && productDetails is MusicDetails)
            {
                MusicDetails musicDetails = productDetails as MusicDetails;

                music.musicAlbum = musicDetails.MusicAlbum;
                music.musicArtist = musicDetails.MusicArtist;
                music.musicDurationMins = musicDetails.MusicDurationMins;
                music.musicSongs = musicDetails.MusicSongs;
                music.musicReleaseDate = musicDetails.MusicReleaseDate;
            }

            else if (product is Film film && productDetails is FilmDetails)
            {
                FilmDetails filmDetails = productDetails as FilmDetails;

                film.filmDirector = filmDetails.FilmDirector;
                film.filmDurationMins = filmDetails.FilmDurationMins;
                film.filmGenre = filmDetails.FilmGenre;
                film.filmRating = filmDetails.FilmRating;
                film.filmReleaseDate = filmDetails.FilmReleaseDate;
            }

            ProductDao.Update(product);
        }


        // ---------- FUN 4 - Buscar productos ----------

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public ProductBlock FindProducts(string keyword, string proCatName, int startIndex, int size)
        {
            /*
            * Find count+1 accounts to determine if there exist more accounts above
            * the specified range.
            */
            CategoryDao.Find(proCatName);

            if (!ValidateString(keyword))
                keyword = "";

            List<Product> products =
                ProductDao.FindByKeywordAndProCatName(keyword, proCatName, startIndex, size + 1);

            bool existMoreProducts = (products.Count == size + 1);

            if (existMoreProducts)
                products.RemoveAt(size);

            return new ProductBlock(products, existMoreProducts);
        }

        [Transactional]
        public ProductBlock FindProducts(string keyword, int startIndex, int size)
        {
            if (!ValidateString(keyword))
                keyword = "";

            List<Product> products =
                ProductDao.FindByKeywordAndProCatName(keyword, "", startIndex, size + 1);

            bool existMoreProducts = (products.Count == size + 1);

            if (existMoreProducts)
                products.RemoveAt(size);

            return new ProductBlock(products, existMoreProducts);
        }

        [Transactional]
        public List<Category> FindAllCategories()
        {
            return CategoryDao.GetAllElements();
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public ProductDetails FindProductDetails(long proId)
        {
            Product product = ProductDao.Find(proId);

            if (product is Music)
            {
                Music music = product as Music;
                return new MusicDetails(proId, product.proName, product.proPrice, product.proReleaseDate,
                    product.proStock, product.proCatName, music.musicArtist, music.musicAlbum, music.musicSongs,
                    music.musicDurationMins, music.musicReleaseDate);
            }

            if (product is Book)
            {
                Book book = product as Book;
                return new BookDetails(proId, product.proName, product.proPrice, product.proReleaseDate,
                    product.proStock, product.proCatName, book.bookISBN, book.bookEditorial, book.bookEdition,
                    book.bookPages, book.bookReleaseDate);
            }

            if (product is Film)
            {
                Film film = product as Film;
                return new FilmDetails(proId, product.proName, product.proPrice, product.proReleaseDate,
                    product.proStock, product.proCatName, film.filmDirector, film.filmGenre, film.filmRating,
                    film.filmDurationMins, film.filmReleaseDate);
            }

            return new ProductDetails(proId, product.proName, product.proPrice, product.proReleaseDate, product.proStock, product.proCatName);
        }

        /// <summary>
        /// Checks if the specified proId corresponds to a valid Product.
        /// </summary>
        /// <returns> Boolean to indicate if the proId exists </returns>
        public bool ProductExists(long proId)
        {
            try
            {
                Product product = ProductDao.Find(proId);
            }
            catch (InstanceNotFoundException)
            {
                return false;
            }
            return true;
        }


        // ---------- FUN 8 - Añadir comentario a producto ----------

        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="InputValidationException"/>
        [Transactional]
        public long AddCommentToProduct(long proId, long usrId, string text, List<string> tags)
        {
            Product product = ProductDao.Find(proId);
            UserProfileDao.Find(usrId);
            if (!ValidateString(text))
                throw new InputValidationException("Comment text can't be null");

            Comment comment = new Comment();

            comment.commentDate = DateTime.Now;
            comment.commentText = text;
            comment.proId = proId;
            comment.usrId = usrId;

            Tag tag;
            foreach (string tagString in tags)
            {
                if (!ValidateString(tagString))
                    throw new InputValidationException("Tag name can't be null");
                try
                {
                    tag = TagDao.FindByTagName(tagString);
                    if (!comment.Tags.Contains(tag))
                    {
                        comment.Tags.Add(tag);
                        //tag.Comments.Add(comment);
                        tag.tagShows++;
                        TagDao.Update(tag);
                    }
                }
                catch (InstanceNotFoundException)
                {
                    tag = new Tag();
                    tag.tagName = tagString;
                    tag.tagShows = 1;
                    comment.Tags.Add(tag);
                    //tag.Comments.Add(comment);
                    TagDao.Create(tag);
                }
            }
            CommentDao.Create(comment);
            product.Comments.Add(comment);
            ProductDao.Update(product);
            return 1;
        }

        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="CommentDoesNotBelongToUserException"/>
        /// <exception cref="InputValidationException"/>
        [Transactional]
        public void UpdateComment(long idComment, long usrId, string newComment, List<string> newTags)
        {
            if (ValidateString(newComment) == false)
                throw new InputValidationException("Comment text can't be null");
            UserProfileDao.Find(usrId);
            if (CommentBelongsToUser(usrId, idComment) == false)
                throw new CommentDoesNotBelongToUserException(usrId, idComment);

            Comment comment = CommentDao.Find(idComment);

            comment.commentText = newComment;

            Tag tag = null;
            List<Tag> tagsToRemove = new List<Tag> { };
            foreach (Tag tagIncluded in comment.Tags)
            {
                if (!newTags.Contains(tagIncluded.tagName))
                    tagsToRemove.Add(tagIncluded);
            }
            foreach (Tag tagToRemove in tagsToRemove)
            {
                comment.Tags.Remove(tagToRemove);
                tagToRemove.tagShows--;
                tagToRemove.Comments.Remove(comment);
                //TagDao.Update(tagToRemove);
            }

            foreach (string tagString in newTags)
            {
                if (ValidateString(tagString) == false)
                    throw new InputValidationException("Tag name can't be null");
                try
                {
                    tag = TagDao.FindByTagName(tagString);
                    if (comment.Tags.Contains(tag) == false)
                    {
                        comment.Tags.Add(tag);
                        //tag.Comments.Add(comment);
                        tag.tagShows++;
                        TagDao.Update(tag);
                    }
                }
                catch (InstanceNotFoundException)
                {
                    tag = new Tag();
                    tag.tagName = tagString;
                    tag.tagShows = 1;
                    comment.Tags.Add(tag);
                    //tag.Comments.Add(comment);
                    TagDao.Create(tag);
                }
            }
            CommentDao.Update(comment);
        }

        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="CommentDoesNotBelongToUserException"/>
        [Transactional]
        public void DeleteComment(long idComment, long usrId)
        {
            Comment comment = CommentDao.Find(idComment);

            UserProfileDao.Find(usrId);
            if (!CommentBelongsToUser(usrId, idComment))
                throw new CommentDoesNotBelongToUserException(usrId, idComment);


            List<Tag> tagsToRemove = new List<Tag> { };
            foreach (Tag tagIncluded in comment.Tags)
            {
                tagsToRemove.Add(tagIncluded);
            }
            foreach (Tag tagToRemove in tagsToRemove)
            {
                comment.Tags.Remove(tagToRemove);
                tagToRemove.tagShows--;
                tagToRemove.Comments.Remove(comment);
                TagDao.Update(tagToRemove);
            }

            CommentDao.Remove(idComment);
        }

        /// <summary>
        /// Checks if the specified idComment corresponds to a valid Comment.
        /// </summary>
        /// <returns> Boolean to indicate if the idComment exists</returns>
        public bool CommentExists(long idComment)
        {
            try
            {
                Comment comment = CommentDao.Find(idComment);
            }
            catch (InstanceNotFoundException)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if the specified usrId belongs to the idComment author's.
        /// </summary>
        /// <returns> Boolean to indicate if usrId is the author of idComment</returns>
        /// <exception cref="InstanceNotFoundException"/>
        public bool CommentBelongsToUser(long usrId, long idComment)
        {
            Comment comment = CommentDao.Find(idComment);

            if (comment.usrId == usrId)
            {
                return true;
            }
            return false;
        }

        /// <exception cref="InstanceNotFoundException"/>
        public Comment FindCommentById(long commentId)
        {
            return CommentDao.Find(commentId);
        }


        // ---------- FUN 9 - Ver comentarios de un producto ----------

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public CommentBlock FindCommentsByProduct(long proId, int startIndex, int size)
        {
            ProductDao.Find(proId);

            List<Comment> comments =
                CommentDao.FindByIdProduct(proId, startIndex, size + 1).OrderByDescending(c => c.commentDate).ToList();

            bool existMoreComments = (comments.Count == size + 1);

            if (existMoreComments)
                comments.RemoveAt(size);

            return new CommentBlock(comments, existMoreComments);
        }

        // ---------- FUN 10 - Etiquetar un comentario ----------
        // Parte de esta funcionalidad se encuentra dentro de la funcionalidad 8

        /// <summary>
        /// Checks if the specified tagName corresponds to a valid Tag.
        /// </summary>
        /// <returns> Boolean to indicate if the tagName exists </returns>
        public bool TagExists(string tagName)
        {
            try
            {
                Tag tag = TagDao.FindByTagName(tagName);
            }
            catch (InstanceNotFoundException)
            {
                return false;
            }
            return true;
        }

        [Transactional]
        public List<Tag> FindAllTags()
        {
            return TagDao.GetAllElements();
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public CommentBlock FindCommentsByTagName(string tagName, int startIndex, int size)
        {
            TagDao.Find(tagName);

            List<Comment> comments =
                CommentDao.FindByTagName(tagName, startIndex, size + 1);

            bool existMoreComments = (comments.Count == size + 1);

            if (existMoreComments)
                comments.RemoveAt(size);

            return new CommentBlock(comments, existMoreComments);
        }

        # endregion ICatalogService Members

    }
}
