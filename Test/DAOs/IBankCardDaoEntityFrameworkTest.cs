using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.BankCardDao;
using Es.Udc.DotNet.PracticaMaD.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.Collections.Generic;
using System.Transactions;

namespace Es.Udc.DotNet.PracticaMaD.Test.DAOs
{
    [TestClass]
    public class IBankCardDaoEntityFrameworkTest
    {
        private static IKernel kernel;
        private static IBankCardDao bankCardDao;

        // Variables used in several tests are initialized here
        private const long NON_EXISTENT_CARD_ID = -1;
        private const long NON_EXISTENT_USER_ID = -1;

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
            bankCardDao = kernel.Get<IBankCardDao>();
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

            BankCard bankCard = null;

            bankCard = bankCardDao.Find(1234567890000001);

            Assert.IsTrue(bankCard.cardPAN == 1234567890000001 && bankCard.cardTypeId == 1 && bankCard.cardCvv == 123 
                && bankCard.cardExpirationDate == new System.DateTime(2024, 01, 01) && bankCard.cardDefault == true 
                && bankCard.cardOwnerId == 1);

            bankCard = bankCardDao.Find(1234567890000002);

            Assert.IsTrue(bankCard.cardPAN == 1234567890000002 && bankCard.cardTypeId == 2 && bankCard.cardCvv == 555
                && bankCard.cardExpirationDate == new System.DateTime(2023, 04, 10) && bankCard.cardDefault == false
                && bankCard.cardOwnerId == 1);

            Assert.ThrowsException<InstanceNotFoundException>(() => bankCardDao.Find(NON_EXISTENT_CARD_ID));
            
        }

        [TestMethod()]
        public void Add()
        {

            BankCard newBankCard = new BankCard();

            newBankCard.cardPAN = 1234567899999999;
            newBankCard.cardTypeId = 3;
            newBankCard.cardCvv = 014;
            newBankCard.cardExpirationDate = new System.DateTime(2030, 02, 03);
            newBankCard.cardDefault = false;
            newBankCard.cardOwnerId = 1;

            bankCardDao.Create(newBankCard);

            BankCard foundBankCard = bankCardDao.Find(1234567899999999);

            Assert.IsTrue(foundBankCard.cardPAN == 1234567899999999 && foundBankCard.cardTypeId == 3 && foundBankCard.cardCvv == 014
                && foundBankCard.cardExpirationDate == new System.DateTime(2030, 02, 03) && foundBankCard.cardDefault == false
                && foundBankCard.cardOwnerId == 1);

            Assert.IsTrue(foundBankCard.Equals(newBankCard));

        }

        [TestMethod()]
        public void Remove()
        {

            bankCardDao.Find(1234567890000001);

            bankCardDao.Remove(1234567890000001);

            Assert.ThrowsException<InstanceNotFoundException>(() => bankCardDao.Find(1234567890000001));

        }

        [TestMethod()]
        public void FindAllByCardOwnerId()
        {

            List<BankCard> userBankCards = null;

            userBankCards = bankCardDao.FindAllByCardOwnerId(1);

            Assert.IsTrue(userBankCards.Count == 2);

            Assert.IsTrue(userBankCards[0].cardOwnerId == 1);

            Assert.IsTrue(userBankCards[1].cardOwnerId == 1);

            userBankCards = bankCardDao.FindAllByCardOwnerId(NON_EXISTENT_USER_ID);

            Assert.IsTrue(userBankCards.Count == 0);

        }

        [TestMethod()]
        public void FindDefaultCardByCardOwnerId()
        {

            BankCard defaultBankCard = null;

            defaultBankCard = bankCardDao.FindDefaultCardByCardOwnerId(1);

            Assert.IsTrue(defaultBankCard.cardPAN == 1234567890000001 && defaultBankCard.cardTypeId == 1 && defaultBankCard.cardCvv == 123
                && defaultBankCard.cardExpirationDate == new System.DateTime(2024, 01, 01) && defaultBankCard.cardDefault == true
                && defaultBankCard.cardOwnerId == 1);

            Assert.ThrowsException<InstanceNotFoundException>(() => bankCardDao.FindDefaultCardByCardOwnerId(NON_EXISTENT_USER_ID));

        }

    }
}
