using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.Services.ShoppingService;
using Es.Udc.DotNet.PracticaMaD.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.Collections.Generic;
using System.Transactions;
using System;
using System.Text;

namespace Es.Udc.DotNet.PracticaMaD.Test.Services
{
    /// <summary>
    /// Descripción resumida de IShoppingServiceTest
    /// </summary>
    [TestClass]
    public class IShoppingServiceTest
    {

        private static IKernel kernel;
        private static IShoppingService shoppingService;

        // Variables used in several tests are initialized here
        private const long NON_EXISTENT_ID = -1;

        //Due to the limited precision of floating point numbers, the equality
        //operator may provide unexpected results if two numbers are close to
        //each other (e.g. 25 and 25.00000000001). In order to solve this
        //issue, a small margin of error (delta) can be allowed.
        private const double delta = 0.00001;

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

        #region Additional test attributes

        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();
            shoppingService = kernel.Get<IShoppingService>();
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

        #endregion Additional test attributes

        #region FUN 5 Tests

        [TestMethod()]
        public void CreateShoppingCart()
        {
            ShoppingCart cart = shoppingService.CreateShoppingCart();

            Assert.IsTrue(cart.cartLines.Count == 0);
            Assert.IsTrue(cart.totalPrice == 0M);
        }

        [TestMethod()]
        public void EmptyShoppingCart()
        {
            ShoppingCart cart = shoppingService.CreateShoppingCart();

            Assert.IsTrue(cart.cartLines.Count == 0);
            Assert.IsTrue(cart.totalPrice == 0M);

            shoppingService.AddProductToShoppingCart(1, cart);

            Assert.IsTrue(cart.cartLines.Count == 1);

            shoppingService.AddProductToShoppingCart(4, cart);

            Assert.IsTrue(cart.cartLines.Count == 2);

            shoppingService.EmptyShoppingCart(cart);

            Assert.IsTrue(cart.cartLines.Count == 0);
            Assert.IsTrue(cart.totalPrice == 0M);
        }
        
        [TestMethod()]
        public void ShoppingCartIsEmpty()
        {
            ShoppingCart cart = shoppingService.CreateShoppingCart();

            Assert.IsTrue(shoppingService.ShoppingCartIsEmpty(cart));

            shoppingService.AddProductToShoppingCart(1, cart);

            Assert.IsFalse(shoppingService.ShoppingCartIsEmpty(cart));

            shoppingService.EmptyShoppingCart(cart);

            Assert.IsTrue(shoppingService.ShoppingCartIsEmpty(cart));
        }

        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void AddNonExistentProductToShoppingCart()
        {
            ShoppingCart cart = shoppingService.CreateShoppingCart();

            shoppingService.AddProductToShoppingCart(NON_EXISTENT_ID, cart);
        }

        [TestMethod()]
        [ExpectedException(typeof(ProductOutOfStockException))]
        public void AddOutOfStockProductToShoppingCart()
        {
            ShoppingCart cart = shoppingService.CreateShoppingCart();

            shoppingService.EmptyShoppingCart(cart);

            shoppingService.AddProductToShoppingCart(3, cart);

            shoppingService.EmptyShoppingCart(cart);
        }

        [TestMethod()]
        public void AddProductToShoppingCart()
        {
            ShoppingCart cart = shoppingService.CreateShoppingCart();

            shoppingService.EmptyShoppingCart(cart);

            shoppingService.AddProductToShoppingCart(1, cart);

            Assert.IsFalse(shoppingService.ShoppingCartIsEmpty(cart));
            Assert.IsTrue(cart.cartLines.Count == 1);
            Assert.IsTrue(cart.totalPrice == 9.95M);

            Assert.IsTrue(cart.cartLines[0].product.proName == "Book 1");
            Assert.IsTrue(cart.cartLines[0].quantity == 1);
            Assert.IsTrue(cart.cartLines[0].gift == false);
            Assert.IsTrue(cart.cartLines[0].linePrice == 9.95M);

            shoppingService.AddProductToShoppingCart(1, cart);

            Assert.IsFalse(shoppingService.ShoppingCartIsEmpty(cart));
            Assert.IsTrue(cart.cartLines.Count == 1);
            Assert.IsTrue(cart.totalPrice == 9.95M *2);

            Assert.IsTrue(cart.cartLines[0].product.proName == "Book 1");
            Assert.IsTrue(cart.cartLines[0].quantity == 2);
            Assert.IsTrue(cart.cartLines[0].gift == false);
            Assert.IsTrue(cart.cartLines[0].linePrice == 9.95M *2);

            shoppingService.AddProductToShoppingCart(2, cart);

            Assert.IsFalse(shoppingService.ShoppingCartIsEmpty(cart));
            Assert.IsTrue(cart.cartLines.Count == 2);
            Assert.IsTrue(cart.totalPrice == 9.95M * 2 + 4.95M);

            Assert.IsTrue(cart.cartLines[1].product.proName == "Book 2");
            Assert.IsTrue(cart.cartLines[1].quantity == 1);
            Assert.IsTrue(cart.cartLines[1].gift == false);
            Assert.IsTrue(cart.cartLines[1].linePrice == 4.95M);

            shoppingService.EmptyShoppingCart(cart);
        }

        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void DeleteNonExistentProductFromShoppingCart()
        {
            ShoppingCart cart = shoppingService.CreateShoppingCart();

            shoppingService.DeleteProductFromShoppingCart(NON_EXISTENT_ID, cart);
        }
        
