using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.Services.CatalogService;
using Es.Udc.DotNet.PracticaMaD.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.Collections.Generic;
using System.Transactions;
using System;
using System.Text;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.MusicDao;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.BookDao;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.FilmDao;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.TagDao;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.CommentDao;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Test.Services
{
    /// <summary>
    /// Descripción resumida de ICatalogServiceTest
    /// </summary>
    [TestClass]
    public class ICatalogServiceTest
    {

        private static IKernel kernel;
        private static ICatalogService catalogService;
        private static IProductDao productDao;
        private static IMusicDao musicDao;
        private static IBookDao bookDao;
        private static IFilmDao filmDao;
        private static ICategoryDao categoryDao;
        private static ITagDao tagDao;
        private static ICommentDao commentDao;

        private const long NON_EXISTENT_ID = -1;
        private const string NON_EXISTENT_NAME = "non_existent_name";

        private TransactionScope transactionScope;
        private TestContext testContextInstance;

        /// <summary>
        ///Obtiene o establece el contexto de las pruebas que proporciona
        ///información y funcionalidad para la serie de pruebas actual.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes

        // Use ClassInitialize para ejecutar el código antes de ejecutar la primera prueba en la clase
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();
            catalogService = kernel.Get<ICatalogService>();
            productDao = kernel.Get<IProductDao>();
            musicDao = kernel.Get<IMusicDao>();
            bookDao = kernel.Get<IBookDao>();
            filmDao = kernel.Get<IFilmDao>();
            categoryDao = kernel.Get<ICategoryDao>();
            tagDao = kernel.Get<ITagDao>();
            commentDao = kernel.Get<ICommentDao>();
        }

        // Use ClassCleanup para ejecutar el código una vez ejecutadas todas las pruebas en una clase
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            TestManager.ClearNInjectKernel(kernel);
        }

        // Usar TestInitialize para ejecutar el código antes de ejecutar cada prueba 
        [TestInitialize()]
        public void MyTestInitialize()
        {
            transactionScope = new TransactionScope();
        }

        // Use TestCleanup para ejecutar el código una vez ejecutadas todas las pruebas
        [TestCleanup()]
        public void MyTestCleanup()
        {
            transactionScope.Dispose();
        }

        #endregion


        #region FUN 3 Tests

        [TestMethod]
        public void TestUpdateProductDetails()
        {
            ProductDetails productDetails = new ProductDetails(1, "updated product", 2, DateTime.Today, 40, "Books");

            catalogService.UpdateProductDetails(productDetails, 1);

            ProductDetails foundDetails = (ProductDetails)catalogService.FindProductDetails(1);

            Assert.AreEqual(productDetails.ProId, foundDetails.ProId);
            Assert.AreEqual(productDetails.ProName, foundDetails.ProName);
            Assert.AreEqual(productDetails.ProPrice, foundDetails.ProPrice);
            Assert.AreEqual(productDetails.ProReleaseDate, foundDetails.ProReleaseDate);
            Assert.AreEqual(productDetails.ProStock, foundDetails.ProStock);
            Assert.AreEqual(productDetails.ProCatName, foundDetails.ProCatName);

        }

        [TestMethod]
        public void TestUpdateBookDetails()
        {
            BookDetails bookDetails = new BookDetails(1, "book1", 12, DateTime.Today, 40, "Books", "978-3-16-148410-9", "editorial hola", "edicion hola",
                250, DateTime.Today);

            catalogService.UpdateProductDetails(bookDetails, 1);

            Assert.AreEqual(catalogService.FindProductDetails(1), bookDetails);
        }

        [TestMethod]
        public void TestUpdateMusicDetails()
        {
            MusicDetails musicDetails = new MusicDetails(4, "music1", 12, DateTime.Today, 40, "Music", "pepito", "grillo", 39, 50, DateTime.Today);

            catalogService.UpdateProductDetails(musicDetails, 1);

            Assert.AreEqual(catalogService.FindProductDetails(4), musicDetails);
        }

        [TestMethod]
        public void TestUpdateFilmDetails()
        {
            FilmDetails filmDetails = new FilmDetails(7, "film1", 12, DateTime.Today, 40, "Films", "pepito", "terror", 4, 160, DateTime.Today);

            catalogService.UpdateProductDetails(filmDetails, 1);

            Assert.AreEqual(catalogService.FindProductDetails(7), filmDetails);
        }

        [TestMethod]
        [ExpectedException(typeof(InputValidationException))]
        public void TestUpdateInvalidName()
        {
            ProductDetails productDetails = new ProductDetails(1, "", 2, DateTime.Today, 40, "Books");

            catalogService.UpdateProductDetails(productDetails, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(InputValidationException))]
        public void TestUpdateInvalidPrice()
        {
            ProductDetails productDetails = new ProductDetails(1, "updated product", 0, DateTime.Today, 40, "Books");

            catalogService.UpdateProductDetails(productDetails, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(InputValidationException))]
        public void TestUpdateInvalidStock()
        {
            ProductDetails productDetails = new ProductDetails(1, "updated product", 2, DateTime.Today, -1, "Books");

            catalogService.UpdateProductDetails(productDetails, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void TestUpdateIncorrectUser()
        {
            ProductDetails productDetails = new ProductDetails(1, "updated product", 2, DateTime.Today, 40, "Books");

            catalogService.UpdateProductDetails(productDetails, NON_EXISTENT_ID);
        }

        [TestMethod]
        [ExpectedException(typeof(UserIsNotAdminException))]
        public void TestUpdateNotAdminUser()
        {
            ProductDetails productDetails = new ProductDetails(1, "updated product", 2, DateTime.Today, 40, "Books");

            catalogService.UpdateProductDetails(productDetails, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void TestUpdateIncorrectProduct()
        {
            ProductDetails productDetails = new ProductDetails(NON_EXISTENT_ID, "updated product", 2, DateTime.Today, 40, "Books");

            catalogService.UpdateProductDetails(productDetails, 1);
        }
        #endregion


        #region FUN 4 Tests

        [TestMethod]
        public void TestFindProducts()
        {
            ProductBlock actual = catalogService.FindProducts("i", 0, 10);
            List<Product> expectedProducts = productDao.FindByKeywordAndProCatName("i", "", 0, 11);


            if (actual.ExistMoreProducts)
            {
                Assert.IsTrue(actual.Products.Count() == 10);
                Assert.IsTrue(expectedProducts.Count() == 11);
            }
            else
            {
                Assert.IsTrue(actual.Products.Count() <= 10);
                Assert.IsTrue(expectedProducts.Count() >= actual.Products.Count());
            }

            foreach (Product product in actual.Products)
            {
                Assert.IsTrue(product.proName.Contains("i"));
                Assert.IsTrue(expectedProducts.Contains(product));
            }
        }

        [TestMethod]
        public void TestFindProductsNoKeyword()
        {
            List<Product> expected = productDao.GetAllElements();
            ProductBlock actual = catalogService.FindProducts("", 0, expected.Count*2);

            Assert.AreEqual(expected.Count(), actual.Products.Count());
            Assert.IsTrue(!actual.ExistMoreProducts);

            foreach (Product product in actual.Products)
            {
                Assert.IsTrue(expected.Contains(product));
            }
        }

        [TestMethod]
        public void TestFindProductsInexistentKeyword()
        {
            ProductBlock products = catalogService.FindProducts("www", 0, 10);

            Assert.IsTrue(products.Products.Count() == 0);
        }

        [TestMethod]
        public void TestFindProductsCategory()
        {
            ProductBlock actual = catalogService.FindProducts("", "Books", 0, 100);
            List<Book> expected = bookDao.GetAllElements();

            Assert.AreEqual(expected.Count, actual.Products.Count());
            Assert.IsTrue(!actual.ExistMoreProducts);

            foreach (Product product in actual.Products)
            {
                Assert.IsTrue(product.proCatName == "Books");
                Assert.IsTrue(expected.Contains(product));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void TestFindProductsIncorrectCategory()
        {
            catalogService.FindProducts("", NON_EXISTENT_NAME, 0, 10);
        }

        [TestMethod]
        public void TestFindAllCategories()
        {
            List<Category> expected = categoryDao.GetAllElements();
            List<Category> actual = catalogService.FindAllCategories();

            Assert.AreEqual(expected.Count, actual.Count);

            foreach(Category category in actual)
            {
                Assert.IsTrue(expected.Contains(category));
            }
        }

        // El resto de casos de prueba de esta función quedan corroborados en los tests de la Func 3
        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void TestFindProductDetailsIncorrectProduct()
        {
            catalogService.FindProductDetails(NON_EXISTENT_ID);
        }

        
        #endregion


        #region FUN 8 Tests

        [TestMethod]
        public void TestAddComment()
        {
            long commentId = catalogService.AddCommentToProduct(2, 2, "no me gusta", new List<string> { });

            Assert.IsTrue(commentId == 1);

            CommentBlock comments = catalogService.FindCommentsByProduct(2, 0, 100);

            Comment comment = comments.Comments[comments.Comments.Count()-1];

            Assert.IsTrue(comment.proId == 2);
            Assert.IsTrue(comment.usrId == 2);
            Assert.IsTrue(comment.commentText == "no me gusta");
            Assert.IsTrue(comment.Tags.Count == 0);
        }

        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void TestAddCommentIncorrectProduct()
        {
            catalogService.AddCommentToProduct(NON_EXISTENT_ID, 2, "no me gusta", new List<string> { });
        }

        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void TestAddCommentIncorrectUser()
        {
            catalogService.AddCommentToProduct(2, NON_EXISTENT_ID, "no me gusta", new List<string> { });
        }

        [TestMethod]
        [ExpectedException(typeof(InputValidationException))]
        public void TestAddCommentInvalidComment()
        {
            catalogService.AddCommentToProduct(2, 2, "", new List<string> { });
        }

        [TestMethod]
        public void TestAddCommentNewTags()
        {
            List<string> tags = new List<string> {
                "Hola",
                "Adios",
                "Nada",
                "Todo"
            };

            foreach (string tag in tags)
            {
                Assert.ThrowsException<InstanceNotFoundException>( () =>
                    tagDao.FindByTagName(tag)
                );
            }

            catalogService.AddCommentToProduct(2, 2, "no me gusta", tags);

            CommentBlock comments = catalogService.FindCommentsByProduct(2, 0, 100);
            Comment comment = comments.Comments[comments.Comments.Count()-1];

            Assert.AreEqual(tags.Count, comment.Tags.Count);
            foreach(string tag in tags)
            {
                Assert.IsTrue(comment.Tags.Contains(tagDao.FindByTagName(tag)));
            }
        }

        [TestMethod]
        public void TestAddCommentExistentTags()
        {
            List<string> tags = new List<string> {
                "Ganga",
                "Pésimo"
            };

            int expected = catalogService.FindAllTags().Count;

            catalogService.AddCommentToProduct(2, 2, "no me gusta", tags);

            CommentBlock comments = catalogService.FindCommentsByProduct(2, 0, 100);
            Comment comment = comments.Comments[comments.Comments.Count()-1];

            Assert.AreEqual(tags.Count, comment.Tags.Count);

            List<Tag> commentTags = tagDao.FindByIdComment(comment.commentId);

            int i = 0;
            foreach (string tag in tags)
            {
                Assert.IsTrue(commentTags[i].tagName == tag);
                i++;
            }

            Assert.AreEqual(expected, catalogService.FindAllTags().Count);
        }

        [TestMethod]
        public void TestUpdateComment()
        {
            Comment comment = commentDao.Find(1);
            List<string> tags = new List<string> { "Hecho" };

            Assert.AreNotEqual(comment.commentText, "Hago el cambio");

            catalogService.UpdateComment(1, 1, "Hago el cambio", tags);

            comment = commentDao.Find(1);

            Assert.AreEqual(comment.commentText, "Hago el cambio");
            Assert.AreEqual(comment.Tags.Count, tags.Count);
            foreach (Tag tag in comment.Tags)
                Assert.IsTrue(tags.Contains(tag.tagName));
        }

        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void TestUpdateCommentIncorrectComment()
        {
            catalogService.UpdateComment(NON_EXISTENT_ID, 1, "hago el cambio", new List<string> { "Hecho" });
        }

        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void TestUpdateCommentIncorrectUser()
        {
            catalogService.UpdateComment(1, NON_EXISTENT_ID, "hago el cambio", new List<string> { "Hecho" });
        }

        [TestMethod]
        [ExpectedException(typeof(CommentDoesNotBelongToUserException))]
        public void TestUpdateCommentUserIsNotCommentOwner()
        {
            catalogService.UpdateComment(1, 2, "hago el cambio", new List<string> { "Hecho" });
        }

        [TestMethod]
        [ExpectedException(typeof(InputValidationException))]
        public void TestUpdateCommentInvalidComment()
        {
            catalogService.UpdateComment(1, 1, "", new List<string> { "Hecho" });
        }

        [TestMethod]
        public void TestDeleteComment()
        {
            catalogService.DeleteComment(1, 1);
            Assert.ThrowsException<InstanceNotFoundException>(() =>
                commentDao.Find(1)
            );
        }

        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void TestDeleteCommentIncorrectComment()
        {
            catalogService.DeleteComment(NON_EXISTENT_ID, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void TestDeleteCommentIncorrectUser()
        {
            catalogService.DeleteComment(1, NON_EXISTENT_ID);
        }

        [TestMethod]
        [ExpectedException(typeof(CommentDoesNotBelongToUserException))]
        public void TestDeleteCommentUserIsNotCommentOwner()
        {
            catalogService.DeleteComment(1, 2);
        }

        #endregion


        #region FUN 9 Tests

        [TestMethod]
        public void TestFindCommentsByProduct()
        {
            var expected = productDao.Find(1).Comments;
            CommentBlock actual = catalogService.FindCommentsByProduct(1, 0, 100);

            Assert.AreEqual(expected.Count, actual.Comments.Count);
            foreach (Comment comment in actual.Comments)
            {
                Assert.IsTrue(expected.Contains(comment));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void TestFindCommentsIncorrectProduct()
        {
            catalogService.FindCommentsByProduct(NON_EXISTENT_ID, 0, 10);
        }

        #endregion


        #region FUN 10 Tests

        [TestMethod]
        public void TestFindAllTags()
        {
            List<Tag> expected = tagDao.GetAllElements();
            List<Tag> actual = catalogService.FindAllTags();

            Assert.AreEqual(expected.Count, actual.Count);

            foreach (Tag tag in actual)
            {
                Assert.IsTrue(expected.Contains(tag));
            }
        }

        [TestMethod]
        public void TestFindCommentsByTagName()
        {
            var expected = commentDao.FindByTagName("Pésimo", 0, 100);
            CommentBlock actual = catalogService.FindCommentsByTagName("Pésimo", 0, 100);

            Assert.AreEqual(expected.Count, actual.Comments.Count());

            foreach (Comment comment in actual.Comments)
            {
                Assert.IsTrue(expected.Contains(comment));
            }
        }

        #endregion
    }
}