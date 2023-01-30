using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.BankCardTypeDao;
using Es.Udc.DotNet.PracticaMaD.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.Collections.Generic;
using System.Transactions;

namespace Es.Udc.DotNet.PracticaMaD.Test.DAOs
{
    [TestClass]
    public class IBankCardTypeDaoEntityFrameworkTest
    {
        private static IKernel kernel;
        private static IBankCardTypeDao bankCardTypeDao;

        // Variables used in several tests are initialized here
        private const long NON_EXISTENT_TYPE_ID = -1;

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
            bankCardTypeDao = kernel.Get<IBankCardTypeDao>();
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

            BankCardType bankCardType = null;

            bankCardType = bankCardTypeDao.Find(1);

            Assert.IsTrue(bankCardType.typeName == "Credit");

            bankCardType = bankCardTypeDao.Find(2);

            Assert.IsTrue(bankCardType.typeName == "Debit");

            bankCardType = bankCardTypeDao.Find(3);

            Assert.IsTrue(bankCardType.typeName == "PayPal");

            Assert.ThrowsException<InstanceNotFoundException>(() => bankCardTypeDao.Find(NON_EXISTENT_TYPE_ID));

        }

        [TestMethod()]
        public void GetAllElements()
        {

            List<BankCardType> bankCardTypes = new List<BankCardType>();

            Assert.IsTrue(bankCardTypes.Count == 0);

            bankCardTypes = bankCardTypeDao.GetAllElements();

            Assert.IsTrue(bankCardTypes.Count == 3);

            Assert.IsTrue(bankCardTypes[0].typeName == "Credit");

            Assert.IsTrue(bankCardTypes[1].typeName == "Debit");

            Assert.IsTrue(bankCardTypes[2].typeName == "PayPal");

        }

        [TestMethod()]
        public void Add()
        {

            List<BankCardType> foundBankCardTypes = bankCardTypeDao.GetAllElements();

            Assert.IsTrue(foundBankCardTypes.Count == 3);

            BankCardType newBankCardType = new BankCardType();

            newBankCardType.typeName = "TestType";

            bankCardTypeDao.Create(newBankCardType);

            foundBankCardTypes = bankCardTypeDao.GetAllElements();

            Assert.IsTrue(foundBankCardTypes.Count == 4);

            Assert.IsTrue(foundBankCardTypes[3].typeName == "TestType");

            Assert.IsTrue(foundBankCardTypes[3].Equals(newBankCardType));

        }

        [TestMethod()]
        public void Remove()
        {

            List<BankCardType> foundBankCardTypes = bankCardTypeDao.GetAllElements();

            Assert.IsTrue(foundBankCardTypes.Count == 3);

            bankCardTypeDao.Remove(1);

            foundBankCardTypes = bankCardTypeDao.GetAllElements();

            Assert.IsTrue(foundBankCardTypes.Count == 2);

            bankCardTypeDao.Remove(2);

            foundBankCardTypes = bankCardTypeDao.GetAllElements();

            Assert.IsTrue(foundBankCardTypes.Count == 1);

            Assert.IsTrue(foundBankCardTypes[0].typeName == "PayPal");

            bankCardTypeDao.Remove(3);

            foundBankCardTypes = bankCardTypeDao.GetAllElements();

            Assert.IsTrue(foundBankCardTypes.Count == 0);

            Assert.ThrowsException<InstanceNotFoundException>(() => bankCardTypeDao.Remove(NON_EXISTENT_TYPE_ID));

        }

    }
}
