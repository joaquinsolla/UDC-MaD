using Es.Udc.DotNet.PracticaMaD.Model.DAOs.BankCardDao;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.BankCardTypeDao;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.BookDao;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.FilmDao;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.MusicDao;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.OrderLineDao;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.TagDao;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.UserOrderDao;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model.Services.CatalogService;
using Es.Udc.DotNet.PracticaMaD.Model.Services.ShoppingService;
using Es.Udc.DotNet.PracticaMaD.Model.Services.UserService;
using Ninject;
using System.Configuration;
using System.Data.Entity;

namespace Es.Udc.DotNet.PracticaMaD.Test
{
    public class TestManager
    {
        /// <summary>
        /// Configures and populates the Ninject kernel
        /// </summary>
        /// <returns>The NInject kernel</returns>
        public static IKernel ConfigureNInjectKernel()
        {
            #region Option A : configuration via sourcecode

            IKernel kernel = new StandardKernel();

            kernel.Bind<ICatalogService>().To<CatalogService>();
            kernel.Bind<IShoppingService>().To<ShoppingService>();
            kernel.Bind<IUserService>().To<UserService>();

            kernel.Bind<IBankCardDao>().
                To<BankCardDaoEntityFramework>();
            kernel.Bind<IBankCardTypeDao>().
                To<BankCardTypeDaoEntityFramework>();
            kernel.Bind<IBookDao>().
                To<BookDaoEntityFramework>();
            kernel.Bind<ICategoryDao>().
                To<CategoryDaoEntityFramework>();
            kernel.Bind<ICommentDao>().
                To<CommentDaoEntityFramework>();
            kernel.Bind<IFilmDao>().
                To<FilmDaoEntityFramework>();
            kernel.Bind<IMusicDao>().
                To<MusicDaoEntityFramework>();
            kernel.Bind<IOrderLineDao>().
                To<OrderLineDaoEntityFramework>();
            kernel.Bind<IProductDao>().
                To<ProductDaoEntityFramework>();
            kernel.Bind<ITagDao>().
                To<TagDaoEntityFramework>();
            kernel.Bind<IUserOrderDao>().
                To<UserOrderDaoEntityFramework>();
            kernel.Bind<IUserProfileDao>().
                To<UserProfileDaoEntityFramework>();

            string connectionString =
                ConfigurationManager.ConnectionStrings["shoppingEntities"].ConnectionString;

            kernel.Bind<DbContext>().
                ToSelf().
                InSingletonScope().
                WithConstructorArgument("nameOrConnectionString", connectionString);

            #endregion Option A : configuration via sourcecode

            #region Option B: configuration via external XML configuration file

            // The kernel should automatically load extensions at startup
            //NinjectSettings settings = new NinjectSettings() { LoadExtensions = false };
            //IKernel kernel = new StandardKernel(settings, new Ninject.Extensions.Xml.XmlExtensionModule());

            //kernel.Load("Ninject_Config.xml");

            #endregion Option B: configuration via external XML configuration file

            return kernel;
        }

        /// <summary>
        /// Configures the Ninject kernel from an external module file.
        /// </summary>
        /// <param name="moduleFilename">The module filename.</param>
        /// <returns>The NInject kernel</returns>
        public static IKernel ConfigureNInjectKernel(string moduleFilename)
        {
            IKernel kernel = new StandardKernel();
            kernel.Load(moduleFilename);

            return kernel;
        }

        public static void ClearNInjectKernel(IKernel kernel)
        {
            kernel.Dispose();
        }
    }
}