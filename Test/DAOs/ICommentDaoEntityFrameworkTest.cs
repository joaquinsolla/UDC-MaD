using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace Es.Udc.DotNet.PracticaMaD.Test.DAOs
{
    [TestClass]
    public class ICommentDaoEntityFrameworkTest
    {
        private static IKernel kernel;
        private static ICommentDao commentDao;

        // Variables used in several tests are initialized here
        private const long NON_EXISTENT_TAG_ID = -1;
        private const long NON_EXISTENT_COMMENT_ID = -1;
        private const long NON_EXISTENT_PRODUCT_ID = -1;

        private TransactionScope transactionScope;

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
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

        #region Atributos de prueba adicionales

        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();
            commentDao = kernel.Get<ICommentDao>();
        }

        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            TestManager.ClearNInjectKernel(kernel);
        }

        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            transactionScope = new TransactionScope();
        }

        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            transactionScope.Dispose();
        }

        #endregion


        [TestMethod()]
        public void Find()
        {

            Comment comment = null;

            comment = commentDao.Find(1);

            Assert.IsTrue(comment.proId == 1 && comment.usrId == 1 && comment.commentText == "Una ganga de libro"
                && comment.commentDate == new System.DateTime(2022, 10, 15));

            comment = commentDao.Find(2);

            Assert.IsTrue(comment.proId == 3 && comment.usrId == 1 && comment.commentText == "Aprovechen la oferta por este libro"
                && comment.commentDate == new System.DateTime(2022, 10, 12));

            comment = commentDao.Find(3);

            Assert.IsTrue(comment.proId == 7 && comment.usrId == 1 && comment.commentText == "No vale la pena esta película"
                && comment.commentDate == new System.DateTime(2022, 10, 10));

            Assert.ThrowsException<InstanceNotFoundException>(() => commentDao.Find(NON_EXISTENT_COMMENT_ID));

        }

        [TestMethod()]
        public void Add()
        {

            Comment newComment = new Comment();

            newComment.proId = 2;
            newComment.usrId = 1;
            newComment.commentText = "Comentario de prueba.";
            newComment.commentDate = new System.DateTime(2010, 01, 01);

            commentDao.Create(newComment);

            Comment foundComment = commentDao.GetAllElements()[3];

            Assert.IsTrue(foundComment.proId == 2 && foundComment.usrId == 1 && foundComment.commentText == "Comentario de prueba."
                && foundComment.commentDate == new System.DateTime(2010, 01, 01));

            Assert.IsTrue(foundComment.Equals(newComment));

        }

        [TestMethod()]
        public void Remove()
        {

            commentDao.Find(1);

            commentDao.Remove(1);

            Assert.ThrowsException<InstanceNotFoundException>(() => commentDao.Find(1));

        }
        /*
        [TestMethod()]
        public void FindByIdProduct()
        {

            IQueryable<Comment> comments = null;

            comments = commentDao.FindByIdProduct(1);

            Assert.IsTrue(comments.Count == 1);

            Assert.IsTrue(comments[0].proId == 1);

            comments = commentDao.FindByIdProduct(3);

            Assert.IsTrue(comments.Count == 1);

            Assert.IsTrue(comments[0].proId == 3);

            comments = commentDao.FindByIdProduct(7);

            Assert.IsTrue(comments.Count == 1);

            Assert.IsTrue(comments[0].proId == 7);

            comments = commentDao.FindByIdProduct(NON_EXISTENT_PRODUCT_ID);

            Assert.IsTrue(comments.Count == 0);

        }

        [TestMethod()]
        public void FindByIdTag()
        {

            IQueryable<Comment> comments = null;

            comments = commentDao.FindByIdTag(1);

            Assert.IsTrue(comments.Count == 2);

            Assert.IsTrue(comments[0].commentId == 1);

            Assert.IsTrue(comments[1].commentId == 2);

            comments = commentDao.FindByIdTag(2);

            Assert.IsTrue(comments.Count == 1);

            Assert.IsTrue(comments[0].commentId == 1);

            comments = commentDao.FindByIdTag(3);

            Assert.IsTrue(comments.Count == 1);

            Assert.IsTrue(comments[0].commentId == 1);

            comments = commentDao.FindByIdProduct(NON_EXISTENT_TAG_ID);

            Assert.IsTrue(comments.Count == 0);

        }
        */
    }
}
