using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.FilmDao;
using Es.Udc.DotNet.PracticaMaD.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.Collections.Generic;
using System.Transactions;

namespace Es.Udc.DotNet.PracticaMaD.Test.DAOs
{
    [TestClass]
    public class IFilmDaoEntityFrameworkTest
    {
        private static IKernel kernel;
        private static IFilmDao filmDao;

        // Variables used in several tests are initialized here
        private const long NON_EXISTENT_FILM_ID = -1;

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
            filmDao = kernel.Get<IFilmDao>();
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

            Film film = null;

            film = filmDao.Find(7);

            Assert.IsTrue(film.proId == 7 && film.proName == "Film 1" && film.proPrice == 1.99M
                && film.proReleaseDate == new System.DateTime(2022, 10, 08) && film.proCatName == "Films"
                && film.filmDirector == "Director x" && film.filmGenre == "Thriller"
                && film.filmRating == 5 && film.filmDurationMins == 120 && film.filmReleaseDate == new System.DateTime(2001, 08, 05));

            film = filmDao.Find(8);

            Assert.IsTrue(film.proId == 8 && film.proName == "Film 2" && film.proPrice == 1.99M
                && film.proReleaseDate == new System.DateTime(2020, 01, 01) && film.proCatName == "Films"
                && film.filmDirector == "Director y" && film.filmGenre == "Horror"
                && film.filmRating == 9 && film.filmDurationMins == 115 && film.filmReleaseDate == new System.DateTime(2004, 07, 10));

            film = filmDao.Find(9);

            Assert.IsTrue(film.proId == 9 && film.proName == "Film 3" && film.proPrice == 2.99M
                && film.proReleaseDate == new System.DateTime(2022, 10, 08) && film.proCatName == "Films"
                && film.filmDirector == "Director z" && film.filmGenre == "Comedy"
                && film.filmRating == 10 && film.filmDurationMins == 105 && film.filmReleaseDate == new System.DateTime(2017, 12, 12));

            Assert.ThrowsException<System.InvalidOperationException>(() => filmDao.Find(4));

            Assert.ThrowsException<InstanceNotFoundException>(() => filmDao.Find(NON_EXISTENT_FILM_ID));

        }

        [TestMethod()]
        public void Add()
        {

            Film newFilm = new Film();

            newFilm.proName = "Film 4";
            newFilm.proPrice = 10M;
            newFilm.proReleaseDate = new System.DateTime(2022, 10, 08);
            newFilm.proStock = 1;
            newFilm.proCatName = "Films";
            newFilm.filmDirector = "Test director";
            newFilm.filmGenre = "Test genre";
            newFilm.filmRating = 2;
            newFilm.filmDurationMins = 99;
            newFilm.filmReleaseDate = new System.DateTime(2022, 12, 12);

            filmDao.Create(newFilm);

            Film foundFilm = filmDao.GetAllElements()[3];

            Assert.IsTrue(foundFilm.proName == "Film 4" && foundFilm.proPrice == 10M
                && foundFilm.proReleaseDate == new System.DateTime(2022, 10, 08) && foundFilm.proStock == 1 && foundFilm.proCatName == "Films"
                && foundFilm.filmDirector == "Test director" && foundFilm.filmGenre == "Test genre"
                && foundFilm.filmRating == 2 && foundFilm.filmDurationMins == 99 && foundFilm.filmReleaseDate == new System.DateTime(2022, 12, 12));

            Assert.IsTrue(foundFilm.Equals(newFilm));

        }

        [TestMethod()]
        public void Remove()
        {

            filmDao.Find(7);

            filmDao.Remove(7);

            Assert.ThrowsException<InstanceNotFoundException>(() => filmDao.Find(7));

        }

    }
}
