using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.UserOrderDao;
using Es.Udc.DotNet.PracticaMaD.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.Collections.Generic;
using System.Transactions;

namespace Es.Udc.DotNet.PracticaMaD.Test.DAOs
{
    [TestClass]
    public class IUserOrderDaoEntityFrameworkTest
    {
        private static IKernel kernel;
        private static IUserOrderDao userOrderDao;

        // Variables used in several tests are initialized here
        private const long NON_EXISTENT_ORDER_ID = -1;

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
            userOrderDao = kernel.Get<IUserOrderDao>();
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

            UserOrder userOrder = null;

            userOrder = userOrderDao.Find(1);

            Assert.IsTrue(userOrder.orderId == 1 && userOrder.orderDate == new System.DateTime(2022, 10, 07) 
                && userOrder.orderBankCardPAN == 1234567890000001 && userOrder.orderPostalAddress == "Order address x" 
                && userOrder.orderValue == 35 && userOrder.orderUserId == 1 && userOrder.orderDescription == "My first order");

            userOrder = userOrderDao.Find(2);

            Assert.IsTrue(userOrder.orderId == 2 && userOrder.orderDate == new System.DateTime(2022, 10, 08)
                && userOrder.orderBankCardPAN == 1234567890000001 && userOrder.orderPostalAddress == "Order address x"
                && userOrder.orderValue == 5 && userOrder.orderUserId == 1 && userOrder.orderDescription == "My second order");

            Assert.ThrowsException<InstanceNotFoundException>(() => userOrderDao.Find(NON_EXISTENT_ORDER_ID));

        }

        [TestMethod()]
        public void Add()
        {

            UserOrder newUserOrder = new UserOrder();

            newUserOrder.orderDate = new System.DateTime(2023, 01, 30);
            newUserOrder.orderBankCardPAN = 1234567890000002;
            newUserOrder.orderPostalAddress = "Test address";
            newUserOrder.orderValue = 25.75M;
            newUserOrder.orderUserId = 1;
            newUserOrder.orderDescription = "Test order";

            userOrderDao.Create(newUserOrder);

            UserOrder foundUserOrder = userOrderDao.GetAllElements()[userOrderDao.GetAllElements().Count -1];

            Assert.IsTrue(foundUserOrder.orderDate == new System.DateTime(2023, 01, 30)
                && foundUserOrder.orderBankCardPAN == 1234567890000002 && foundUserOrder.orderPostalAddress == "Test address"
                && foundUserOrder.orderValue == 25.75M && foundUserOrder.orderUserId == 1 && foundUserOrder.orderDescription == "Test order");

            Assert.IsTrue(foundUserOrder.Equals(newUserOrder));

        }

        [TestMethod()]
        public void Remove()
        {

            userOrderDao.Find(1);

            userOrderDao.Remove(1);

            Assert.ThrowsException<InstanceNotFoundException>(() => userOrderDao.Find(1));

        }

        [TestMethod()]
        public void FindAllByOrderUserId()
        {

            List<UserOrder> userUserOrders = null;

            userUserOrders = userOrderDao.FindAllByOrderUserId(1, 0, 10);

            Assert.IsTrue(userUserOrders.Count == 2);

            Assert.IsTrue(userUserOrders[0].orderId == 2 && userUserOrders[0].orderDate == new System.DateTime(2022, 10, 8)
                && userUserOrders[0].orderBankCardPAN == 1234567890000001 && userUserOrders[0].orderPostalAddress == "Order address x"
                && userUserOrders[0].orderValue == 5 && userUserOrders[0].orderUserId == 1 && userUserOrders[0].orderDescription == "My second order");

            Assert.IsTrue(userUserOrders[1].orderId == 1 && userUserOrders[1].orderDate == new System.DateTime(2022, 10, 7)
                && userUserOrders[1].orderBankCardPAN == 1234567890000001 && userUserOrders[1].orderPostalAddress == "Order address x"
                && userUserOrders[1].orderValue == 35 && userUserOrders[1].orderUserId == 1 && userUserOrders[1].orderDescription == "My first order");

            userUserOrders = userOrderDao.FindAllByOrderUserId(NON_EXISTENT_ORDER_ID, 0, 10);

            Assert.IsTrue(userUserOrders.Count == 0);

        }

    }
}