        [TestMethod()]
        [ExpectedException(typeof(NotInShoppingCartException))]
        public void DeleteProductNotPresentFromShoppingCart()
        {
            ShoppingCart cart = shoppingService.CreateShoppingCart();

            shoppingService.DeleteProductFromShoppingCart(1, cart);
        }
        
        [TestMethod()]
        public void DeleteProductThatWasOutOfStockFromShoppingCart()
        {
            ShoppingCart cart = shoppingService.CreateShoppingCart();

            shoppingService.AddProductToShoppingCart(2, cart);

            Assert.ThrowsException<ProductOutOfStockException>(() =>
            shoppingService.AddProductToShoppingCart(2, cart));

            shoppingService.DeleteProductFromShoppingCart(2, cart);

            shoppingService.AddProductToShoppingCart(2, cart);

            shoppingService.EmptyShoppingCart(cart);
        }

        [TestMethod()]
        public void DeleteProductFromShoppingCart()
        {
            ShoppingCart cart = shoppingService.CreateShoppingCart();

            shoppingService.AddProductToShoppingCart(1, cart);

            Assert.IsFalse(shoppingService.ShoppingCartIsEmpty(cart));

            shoppingService.DeleteProductFromShoppingCart(1, cart);

            Assert.IsTrue(shoppingService.ShoppingCartIsEmpty(cart));

            shoppingService.AddProductToShoppingCart(1, cart);
            shoppingService.AddProductToShoppingCart(1, cart);

            Assert.IsFalse(shoppingService.ShoppingCartIsEmpty(cart));

            shoppingService.DeleteProductFromShoppingCart(1, cart);

            Assert.IsTrue(shoppingService.ShoppingCartIsEmpty(cart));

            shoppingService.EmptyShoppingCart(cart);
        }
        
        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void AddOneNonExistentToShoppingCart()
        {
            ShoppingCart cart = shoppingService.CreateShoppingCart();

            shoppingService.AddOneToShoppingCart(NON_EXISTENT_ID, cart);
        }
        
        [TestMethod()]
        [ExpectedException(typeof(NotInShoppingCartException))]
        public void AddOneNotPresentToShoppingCart()
        {
            ShoppingCart cart = shoppingService.CreateShoppingCart();

            shoppingService.AddOneToShoppingCart(1, cart);

            Assert.IsTrue(shoppingService.ShoppingCartIsEmpty(cart));
        }

        [TestMethod()]
        public void AddOneOutOfStockToShoppingCart()
        {
            ShoppingCart cart = shoppingService.CreateShoppingCart();

            shoppingService.AddProductToShoppingCart(2, cart);

            Assert.ThrowsException<ProductOutOfStockException>(() =>
                        shoppingService.AddOneToShoppingCart(2, cart));

            shoppingService.EmptyShoppingCart(cart);
        }
        
        [TestMethod()]
        public void AddOneToShoppingCart()
        {
            ShoppingCart cart = shoppingService.CreateShoppingCart();

            shoppingService.AddProductToShoppingCart(1, cart);

            shoppingService.AddOneToShoppingCart(1, cart);

            Assert.IsFalse(shoppingService.ShoppingCartIsEmpty(cart));
            Assert.IsTrue(cart.cartLines.Count == 1);
            Assert.IsTrue(cart.totalPrice == 9.95M *2);

            Assert.IsTrue(cart.cartLines[0].product.proName == "Book 1");
            Assert.IsTrue(cart.cartLines[0].quantity == 2);
            Assert.IsTrue(cart.cartLines[0].gift == false);
            Assert.IsTrue(cart.cartLines[0].linePrice == 9.95M *2);

            shoppingService.EmptyShoppingCart(cart);
        }
        
        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void RemoveOneNonExistentFromShoppingCart()
        {
            ShoppingCart cart = shoppingService.CreateShoppingCart();

            shoppingService.RemoveOneFromShoppingCart(NON_EXISTENT_ID, cart);
        }

        [TestMethod()]
        [ExpectedException(typeof(NotInShoppingCartException))]
        public void RemoveOneNotPresentFromShoppingCart()
        {
            ShoppingCart cart = shoppingService.CreateShoppingCart();

            shoppingService.RemoveOneFromShoppingCart(1, cart);
        }

        [TestMethod()]
        public void RemoveOneThatWasOutOfStockFromShoppingCart()
        {
            ShoppingCart cart = shoppingService.CreateShoppingCart();

            shoppingService.AddProductToShoppingCart(2, cart);

            Assert.ThrowsException<ProductOutOfStockException>(() => 
            shoppingService.AddOneToShoppingCart(2, cart));

            shoppingService.RemoveOneFromShoppingCart(2, cart);

            shoppingService.AddProductToShoppingCart(2, cart);

            shoppingService.EmptyShoppingCart(cart);
        }
        
