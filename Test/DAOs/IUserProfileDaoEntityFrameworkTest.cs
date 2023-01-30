using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.Collections.Generic;
using System.Transactions;

namespace Es.Udc.DotNet.PracticaMaD.Test.DAOs
{
    [TestClass]
    public class IUserProfileDaoEntityFrameworkTest
    {
        private static IKernel kernel;
        private static IUserProfileDao userProfileDao;

        // Variables used in several tests are initialized here
        private const long NON_EXISTENT_USER_ID = -1;
        private const string NON_EXISTENT_USER_LOGINNAME = "non_existent_login_name";

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

            UserProfile userProfile = null;

            userProfile = userProfileDao.Find(1);

            Assert.IsTrue(userProfile.usrId == 1 && userProfile.loginName == "admin" 
                && userProfile.enPassword == "n4bQgYhMfWWaL+qgxVrQFaO/TxsrC4Is0V1sFbDwCgg="
                && userProfile.firstName == "User" && userProfile.lastName == "Admin" 
                && userProfile.email == "admin@admin.com" && userProfile.postalAddress == "Admin postal address"
                && userProfile.language == "en" && userProfile.country == "US" && userProfile.admin == true);

            userProfile = userProfileDao.Find(2);

            Assert.IsTrue(userProfile.usrId == 2 && userProfile.loginName == "test"
                && userProfile.enPassword == "n4bQgYhMfWWaL+qgxVrQFaO/TxsrC4Is0V1sFbDwCgg=" +
                ""
                && userProfile.firstName == "User" && userProfile.lastName == "Tester"
                && userProfile.email == "test@test.com" && userProfile.postalAddress == "Testing postal address"
                && userProfile.language == "es" && userProfile.country == "ES" && userProfile.admin == false);

            Assert.ThrowsException<InstanceNotFoundException>(() => userProfileDao.Find(NON_EXISTENT_USER_ID));

        }

        [TestMethod()]
        public void Add()
        {

            UserProfile newUserProfile = new UserProfile();

            newUserProfile.loginName = "testlogin";
            newUserProfile.enPassword = "n4bQgYhMfWWaL+qgxVrQFaO/TxsrC4Is0V1sFbDwCgg=";
            newUserProfile.firstName = "Test Name";
            newUserProfile.lastName = "Test Surname";
            newUserProfile.email = "test@email.com";
            newUserProfile.postalAddress = "Test address";
            newUserProfile.language = "en";
            newUserProfile.country = "US";
            newUserProfile.admin = false;

            userProfileDao.Create(newUserProfile);

            UserProfile foundUserProfile = userProfileDao.GetAllElements()[2];

            Assert.IsTrue(foundUserProfile.loginName == "testlogin"
                && foundUserProfile.enPassword == "n4bQgYhMfWWaL+qgxVrQFaO/TxsrC4Is0V1sFbDwCgg="
                && foundUserProfile.firstName == "Test Name" && foundUserProfile.lastName == "Test Surname"
                && foundUserProfile.email == "test@email.com" && foundUserProfile.postalAddress == "Test address"
                && foundUserProfile.language == "en" && foundUserProfile.country == "US" && foundUserProfile.admin == false);

            Assert.IsTrue(foundUserProfile.Equals(newUserProfile));

        }

        [TestMethod()]
        public void Remove()
        {

            userProfileDao.Find(1);

            userProfileDao.Remove(1);

            Assert.ThrowsException<InstanceNotFoundException>(() => userProfileDao.Find(1));

        }

        [TestMethod()]
        public void FindByLoginName()
        {

            UserProfile foundUserProfile = null;

            foundUserProfile = userProfileDao.FindByLoginName("admin");

            Assert.IsTrue(foundUserProfile.usrId == 1 && foundUserProfile.loginName == "admin"
                && foundUserProfile.enPassword == "n4bQgYhMfWWaL+qgxVrQFaO/TxsrC4Is0V1sFbDwCgg="
                && foundUserProfile.firstName == "User" && foundUserProfile.lastName == "Admin"
                && foundUserProfile.email == "admin@admin.com" && foundUserProfile.postalAddress == "Admin postal address"
                && foundUserProfile.language == "en" && foundUserProfile.country == "US" && foundUserProfile.admin == true);

            foundUserProfile = userProfileDao.FindByLoginName("test");

            Assert.IsTrue(foundUserProfile.usrId == 2 && foundUserProfile.loginName == "test"
                && foundUserProfile.enPassword == "n4bQgYhMfWWaL+qgxVrQFaO/TxsrC4Is0V1sFbDwCgg="
                && foundUserProfile.firstName == "User" && foundUserProfile.lastName == "Tester"
                && foundUserProfile.email == "test@test.com" && foundUserProfile.postalAddress == "Testing postal address"
                && foundUserProfile.language == "es" && foundUserProfile.country == "ES" && foundUserProfile.admin == false);

            Assert.ThrowsException<InstanceNotFoundException>(() => userProfileDao.FindByLoginName(NON_EXISTENT_USER_LOGINNAME));

        }

    }
}
