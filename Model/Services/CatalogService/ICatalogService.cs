using Es.Udc.DotNet.PracticaMaD.Model.Exceptions;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System;
using Ninject;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.TagDao;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.MusicDao;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.BookDao;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.FilmDao;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.UserProfileDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.Services.CatalogService
{
    public interface ICatalogService
    {


        [Inject]
        IProductDao ProductDao { get; set; }

        [Inject]
        IMusicDao MusicDao { get; set; }

        [Inject]
        IBookDao BookDao { get; set; }

        [Inject]
        IFilmDao FilmDao { get; set; }

        [Inject]
        ICategoryDao CategoryDao { get; set; }

        [Inject]
        ITagDao TagDao { get; set; }

        [Inject]
        ICommentDao CommentDao { get; set; }

        [Inject]
        IUserProfileDao UserProfileDao { get; set; }


        // ---------- FUN 3 ----------

        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="InputValidationException"/>
        /// <exception cref="UserIsNotAdminException"/>
        [Transactional]
        void UpdateProductDetails(ProductDetails productDetails, long usrId);


        // ---------- FUN 4 ----------

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        ProductBlock FindProducts(string keyword, string proCatName, int startIndex, int size);

        [Transactional]
        ProductBlock FindProducts(string keyword, int startIndex, int size);

        [Transactional]
        List<Category> FindAllCategories();

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        ProductDetails FindProductDetails(long proId);

        /// <summary>
        /// Checks if the specified proId corresponds to a valid Product.
        /// </summary>
        /// <returns> Boolean to indicate if the proId exists </returns>
        bool ProductExists(long proId);


        // ---------- FUN 8 ----------

        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="InputValidationException"/>
        [Transactional]
        long AddCommentToProduct(long proId, long usrId, string text, List<string> tags);

        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="CommentDoesNotBelongToUserException"/>
        /// <exception cref="InputValidationException"/>
        [Transactional]
        void UpdateComment(long idComment, long usrId, string newComment, List<string> newTags);

        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="CommentDoesNotBelongToUserException"/>
        [Transactional]
        void DeleteComment(long idComment, long usrId);

        /// <summary>
        /// Checks if the specified idComment corresponds to a valid Comment.
        /// </summary>
        /// <returns> Boolean to indicate if the idComment exists</returns>
        bool CommentExists(long idComment);

        /// <summary>
        /// Checks if the specified usrId belongs to the idComment author's.
        /// </summary>
        /// <returns> Boolean to indicate if usrId is the author of idComment</returns>
        /// <exception cref="InstanceNotFoundException"/>
        bool CommentBelongsToUser(long usrId, long idComment);

        
        /// <exception cref="InstanceNotFoundException"/>
        Comment FindCommentById(long commentId);


        // ---------- FUN 9 ----------

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        CommentBlock FindCommentsByProduct(long proId, int startIndex, int size);

        // ---------- FUN 10 ----------

        /// <summary>
        /// Checks if the specified tagName corresponds to a valid Tag.
        /// </summary>
        /// <returns> Boolean to indicate if the tagName exists </returns>
        bool TagExists(string tagName);

        [Transactional]
        List<Tag> FindAllTags();

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        CommentBlock FindCommentsByTagName(string tagName, int startIndex, int size);
    }
}
