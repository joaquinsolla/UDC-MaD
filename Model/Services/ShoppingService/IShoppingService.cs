using Es.Udc.DotNet.PracticaMaD.Model.Exceptions;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Ninject;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.UserOrderDao;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.OrderLineDao;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.BankCardDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.Services.ShoppingService
{

    public interface IShoppingService
    {
        [Inject]
        IProductDao ProductDao { set; }

        [Inject]
        IUserOrderDao UserOrderDao { set; }

        [Inject]
        IOrderLineDao OrderLineDao { set; }

        [Inject]
        IUserProfileDao UserProfileDao { set; }

        [Inject]
        IBankCardDao BankCardDao { set; }

        // ---------- FUN 5 ----------

        ShoppingCart CreateShoppingCart();

        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="ProductOutOfStockException"/>
        [Transactional]
        void AddProductToShoppingCart(long proId, ShoppingCart shoppingCart);

        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="NotInShoppingCartException"/>
        [Transactional]
        void DeleteProductFromShoppingCart(long proId, ShoppingCart shoppingCart);

        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="ProductOutOfStockException"/>
        /// <exception cref="NotInShoppingCartException"/>
        [Transactional]
        void AddOneToShoppingCart(long proId, ShoppingCart shoppingCart);

        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="NotInShoppingCartException"/>
        [Transactional]
        void RemoveOneFromShoppingCart(long proId, ShoppingCart shoppingCart);

        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="NotInShoppingCartException"/>
        [Transactional]
        void ToggleGiftAtShoppingCart(long proId, ShoppingCart shoppingCart);

        /// <summary>
        /// Checks if the ShoppingCart is empty.
        /// </summary>
        /// <returns> True if the ShoppingCart is empty </returns>
        bool ShoppingCartIsEmpty(ShoppingCart shoppingCart);

        /// <summary>
        /// Empties the ShoppingCart
        /// </summary>
        [Transactional]
        void EmptyShoppingCart(ShoppingCart shoppingCart);

        // ---------- FUN 6 ----------

        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="ShoppingCartIsEmptyException"/>
        [Transactional]
        long BuyOrder(long usrId, long cardPAN, string postalAddress, string orderDescription, ShoppingCart shoppingCart);
        
    }

}