        [TestMethod()]
        public void RemoveOneFromShoppingCart()
        {
            ShoppingCart cart = shoppingService.CreateShoppingCart();

            shoppingService.AddProductToShoppingCart(1, cart);

            shoppingService.AddOneToShoppingCart(1, cart);

            Assert.IsFalse(shoppingService.ShoppingCartIsEmpty(cart));
            Assert.IsTrue(cart.cartLines.Count == 1);
            Assert.IsTrue(cart.totalPrice == 9.95M * 2);

            Assert.IsTrue(cart.cartLines[0].product.proName == "Book 1");
            Assert.IsTrue(cart.cartLines[0].quantity == 2);
            Assert.IsTrue(cart.cartLines[0].gift == false);
            Assert.IsTrue(cart.cartLines[0].linePrice == 9.95M * 2);

            shoppingService.RemoveOneFromShoppingCart(1, cart);

            Assert.IsTrue(cart.cartLines.Count == 1);
            Assert.IsTrue(cart.totalPrice == 9.95M);
            Assert.IsTrue(cart.cartLines[0].quantity == 1);

            shoppingService.RemoveOneFromShoppingCart(1, cart);

            Assert.IsTrue(cart.cartLines.Count == 0);
            Assert.IsTrue(cart.totalPrice == 0M);
        }
        
        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void MarkAsGiftNonExistentOnShoppingCart()
        {
            ShoppingCart cart = shoppingService.CreateShoppingCart();

            shoppingService.ToggleGiftAtShoppingCart(NON_EXISTENT_ID, cart);
        }

        [TestMethod()]
        [ExpectedException(typeof(NotInShoppingCartException))]
        public void MarkAsGiftNotPresentOnShoppingCart()
        {
            ShoppingCart cart = shoppingService.CreateShoppingCart();

            shoppingService.ToggleGiftAtShoppingCart(1, cart);
        }

        [TestMethod()]
        public void MarkAsGiftOnShoppingCart()
        {
            ShoppingCart cart = shoppingService.CreateShoppingCart();

            shoppingService.AddProductToShoppingCart(1, cart);

            shoppingService.ToggleGiftAtShoppingCart(1, cart);

            Assert.IsFalse(shoppingService.ShoppingCartIsEmpty(cart));
            Assert.IsTrue(cart.cartLines.Count == 1);

            Assert.IsTrue(cart.cartLines[0].quantity == 1);
            Assert.IsTrue(cart.cartLines[0].gift == true);

            shoppingService.ToggleGiftAtShoppingCart(1, cart);

            Assert.IsFalse(shoppingService.ShoppingCartIsEmpty(cart));
            Assert.IsTrue(cart.cartLines.Count == 1);

            Assert.IsTrue(cart.cartLines[0].quantity == 1);
            Assert.IsTrue(cart.cartLines[0].gift == false);

            shoppingService.AddOneToShoppingCart(1, cart);
            shoppingService.ToggleGiftAtShoppingCart(1, cart);

            Assert.IsFalse(shoppingService.ShoppingCartIsEmpty(cart));
            Assert.IsTrue(cart.cartLines.Count == 1);

            Assert.IsTrue(cart.cartLines[0].quantity == 2);
            Assert.IsTrue(cart.cartLines[0].gift == true);

            shoppingService.EmptyShoppingCart(cart);
        }

        #endregion FUN 5 Tests

        #region FUN 6 Tests
        
        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void BuyOrderNonExistentUser()
        {
            ShoppingCart cart = shoppingService.CreateShoppingCart();

            shoppingService.AddProductToShoppingCart(1, cart);

            shoppingService.BuyOrder(NON_EXISTENT_ID, 1234567890000001, "Test address","Test description", cart);
        }
        
        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void BuyOrderNonExistentBankCard()
        {
            ShoppingCart cart = shoppingService.CreateShoppingCart();
            
            shoppingService.AddProductToShoppingCart(1, cart);

            shoppingService.BuyOrder(2, NON_EXISTENT_ID, "Test address", "Test description", cart);
        }

        [TestMethod()]
        [ExpectedException(typeof(ShoppingCartIsEmptyException))]
        public void BuyOrderEmptyShoppingCart()
        {
            ShoppingCart cart = shoppingService.CreateShoppingCart();

            shoppingService.BuyOrder(2, 1234567890000001, "Test address", "Test description", cart);
        }

        [TestMethod()]
        public void BuyOrder()
        {
            ShoppingCart cart = shoppingService.CreateShoppingCart();

            shoppingService.AddProductToShoppingCart(5, cart);
            shoppingService.AddOneToShoppingCart(5, cart);

            shoppingService.BuyOrder(2, 1234567890000001, "Test address", "Test description 1", cart);

            shoppingService.AddProductToShoppingCart(5, cart);

            shoppingService.BuyOrder(2, 1234567890000002, "Test address", "Test description 2", cart);
        }
        
        #endregion FUN 6 Tests


    }
}
