using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace Es.Udc.DotNet.PracticaMaD.Test.DAOs
{
    [TestClass]
    public class IProductDaoEntityFrameworkTest
    {
        private static IKernel kernel;
        private static IProductDao productDao;

        // Variables used in several tests are initialized here
        private const long NON_EXISTENT_PRODUCT_ID = -1;
        private const string  NON_EXISTENT_KEYWORD = "non_existent_keyword";

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
            productDao = kernel.Get<IProductDao>();
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

            Product product = null;

            product = productDao.Find(1);

            Assert.IsTrue(product.proId == 1 && product.proName == "Book 1" && product.proPrice == 9.95M
                && product.proReleaseDate == new System.DateTime(2022, 10, 08)
                && product.proCatName == "Books");

            product = productDao.Find(2);

            Assert.IsTrue(product.proId == 2 && product.proName == "Book 2" && product.proPrice == 4.95M
                && product.proReleaseDate == new System.DateTime(2020, 01, 01)
                && product.proCatName == "Books");

            product = productDao.Find(4);

            Assert.IsTrue(product.proId == 4 && product.proName == "Music 1" && product.proPrice == 11.95M
                && product.proReleaseDate == new System.DateTime(2022, 10, 08)
                && product.proCatName == "Music");

            Assert.ThrowsException<InstanceNotFoundException>(() => productDao.Find(NON_EXISTENT_PRODUCT_ID));

        }

        [TestMethod()]
        public void Add()
        {

            Product newProduct = new Product();

            newProduct.proName = "Test product";
            newProduct.proPrice = 250;
            newProduct.proReleaseDate = new System.DateTime(1999, 01, 01);
            newProduct.proStock = 1;
            newProduct.proCatName = "Films";

            productDao.Create(newProduct);

            Product foundProduct = productDao.GetAllElements()[9];

            Assert.IsTrue(foundProduct.proName == "Test product" && foundProduct.proPrice == 250M
                && foundProduct.proReleaseDate == new System.DateTime(1999, 01, 01) && foundProduct.proStock == 1
                && foundProduct.proCatName == "Films");

            Assert.IsTrue(foundProduct.Equals(newProduct));

        }

        [TestMethod()]
        public void Remove()
        {

            productDao.Find(1);

            productDao.Remove(1);

            Assert.ThrowsException<InstanceNotFoundException>(() => productDao.Find(1));

        }

        // TODO: re-do
        /*
        [TestMethod()]
        public void FindByKeywordAndProCatId()
        {

            IQueryable<Product> foundProducts = null;

            foundProducts = productDao.FindByKeywordAndProCatName("Book", "");

            Assert.IsTrue(foundProducts.Count == 3);

            Assert.IsTrue(foundProducts[0].proId == 1 && foundProducts[0].proName == "Book 1" && foundProducts[0].proPrice == 9.95M
                && foundProducts[0].proReleaseDate == new System.DateTime(2022, 10, 08)
                && foundProducts[0].proCatName == "Books");

            Assert.IsTrue(foundProducts[1].proId == 2 && foundProducts[1].proName == "Book 2" && foundProducts[1].proPrice == 4.95M
                && foundProducts[1].proReleaseDate == new System.DateTime(2020, 01, 01)
                && foundProducts[1].proCatName == "Books");

            Assert.IsTrue(foundProducts[2].proId == 3 && foundProducts[2].proName == "Book 3" && foundProducts[2].proPrice == 15M
                && foundProducts[2].proReleaseDate == new System.DateTime(2022, 10, 08)
                && foundProducts[2].proCatName == "Books");


            foundProducts = productDao.FindByKeywordAndProCatName("book", "");

            Assert.IsTrue(foundProducts.Count == 3);

            Assert.IsTrue(foundProducts[0].proId == 1 && foundProducts[0].proName == "Book 1" && foundProducts[0].proPrice == 9.95M
                && foundProducts[0].proReleaseDate == new System.DateTime(2022, 10, 08)
                && foundProducts[0].proCatName == "Books");

            Assert.IsTrue(foundProducts[1].proId == 2 && foundProducts[1].proName == "Book 2" && foundProducts[1].proPrice == 4.95M
                && foundProducts[1].proReleaseDate == new System.DateTime(2020, 01, 01)
                && foundProducts[1].proCatName == "Books");

            Assert.IsTrue(foundProducts[2].proId == 3 && foundProducts[2].proName == "Book 3" && foundProducts[2].proPrice == 15M
                && foundProducts[2].proReleaseDate == new System.DateTime(2022, 10, 08)
                && foundProducts[2].proCatName == "Books");


            foundProducts = productDao.FindByKeywordAndProCatName("book 1", "");

            Assert.IsTrue(foundProducts.Count == 1);

            Assert.IsTrue(foundProducts[0].proId == 1 && foundProducts[0].proName == "Book 1" && foundProducts[0].proPrice == 9.95M
                && foundProducts[0].proReleaseDate == new System.DateTime(2022, 10, 08)
                && foundProducts[0].proCatName == "Books");


            foundProducts = productDao.FindByKeywordAndProCatName("", "");

            Assert.IsTrue(foundProducts.Count == 9);


            foundProducts = productDao.FindByKeywordAndProCatName("Book", "Books");

            Assert.IsTrue(foundProducts.Count == 3);

            Assert.IsTrue(foundProducts[0].proId == 1 && foundProducts[0].proName == "Book 1" && foundProducts[0].proPrice == 9.95M
                && foundProducts[0].proReleaseDate == new System.DateTime(2022, 10, 08)
                && foundProducts[0].proCatName == "Books");

            Assert.IsTrue(foundProducts[1].proId == 2 && foundProducts[1].proName == "Book 2" && foundProducts[1].proPrice == 4.95M
                && foundProducts[1].proReleaseDate == new System.DateTime(2020, 01, 01)
                && foundProducts[1].proCatName == "Books");

            Assert.IsTrue(foundProducts[2].proId == 3 && foundProducts[2].proName == "Book 3" && foundProducts[2].proPrice == 15M
                && foundProducts[2].proReleaseDate == new System.DateTime(2022, 10, 08)
                && foundProducts[2].proCatName == "Books");


            foundProducts = productDao.FindByKeywordAndProCatName("Book", "Music");

            Assert.IsTrue(foundProducts.Count == 0);


            foundProducts = productDao.FindByKeywordAndProCatName("Music", "Books");

            Assert.IsTrue(foundProducts.Count == 0);


            foundProducts = productDao.FindByKeywordAndProCatName(NON_EXISTENT_KEYWORD, "");

            Assert.IsTrue(foundProducts.Count == 0);

        }
        */

    }
}
