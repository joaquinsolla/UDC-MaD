using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.OrderLineDao;
using Es.Udc.DotNet.PracticaMaD.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.Collections.Generic;
using System.Transactions;

namespace Es.Udc.DotNet.PracticaMaD.Test.DAOs
{
    [TestClass]
    public class IOrderLineDaoEntityFrameworkTest
    {
        private static IKernel kernel;
        private static IOrderLineDao orderLineDao;

        // Variables used in several tests are initialized here
        private const long NON_EXISTENT_LINE_ID = -1;
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
            orderLineDao = kernel.Get<IOrderLineDao>();
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

            OrderLine orderLine = null;

            orderLine = orderLineDao.Find(1);

            Assert.IsTrue(orderLine.lineId == 1 && orderLine.lineOrderId == 1 && orderLine.lineProductId == 3
                && orderLine.lineUnitaryPrice == 15 && orderLine.lineQuantity == 2);

            orderLine = orderLineDao.Find(2);

            Assert.IsTrue(orderLine.lineId == 2 && orderLine.lineOrderId == 1 && orderLine.lineProductId == 6
                && orderLine.lineUnitaryPrice == 5 && orderLine.lineQuantity == 1);

            orderLine = orderLineDao.Find(3);

            Assert.IsTrue(orderLine.lineId == 3 && orderLine.lineOrderId == 2 && orderLine.lineProductId == 6
                && orderLine.lineUnitaryPrice == 5 && orderLine.lineQuantity == 1);

            Assert.ThrowsException<InstanceNotFoundException>(() => orderLineDao.Find(NON_EXISTENT_LINE_ID));

        }

        [TestMethod()]
        public void Add()
        {

            OrderLine newOrderLine = new OrderLine();

            newOrderLine.lineOrderId = 2;
            newOrderLine.lineProductId = 3;
            newOrderLine.lineUnitaryPrice = 100.5M;
            newOrderLine.lineQuantity = 20;

            orderLineDao.Create(newOrderLine);

            OrderLine foundOrderLine = orderLineDao.GetAllElements()[orderLineDao.GetAllElements().Count -1];

            Assert.IsTrue(foundOrderLine.lineProductId == 3
                && foundOrderLine.lineUnitaryPrice == 100.5M && foundOrderLine.lineQuantity == 20);

            Assert.IsTrue(foundOrderLine.Equals(newOrderLine));

        }

        [TestMethod()]
        public void Remove()
        {

            orderLineDao.Find(1);

            orderLineDao.Remove(1);

            Assert.ThrowsException<InstanceNotFoundException>(() => orderLineDao.Find(1));

        }

        [TestMethod()]
        public void FindAllByLineOrderId()
        {

            List<OrderLine> userOrderLines = null;

            userOrderLines = orderLineDao.FindAllByLineOrderId(1);

            Assert.IsTrue(userOrderLines.Count == 2);

            Assert.IsTrue(userOrderLines[0].lineId == 1 && userOrderLines[0].lineOrderId == 1 && userOrderLines[0].lineProductId == 3
                && userOrderLines[0].lineUnitaryPrice == 15 && userOrderLines[0].lineQuantity == 2);

            Assert.IsTrue(userOrderLines[1].lineId == 2 && userOrderLines[1].lineOrderId == 1 && userOrderLines[1].lineProductId == 6
                && userOrderLines[1].lineUnitaryPrice == 5 && userOrderLines[1].lineQuantity == 1);

            userOrderLines = orderLineDao.FindAllByLineOrderId(NON_EXISTENT_ORDER_ID);

            Assert.IsTrue(userOrderLines.Count == 0);

        }

    }
}
