using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.BookDao;
using Es.Udc.DotNet.PracticaMaD.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.Collections.Generic;
using System.Transactions;

namespace Es.Udc.DotNet.PracticaMaD.Test.DAOs
{
    [TestClass]
    public class IBookDaoEntityFrameworkTest
    {
        private static IKernel kernel;
        private static IBookDao bookDao;

        // Variables used in several tests are initialized here
        private const long NON_EXISTENT_BOOK_ID = -1;

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
            bookDao = kernel.Get<IBookDao>();
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

            Book book = null;

            book = bookDao.Find(1);

            Assert.IsTrue(book.proId == 1 && book.proName == "Book 1" && book. proPrice == 9.95M 
                && book.proReleaseDate == new System.DateTime(2022, 10, 08) && book.proCatName == "Books"
                && book.bookISBN == "978-3-16-148410-1" && book.bookEditorial == "Editorial x"
                && book.bookEdition == "Tapa dura" && book.bookPages == 254 && book.bookReleaseDate == new System.DateTime(2001, 08, 05));

            book = bookDao.Find(2);

            Assert.IsTrue(book.proId == 2 && book.proName == "Book 2" && book.proPrice == 4.95M
                && book.proReleaseDate == new System.DateTime(2020, 01, 01) && book.proCatName == "Books"
                && book.bookISBN == "978-3-16-148410-2" && book.bookEditorial == "Editorial x"
                && book.bookEdition == "Tapa blanda" && book.bookPages == 333 && book.bookReleaseDate == new System.DateTime(2004, 07, 10));

            book = bookDao.Find(3);

            Assert.IsTrue(book.proId == 3 && book.proName == "Book 3" && book.proPrice == 15M
                && book.proReleaseDate == new System.DateTime(2022, 10, 08) && book.proCatName == "Books"
                && book.bookISBN == "978-3-16-148410-3" && book.bookEditorial == "Editorial y"
                && book.bookEdition == "De bolsillo" && book.bookPages == 182 && book.bookReleaseDate == new System.DateTime(2017, 12, 12));

            Assert.ThrowsException<System.InvalidOperationException>(() => bookDao.Find(4));

            Assert.ThrowsException<InstanceNotFoundException>(() => bookDao.Find(NON_EXISTENT_BOOK_ID));

        }

        [TestMethod()]
        public void Add()
        {

            Book newBook = new Book();

            newBook.proName = "Book 4";
            newBook.proPrice = 10M;
            newBook.proReleaseDate = new System.DateTime(2022, 10, 08);
            newBook.proStock = 1;
            newBook.proCatName = "Books";
            newBook.bookISBN = "978-3-16-148410-4";
            newBook.bookEditorial = "Editorial z";
            newBook.bookEdition = "De bolsillo";
            newBook.bookPages = 100;
            newBook.bookReleaseDate = new System.DateTime(2022, 12, 12);

            bookDao.Create(newBook);

            Book foundBook = bookDao.GetAllElements()[3];

            Assert.IsTrue(foundBook.proName == "Book 4" && foundBook.proPrice == 10M
                && foundBook.proReleaseDate == new System.DateTime(2022, 10, 08) && foundBook.proStock == 1 && foundBook.proCatName == "Books"
                && foundBook.bookISBN == "978-3-16-148410-4" && foundBook.bookEditorial == "Editorial z"
                && foundBook.bookEdition == "De bolsillo" && foundBook.bookPages == 100 && foundBook.bookReleaseDate == new System.DateTime(2022, 12, 12));

            Assert.IsTrue(foundBook.Equals(newBook));

        }

        [TestMethod()]
        public void Remove()
        {

            bookDao.Find(1);

            bookDao.Remove(1);

            Assert.ThrowsException<InstanceNotFoundException>(() => bookDao.Find(1));

        }

    }
}
