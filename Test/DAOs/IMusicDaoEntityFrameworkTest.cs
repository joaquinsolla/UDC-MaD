using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.MusicDao;
using Es.Udc.DotNet.PracticaMaD.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.Collections.Generic;
using System.Transactions;

namespace Es.Udc.DotNet.PracticaMaD.Test.DAOs
{
    [TestClass]
    public class IMusicDaoEntityFrameworkTest
    {
        private static IKernel kernel;
        private static IMusicDao musicDao;

        // Variables used in several tests are initialized here
        private const long NON_EXISTENT_MUSIC_ID = -1;

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
            musicDao = kernel.Get<IMusicDao>();
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

            Music music = null;

            music = musicDao.Find(4);

            Assert.IsTrue(music.proId == 4 && music.proName == "Music 1" && music.proPrice == 11.95M
                && music.proReleaseDate == new System.DateTime(2022, 10, 08) && music.proCatName == "Music"
                && music.musicArtist == "Artist x" && music.musicAlbum == "Album x"
                && music.musicSongs == 11 && music.musicDurationMins == 45 && music.musicReleaseDate == new System.DateTime(2001, 08, 05));

            music = musicDao.Find(5);

            Assert.IsTrue(music.proId == 5 && music.proName == "Music 2" && music.proPrice == 2.95M
                && music.proReleaseDate == new System.DateTime(2020, 01, 01) && music.proCatName == "Music"
                && music.musicArtist == "Artist x" && music.musicAlbum == "Album y"
                && music.musicSongs == 8 && music.musicDurationMins == 30 && music.musicReleaseDate == new System.DateTime(2004, 07, 10));

            music = musicDao.Find(6);

            Assert.IsTrue(music.proId == 6 && music.proName == "Music 3" && music.proPrice == 5M
                && music.proReleaseDate == new System.DateTime(2022, 10, 08) && music.proCatName == "Music"
                && music.musicArtist == "Artist y" && music.musicAlbum == "Album z"
                && music.musicSongs == 15 && music.musicDurationMins == 60 && music.musicReleaseDate == new System.DateTime(2017, 12, 12));

            Assert.ThrowsException<System.InvalidOperationException>(() => musicDao.Find(1));

            Assert.ThrowsException<InstanceNotFoundException>(() => musicDao.Find(NON_EXISTENT_MUSIC_ID));

        }

        [TestMethod()]
        public void Add()
        {

            Music newMusic = new Music();

            newMusic.proName = "Music 4";
            newMusic.proPrice = 10M;
            newMusic.proReleaseDate = new System.DateTime(2022, 10, 08);
            newMusic.proStock = 1;
            newMusic.proCatName = "Music";
            newMusic.musicArtist = "Test artist";
            newMusic.musicAlbum = "Test album";
            newMusic.musicSongs = 2;
            newMusic.musicDurationMins = 6;
            newMusic.musicReleaseDate = new System.DateTime(2022, 12, 12);

            musicDao.Create(newMusic);

            Music foundMusic = musicDao.GetAllElements()[3];

            Assert.IsTrue(foundMusic.proName == "Music 4" && foundMusic.proPrice == 10M
                && foundMusic.proReleaseDate == new System.DateTime(2022, 10, 08) && foundMusic.proStock == 1 && foundMusic.proCatName == "Music"
                && foundMusic.musicArtist == "Test artist" && foundMusic.musicAlbum == "Test album"
                && foundMusic.musicSongs == 2 && foundMusic.musicDurationMins == 6 && foundMusic.musicReleaseDate == new System.DateTime(2022, 12, 12));

            Assert.IsTrue(foundMusic.Equals(newMusic));

        }

        [TestMethod()]
        public void Remove()
        {

            musicDao.Find(4);

            musicDao.Remove(4);

            Assert.ThrowsException<InstanceNotFoundException>(() => musicDao.Find(4));

        }

    }
}
