using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.TagDao;
using Es.Udc.DotNet.PracticaMaD.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.Collections.Generic;
using System.Transactions;

namespace Es.Udc.DotNet.PracticaMaD.Test.DAOs
{
    [TestClass]
    public class ITagDaoEntityFrameworkTest
    {
        private static IKernel kernel;
        private static ITagDao tagDao;

        // Variables used in several tests are initialized here
        private const string NON_EXISTENT_TAG_NAME = "non_existent_tag";
        private const long NON_EXISTENT_COMMENT_ID = -1;

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
            tagDao = kernel.Get<ITagDao>();
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

            Tag tag = null;

            tag = tagDao.Find("Ganga");

            Assert.IsTrue(tag.tagName == "Ganga");

            tag = tagDao.Find("Pésimo");

            Assert.IsTrue(tag.tagName == "Pésimo");

            tag = tagDao.Find("Oferta");

            Assert.IsTrue(tag.tagName == "Oferta");

            Assert.ThrowsException<InstanceNotFoundException>(() => tagDao.Find(NON_EXISTENT_TAG_NAME));

        }

        [TestMethod()]
        public void Add()
        {

            Tag newTag = new Tag();

            newTag.tagName = "TestTag";

            tagDao.Create(newTag);

            Tag foundTag = tagDao.GetAllElements()[3];

            Assert.IsTrue(foundTag.tagName == "TestTag");

            Assert.IsTrue(foundTag.Equals(newTag));

        }

        [TestMethod()]
        public void Remove()
        {

            tagDao.Find("Ganga");

            tagDao.Remove("Ganga");

            Assert.ThrowsException<InstanceNotFoundException>(() => tagDao.Find("Ganga"));

        }

        [TestMethod()]
        public void FindByTagName()
        {

            Tag foundTag = null;

            foundTag = tagDao.FindByTagName("Ganga");

            Assert.IsTrue(foundTag.tagName == "Ganga");
            Assert.ThrowsException<InstanceNotFoundException>(() => tagDao.FindByTagName(NON_EXISTENT_TAG_NAME));
        }

        [TestMethod()]
        public void FindByIdComment()
        {

            List<Tag> tags = new List<Tag>();

            tags = tagDao.FindByIdComment(1);

            Assert.IsTrue(tags.Count == 3);

            Assert.IsTrue(tags[0].tagName == "Ganga");

            Assert.IsTrue(tags[1].tagName == "Oferta");

            Assert.IsTrue(tags[2].tagName == "Pésimo");

            tags = tagDao.FindByIdComment(2);

            Assert.IsTrue(tags.Count == 1);

            Assert.IsTrue(tags[0].tagName == "Ganga");

            //tags = tagDao.FindByIdComment(NON_EXISTENT_COMMENT_ID);

            //Assert.IsTrue(tags.Count == 0);

            // TODO: Check
            Assert.ThrowsException<System.ArgumentNullException>(() => tagDao.FindByIdComment(NON_EXISTENT_COMMENT_ID));

        }

    }
}
