using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Es.Udc.DotNet.PracticaMaD.Model.Services.UserService;
using Es.Udc.DotNet.PracticaMaD.Model.Services.ShoppingService;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.UserOrderDao;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.BankCardDao;
using System.Transactions;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.Exceptions;
using Ninject;
using Es.Udc.DotNet.PracticaMaD.Model.Services.UserService.Util;
using Es.Udc.DotNet.PracticaMaD.Model;

namespace Es.Udc.DotNet.PracticaMaD.Test.Services
{
    /// <summary>
    /// Descripción resumida de IUserServiceTest
    /// </summary>
    [TestClass]
    public class IUserServiceTest
    {

        private static IKernel kernel;
        private static IUserService userService;
        private static IBankCardDao bankCardDao;
        private static IUserProfileDao userProfileDao;
        private static IShoppingService shoppingService;

        private const long NON_EXISTENT_USER_ID = -1;
        private const long NON_EXISTENT_ORDER_ID = -1;
        private const long NON_EXISTENT_CARD_ID = -1;
        private TransactionScope transactionScope;
        private TestContext testContextInstance;

        private const string postalAddress = "postalAddress";
        private const string clearPassword = "password";
        private const string firstName = "name";
        private const string lastName = "lastName";
        private const string email = "enc@udc.es";
        private const string language = "es";
        private const string country = "ES";

        /// <summary>
        ///Obtiene o establece el contexto de las pruebas que proporciona
        ///información y funcionalidad para la serie de pruebas actual.
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
            userService = kernel.Get<IUserService>();
            shoppingService = kernel.Get<IShoppingService>();
            bankCardDao = kernel.Get<IBankCardDao>();
            userProfileDao = kernel.Get<IUserProfileDao>();
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

        // Use TestCleanup para ejecutar el código una vez ejecutadas todas las pruebas
        [TestCleanup()]
        public void MyTestCleanup()
        {
            transactionScope.Dispose();
        }

        #endregion Additional test atributes

        /// <summary>
        /// A test for ChangePassword
        /// </summary>
        [TestMethod]
        public void ChangePasswordTest()
        {
            var user = userService.RegisterUser("loginChange", clearPassword,
                new UserProfileDetails(firstName, lastName, "change@udc.es", postalAddress, language, country, true));

            var newClearPassword = "passwordX";
            userService.ChangePassword(user, clearPassword, newClearPassword);

            userService.Login("loginChange", newClearPassword, false);
        }

