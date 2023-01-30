using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.Collections.Generic;
using System.Transactions;

namespace Es.Udc.DotNet.PracticaMaD.Test.DAOs
{
    [TestClass]
    public class ICategoryDaoEntityFrameworkTest
    {
        private static IKernel kernel;
        private static ICategoryDao categoryDao;

        // Variables used in several tests are initialized here
        private const string NON_EXISTENT_CATEGORY_NAME = "non_existent_category";

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
            categoryDao = kernel.Get<ICategoryDao>();
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

            Category category = null;

            category = categoryDao.Find("Books");

            Assert.IsTrue(category.catName == "Books");

            category = categoryDao.Find("Music");

            Assert.IsTrue(category.catName == "Music");

            category = categoryDao.Find("Films");

            Assert.IsTrue(category.catName == "Films");

            Assert.ThrowsException<InstanceNotFoundException>(() => categoryDao.Find(NON_EXISTENT_CATEGORY_NAME));

        }

        [TestMethod()]
        public void GetAllElements()
        {

            List<Category> categories = new List<Category>();

            Assert.IsTrue(categories.Count == 0);

            categories = categoryDao.GetAllElements();

            Assert.IsTrue(categories.Count == 3);

            Assert.IsTrue(categories[0].catName == "Books");

            Assert.IsTrue(categories[2].catName == "Music");

            Assert.IsTrue(categories[1].catName == "Films");

        }

        [TestMethod()]
        public void Add()
        {

            List<Category> foundCategories = categoryDao.GetAllElements();

            Assert.IsTrue(foundCategories.Count == 3);

            Category newCategory = new Category();

            newCategory.catName = "TestCategory";

            categoryDao.Create(newCategory);

            foundCategories = categoryDao.GetAllElements();

            Assert.IsTrue(foundCategories.Count == 4);

            Assert.IsTrue(foundCategories[3].catName == "TestCategory");

            Assert.IsTrue(foundCategories[3].Equals(newCategory));

        }

        [TestMethod()]
        public void Remove()
        {

            List<Category> foundCategories = categoryDao.GetAllElements();

            Assert.IsTrue(foundCategories.Count == 3);

            categoryDao.Remove("Books");

            foundCategories = categoryDao.GetAllElements();

            Assert.IsTrue(foundCategories.Count == 2);

            categoryDao.Remove("Music");

            foundCategories = categoryDao.GetAllElements();

            Assert.IsTrue(foundCategories.Count == 1);

            Assert.IsTrue(foundCategories[0].catName == "Films");

            categoryDao.Remove("Films");

            foundCategories = categoryDao.GetAllElements();

            Assert.IsTrue(foundCategories.Count == 0);

            Assert.ThrowsException<InstanceNotFoundException>(() => categoryDao.Remove(NON_EXISTENT_CATEGORY_NAME));

        }

    }
}
