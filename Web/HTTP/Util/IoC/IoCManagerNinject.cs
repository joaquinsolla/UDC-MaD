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

using Es.Udc.DotNet.ModelUtil.IoC;
using Ninject;
using System.Configuration;
using System.Data.Entity;

namespace Es.Udc.DotNet.PracticaMaD.HTTP.Util.IoC
{
    internal class IoCManagerNinject : IIoCManager
    {
        private static IKernel kernel;
        private static NinjectSettings settings;

        public void Configure()
        {
            settings = new NinjectSettings() { LoadExtensions = true };
            kernel = new StandardKernel(settings);

            /* DAOs */
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

            /* Services */
            kernel.Bind<ICatalogService>().
                To<CatalogService>();
            kernel.Bind<IShoppingService>().
                To<ShoppingService>();
            kernel.Bind<IUserService>().
                To<UserService>();

            /* DbContext */
            string connectionString =
                ConfigurationManager.ConnectionStrings["shoppingEntities"].ConnectionString;

            kernel.Bind<DbContext>().
                ToSelf().
                InSingletonScope().
                WithConstructorArgument("nameOrConnectionString", connectionString);
        }

        public T Resolve<T>()
        {
            return kernel.Get<T>();
        }
    }
}