        /// <summary>
        /// A test for ChangePassword entering a wrong old password
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(IncorrectPasswordException))]
        public void ChangePasswordWithIncorrectPasswordTest()
        {
            var user = userService.RegisterUser("loginNotChange", clearPassword,
                new UserProfileDetails(firstName, lastName, "incorrect@udc.es", postalAddress, language, country, true));

            var newClearPassword = "passwordX";
            userService.ChangePassword(user, clearPassword + "Y", newClearPassword);
        }

        /// <summary>
        /// A test for ChangePassword when the user does not exist
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void ChangePasswordForNonExistingUserTest()
        {
            userService.ChangePassword(NON_EXISTENT_USER_ID, "password", "passwordX");
        }

        [TestMethod]
        public void FindUserProfileDetailsTest()
        {
            var expected = new UserProfileDetails(firstName, lastName, "findUserProfile@udc.es", postalAddress, language, country, false);
            var user = userService.RegisterUser("loginProfile", clearPassword, expected);
            var obtained = userService.FindUserProfileDetails(user);

            //check data
            Assert.AreEqual(expected.FirstName, obtained.FirstName);
            Assert.AreEqual(expected.LastName, obtained.LastName);
            Assert.AreEqual(expected.Email, obtained.Email);
            Assert.AreEqual(expected.PostalAddress, obtained.PostalAddress);
            Assert.AreEqual(expected.Language, obtained.Language);
            Assert.AreEqual(expected.Country, obtained.Country);
            Assert.AreEqual(expected.Admin, obtained.Admin);
            Assert.AreEqual(expected, obtained);
        }

        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void FindUserProfileDetailsNotExistingUserTest()
        {
            userService.FindUserProfileDetails(NON_EXISTENT_USER_ID);
        }

        ///// <summary>
        /////A test for Login with clear password
        /////</summary>
        [TestMethod]
        public void LoginClearPasswordTest()
        {
            var user = userService.RegisterUser("loginClear", clearPassword,
                new UserProfileDetails(firstName, lastName, email, postalAddress, language, country, false));

            var expected = new LoginResult(user, PasswordEncrypter.Crypt(clearPassword), firstName, lastName, email, postalAddress, language, country, false);

            var actual = userService.Login("loginClear", clearPassword, false);

            // Check data
            Assert.AreEqual(expected.FirstName, actual.FirstName);
            Assert.AreEqual(expected.LastName, actual.LastName);
            Assert.AreEqual(expected.Email, actual.Email);
            Assert.AreEqual(expected.PostalAddress, actual.PostalAddress);
            Assert.AreEqual(expected.Language, actual.Language);
            Assert.AreEqual(expected.Country, actual.Country);
            Assert.AreEqual(expected.EnPassword, actual.EnPassword);
            Assert.AreEqual(expected, actual);
        }

        ///// <summary>
        /////A test for Login with encrypted password
        /////</summary>
        [TestMethod]
        public void LoginEncryptedPasswordTest()
        {
            var user = userService.RegisterUser("loginEnc", clearPassword,
                new UserProfileDetails(firstName, lastName, email, postalAddress, language, country, false));

            var expected = new LoginResult(user, PasswordEncrypter.Crypt(clearPassword), firstName, lastName, email, postalAddress, language, country, false);

            var obtained = userService.Login("loginEnc", clearPassword, false);

            // Check data
            Assert.AreEqual(expected.FirstName, obtained.FirstName);
            Assert.AreEqual(expected.LastName, obtained.LastName);
            Assert.AreEqual(expected.Email, obtained.Email);
            Assert.AreEqual(expected.PostalAddress, obtained.PostalAddress);
            Assert.AreEqual(expected.Language, obtained.Language);
            Assert.AreEqual(expected.Country, obtained.Country);
            Assert.AreEqual(expected.EnPassword, obtained.EnPassword);
            Assert.AreEqual(expected, obtained);
        }

        ///// <summary>
        /////A test for Login with incorrect password
        /////</summary>
        [TestMethod]
        [ExpectedException(typeof(IncorrectPasswordException))]
        public void LoginIncorrectPasswordTest()
        {
            var user = userService.RegisterUser("loginInc", clearPassword,
                new UserProfileDetails(firstName, lastName, "inc@udc.es", postalAddress, language, country, true));

            var actual = userService.Login("loginInc", "passwordX", false);
        }

        ///// <summary>
        /////A test for Login with a non-existing user
        /////</summary>
        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void LoginNonExistingUserTest()
        {
            var actual = userService.Login("loginNameTestX", "password", false);
        }

        [TestMethod]
        public void RegisterUserTest()
        {
            var user = userService.RegisterUser("loginNameTester", clearPassword,
                new UserProfileDetails(firstName, lastName, "rec@udc.es", postalAddress, language, country, true));

            var userProfile = userProfileDao.Find(user);

            //check data
            Assert.AreEqual(user, userProfile.usrId);
            Assert.AreEqual("loginNameTester", userProfile.loginName);
            Assert.AreEqual(PasswordEncrypter.Crypt("password"), userProfile.enPassword);
            Assert.AreEqual("name", userProfile.firstName);
            Assert.AreEqual("lastName", userProfile.lastName);
            Assert.AreEqual("rec@udc.es", userProfile.email);
            Assert.AreEqual("postalAddress", userProfile.postalAddress);
            Assert.AreEqual("es", userProfile.language);
            Assert.AreEqual("ES", userProfile.country);
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateInstanceException))]
        public void RegisterDuplicatedUserTest()
        {
            userService.RegisterUser("loginNameDuplicated", clearPassword,
                new UserProfileDetails(firstName, lastName, "duplicated@udc.es", postalAddress, language, country, true));

            userService.RegisterUser("loginNameDuplicated", clearPassword,
                new UserProfileDetails(firstName, lastName, "notduplicated@udc.es", postalAddress, language, country, true));
        }

        /// <summary>
        /// A test for UpdateUserProfileDetails
        /// </summary>
        [TestMethod]
        public void UpdateUserProfileDetailsTest()
        {
            var user = userService.RegisterUser("loginUpdate", clearPassword,
                new UserProfileDetails(firstName, lastName, "update@udc.es", postalAddress, language, country, true));

            var expected = new UserProfileDetails("nameX", "lastNameX",
                    "user@udc.es" + "X", "postalAddres" + "1", "XX", "XX", false);

            userService.UpdateUserProfileDetails(user, expected);

            var obtained = userService.FindUserProfileDetails(user);

            //Check changes
            Assert.AreEqual(expected.FirstName, obtained.FirstName);
            Assert.AreEqual(expected.LastName, obtained.LastName);
            Assert.AreEqual(expected.Email, obtained.Email);
            Assert.AreEqual(expected.PostalAddress, obtained.PostalAddress);
            Assert.AreEqual(expected.Language, obtained.Language);
            Assert.AreEqual(expected.Country, obtained.Country);
            Assert.AreEqual(expected.Admin, obtained.Admin);
            Assert.AreEqual(expected, obtained);
        }

        /// <summary>
        /// A test for UpdateUserProfileDetails when the user does not exist
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void UpdateUserProfileDetailsForNonExistingUserTest()
        {
            userService.UpdateUserProfileDetails(NON_EXISTENT_USER_ID,
                                new UserProfileDetails("name", "lastName", "user@udc.es", "postalAddres", "es", "ES", true));
        }

        /// <summary>
        /// A test to check if a valid user loginName is found
        /// </summary>
        [TestMethod]
        public void UserExistsForValidUserTest()
        {
            bool userExists = userService.UserExists("test");

            //Check
            Assert.IsTrue(userExists);
        }

        /// <summary>
        /// A test to check if a not valid user loginame is found
        /// </summary>
        [TestMethod]
        public void UserExistsForNotValidUserTest()
        {
            String invalidLoginName = "loginName" + "_fakeUser";

            bool userExists = userService.UserExists(invalidLoginName);

            //Check
            Assert.IsFalse(userExists);
        }

        [TestMethod]
        public void FindUserBankCardsTest()
        {
            var obtained = userService.FindUserBankCards(1);
            var date1 = new DateTime(2023,04,10);
            var date2 = new DateTime(2024, 01, 01);

            Assert.AreEqual(obtained.Count, 2);

            Assert.IsTrue(obtained[0].cardPAN == 1234567890000001);
            Assert.IsTrue(obtained[0].cardTypeId == 1);
            Assert.IsTrue(obtained[0].cardCvv == 123);
            Assert.IsTrue(obtained[0].cardExpirationDate == date2);
            Assert.IsTrue(obtained[0].cardDefault == false);
            Assert.IsTrue(obtained[0].cardOwnerId == 1);

            Assert.IsTrue(obtained[1].cardPAN == 1234567890000002);
            Assert.IsTrue(obtained[1].cardTypeId == 2);
            Assert.IsTrue(obtained[1].cardCvv == 555);
            Assert.IsTrue(obtained[1].cardExpirationDate == date1);
            Assert.IsTrue(obtained[1].cardDefault == false);
            Assert.IsTrue(obtained[1].cardOwnerId == 1);
        }

        [TestMethod]
        public void FindBankCardDetailsTest()
        {
            DateTime date = new DateTime(2024, 6, 10);

            var expected = new BankCardDetails(1, 917, date, true, 1);
            var cardPAN = userService.AddBankCard(1, 98198198, expected);
            var obtained = userService.FindBankCardDetails(cardPAN);

            //Check data
            Assert.AreEqual(expected.CardTypeId, obtained.CardTypeId);
            Assert.AreEqual(expected.CardCvv, obtained.CardCvv);
            Assert.AreEqual(expected.CardExpirationDate, obtained.CardExpirationDate);
            Assert.AreEqual(expected.CardDefault, obtained.CardDefault);
            Assert.AreEqual(expected.CardOwnerId, obtained.CardOwnerId);
            Assert.AreEqual(expected, obtained);
        }

        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void FindBankCardDetailsNotExistingPANTest()
        {
            userService.FindUserOrderDetails(NON_EXISTENT_CARD_ID);
        }

        [TestMethod]
        public void AddBankCardTest()
        {
            DateTime date = new DateTime(2024, 6, 10);

            var expected = new BankCardDetails(1, 917, date, true, 1);
            var cardId = userService.AddBankCard(1, 98468348168, expected);

            var bankCard = bankCardDao.Find(cardId);

            Assert.AreEqual(bankCard.cardTypeId, 1);
            Assert.AreEqual(bankCard.cardCvv, 917);
            Assert.AreEqual(bankCard.cardExpirationDate, date);
            Assert.AreEqual(bankCard.cardDefault, true);
            Assert.AreEqual(bankCard.cardOwnerId, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateInstanceException))]
        public void AddDuplicatedBankCardTest()
        {
            DateTime date = new DateTime(2024, 6, 10);

            userService.AddBankCard(1, 989198465197,
                                new BankCardDetails(1, 917, date, true, 1));

            userService.AddBankCard(1, 989198465197,
                new BankCardDetails(1, 917, date, true, 1));
        }

        [TestMethod]
        public void UpdateBankCardDetailsTest()
        {
            DateTime date = new DateTime(2024, 6, 10);

            var cardId = userService.AddBankCard(1, 184984984984,
                                new BankCardDetails(1, 917, date, true, 1));

            var expected = new BankCardDetails(1, 946, date, false, 1);

            userService.UpdateBankCardDetails(cardId, expected);
            
            var obtained = userService.FindBankCardDetails(cardId);

            //Check changes
            Assert.AreEqual(expected.CardTypeId, obtained.CardTypeId);
            Assert.AreEqual(expected.CardCvv, obtained.CardCvv);
            Assert.AreEqual(expected.CardExpirationDate, obtained.CardExpirationDate);
            Assert.AreEqual(expected.CardDefault, obtained.CardDefault);
            Assert.AreEqual(expected.CardOwnerId, obtained.CardOwnerId);
            Assert.AreEqual(expected, obtained);
        }

        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void UpdateBankCardDetailsForNonExistingCardTest()
        {
            DateTime date = new DateTime(2024, 6, 10);

            userService.UpdateBankCardDetails(NON_EXISTENT_CARD_ID,
                                new BankCardDetails(1, 917, date, true, 1));
        }

        [TestMethod]
        public void BankCardExistsForValidCardTest()
        {
            DateTime date = new DateTime(2024, 6, 10);
            var cardPAN = 911198464435;

            userService.AddBankCard(1, cardPAN,
                new BankCardDetails(1, 917, date, true, 1));

            bool bankCardExists = userService.BankCardExists(cardPAN);

            //Check
            Assert.IsTrue(bankCardExists);
        }

        [TestMethod]
        public void BankCardExistsForNotValidCardTest()
        {
            bool bankCardExists = userService.BankCardExists(NON_EXISTENT_CARD_ID);

            //Check
            Assert.IsFalse(bankCardExists);
        }

        [TestMethod]
        public void FindAllUserOrdersTest()
        {
            var date1 = new DateTime(2022, 10, 7);
            var date2 = new DateTime(2022, 10, 8);
            var obtained = userService.FindUserOrders(1, 0, 10);

            Assert.AreEqual(obtained.Orders.Count, 2);

            Assert.IsTrue(obtained.Orders[0].orderDate == date2);
            Assert.IsTrue(obtained.Orders[0].orderBankCardPAN == 1234567890000001);
            Assert.IsTrue(obtained.Orders[0].orderPostalAddress == "Order address x");
            Assert.IsTrue(obtained.Orders[0].orderValue == 5);
            Assert.IsTrue(obtained.Orders[0].orderUserId == 1);
            Assert.IsTrue(obtained.Orders[0].orderDescription == "My second order");

            Assert.IsTrue(obtained.Orders[1].orderDate == date1);
            Assert.IsTrue(obtained.Orders[1].orderBankCardPAN == 1234567890000001);
            Assert.IsTrue(obtained.Orders[1].orderPostalAddress == "Order address x");
            Assert.IsTrue(obtained.Orders[1].orderValue == 35);
            Assert.IsTrue(obtained.Orders[1].orderUserId == 1);
            Assert.IsTrue(obtained.Orders[1].orderDescription == "My first order");
        }

        [TestMethod]
        public void FindUserOrderDetailsTest()
        {
            var dateAfter = new DateTime(2022, 12, 25);
            var dateBefore = new DateTime(2022, 12, 14);

            ShoppingCart cart = shoppingService.CreateShoppingCart();

            var user = userService.RegisterUser("loginFind", clearPassword,
                new UserProfileDetails(firstName, lastName, "findUserOrderDetails@udc.es", postalAddress, language, country, true));

            shoppingService.EmptyShoppingCart(cart);
            shoppingService.AddProductToShoppingCart(1, cart);

            var expected = new UserOrderDetails(-1, new DateTime(2022, 12, 12), 1234567890000001, "postalAddres", Convert.ToDecimal(9.95) , user, "Description"); 
            var orderId = shoppingService.BuyOrder(user, 1234567890000001, "postalAddres", "Description", cart);
            var obtained = userService.FindUserOrderDetails(orderId);

            //Check data
            Assert.IsTrue(obtained.OrderDate > dateBefore);
            Assert.IsTrue(obtained.OrderDate < dateAfter);
            Assert.AreEqual(expected.OrderBankCardPAN, obtained.OrderBankCardPAN);
            Assert.AreEqual(expected.OrderPostalAddress, obtained.OrderPostalAddress);
            Assert.AreEqual(expected.OrderValue, obtained.OrderValue);
            Assert.AreEqual(expected.OrderUserId, obtained.OrderUserId);
            Assert.AreEqual(expected.OrderDescription, obtained.OrderDescription);
        }

        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void FindUserOrderDetailsNotExistingOrderTest()
        {
            userService.FindUserOrderDetails(NON_EXISTENT_ORDER_ID);
        }

        [TestMethod]
        public void FindAllBankCardTypesTest()
        {
            List<BankCardType> types = userService.FindAllBankCardTypes();
            Assert.IsTrue(types.Count == 3);
        }

    }
}
