using System;
using System.Collections.Generic;
using Ninject;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.UserOrderDao;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.OrderLineDao;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.BankCardDao;
using Es.Udc.DotNet.PracticaMaD.Model.Exceptions;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;

namespace Es.Udc.DotNet.PracticaMaD.Model.Services.ShoppingService
{
    public class ShoppingService : IShoppingService
    {
        [Inject]
        public IProductDao ProductDao { private get; set; }

        [Inject]
        public IUserOrderDao UserOrderDao { private get; set; }

        [Inject]
        public IOrderLineDao OrderLineDao { private get; set; }

        [Inject]
        public IUserProfileDao UserProfileDao { private get; set; }

        [Inject]
        public IBankCardDao BankCardDao { private get; set; }

        #region IShoppingService Members

        // ---------- FUN 5 ----------

        public ShoppingCart CreateShoppingCart() => new ShoppingCart();

        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="ProductOutOfStockException"/>
        [Transactional]
        public void AddProductToShoppingCart(long proId, ShoppingCart shoppingCart) {

            Product product = null;
            product = ProductDao.Find(proId);

            // Comprobamos el stock del producto
            if (product.proStock < 1)
                throw new ProductOutOfStockException(proId);

            // Si ya existe en el carrito, se suma una unidad más
            foreach (ShoppingCartLine line in shoppingCart.cartLines)
            {
                if (line.product.proId == proId)
                {
                    line.quantity++;
                    line.linePrice += product.proPrice;
                    shoppingCart.totalPrice += product.proPrice;

                    // Mientras el producto esté en el carrito, esa unidad queda reservada
                    product.proStock--;
                    ProductDao.Update(product);

                    return;
                }

            }

            // Si no está en el carrito, se añade
            ShoppingCartLine newLine = new ShoppingCartLine(product, 1, false);
            shoppingCart.cartLines.Add(newLine);
            shoppingCart.totalPrice += product.proPrice;

            // Mientras el producto esté en el carrito, esa unidad queda reservada
            product.proStock--;
            ProductDao.Update(product);

        }

        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="NotInShoppingCartException"/>
        [Transactional]
        public void DeleteProductFromShoppingCart(long proId, ShoppingCart shoppingCart) {

            Product product = ProductDao.Find(proId);

            // Comprobamos que el producto figure en el carrito y lo eliminamos
            foreach (ShoppingCartLine line in shoppingCart.cartLines)
            {
                if (line.product.proId == proId)
                {
                    // Devolvemos las unidades del producto al stock
                    product.proStock += line.quantity;
                    ProductDao.Update(product);

                    // Eliminamos el producto del carrito
                    shoppingCart.totalPrice -= line.linePrice;
                    shoppingCart.cartLines.Remove(line);
                    return;
                }

            }
            throw new NotInShoppingCartException(proId);

        }

        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="ProductOutOfStockException"/>
        /// <exception cref="NotInShoppingCartException"/>
        [Transactional]
        public void AddOneToShoppingCart(long proId, ShoppingCart shoppingCart) {

            Product product = ProductDao.Find(proId);

            // Comprobamos que ya exista en el carrito
            foreach (ShoppingCartLine line in shoppingCart.cartLines)
            {
                if (line.product.proId == proId)
                {
                    // Comprobamos el stock del producto
                    if (product.proStock < 1)
                        throw new ProductOutOfStockException(proId);

                    line.quantity++;
                    line.linePrice += product.proPrice;
                    shoppingCart.totalPrice += product.proPrice;

                    // Mientras el producto esté en el carrito, esa unidad queda reservada
                    product.proStock--;
                    ProductDao.Update(product);

                    return;
                }

            }
            throw new NotInShoppingCartException(proId);

        }

        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="NotInShoppingCartException"/>
        [Transactional]
        public void RemoveOneFromShoppingCart(long proId, ShoppingCart shoppingCart) {

            Product product = ProductDao.Find(proId);

            // Comprobamos que ya exista en el carrito
            foreach (ShoppingCartLine line in shoppingCart.cartLines)
            {
                if (line.product.proId == proId)
                {
                    if (line.quantity == 1)
                    {
                        // Eliminamos la linea del carrito
                        shoppingCart.cartLines.Remove(line);
                    }
                    else
                    {
                        // Eliminamos una unidad de la linea
                        line.quantity--;
                        line.linePrice -= product.proPrice;
                    }
                    shoppingCart.totalPrice -= product.proPrice;

                    // Devolvemos la unidad al stock
                    product.proStock++;
                    ProductDao.Update(product);

                    return;
                }

            }
            throw new NotInShoppingCartException(proId);

        }

        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="NotInShoppingCartException"/>
        [Transactional]
        public void ToggleGiftAtShoppingCart(long proId, ShoppingCart shoppingCart) {

            Product product = ProductDao.Find(proId);

            // Comprobamos que ya exista en el carrito
            foreach (ShoppingCartLine line in shoppingCart.cartLines)
            {
                if (line.product.proId == proId)
                {
                    // Marcamos/desmarcamos el producto como regalo
                    line.gift = !line.gift;
                    return;
                }

            }
            throw new NotInShoppingCartException(proId);

        }

        /// <summary>
        /// Checks if the ShoppingCart is empty.
        /// </summary>
        /// <returns> True if the ShoppingCart is empty </returns>
        public bool ShoppingCartIsEmpty(ShoppingCart shoppingCart) => (shoppingCart.cartLines.Count == 0);

        /// <summary>
        /// Empties the ShoppingCart
        /// </summary>
        [Transactional]
        public void EmptyShoppingCart(ShoppingCart shoppingCart) {

            if (ShoppingCartIsEmpty(shoppingCart) == false) 
            {
                List<ShoppingCartLine> lines = new List<ShoppingCartLine>();

                foreach (ShoppingCartLine line in shoppingCart.cartLines)
                {
                    lines.Add(line);
                }

                foreach (ShoppingCartLine line in lines)
                {
                    DeleteProductFromShoppingCart(line.product.proId, shoppingCart);
                }

            }

        }

        // ---------- FUN 6 ----------

        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="ShoppingCartIsEmptyException"/>
        [Transactional]
        public long BuyOrder(long usrId, long cardPAN, string postalAddress, string orderDescription, ShoppingCart shoppingCart) {

            UserProfile user = UserProfileDao.Find(usrId);
            BankCard bankCard = BankCardDao.Find(cardPAN);

            // Comprobamos que el carrito no esté vacío
            if (ShoppingCartIsEmpty(shoppingCart))
                throw new ShoppingCartIsEmptyException();

            // Creamos el UserOrder
            UserOrder userOrder = new UserOrder();

            userOrder.orderDate = DateTime.Now;
            userOrder.orderBankCardPAN = cardPAN;
            userOrder.orderPostalAddress = postalAddress;
            userOrder.orderValue = shoppingCart.totalPrice;
            userOrder.orderUserId = usrId;
            userOrder.orderDescription = orderDescription;

            UserOrderDao.Create(userOrder);

            // Creamos las OrderLines
            List<OrderLine> orderLines = new List<OrderLine>();
            foreach (ShoppingCartLine line in shoppingCart.cartLines)
            {

                OrderLine orderLine = new OrderLine();

                orderLine.lineOrderId = userOrder.orderId;
                orderLine.lineProductId = line.product.proId;
                orderLine.lineUnitaryPrice = line.product.proPrice;
                orderLine.lineQuantity = line.quantity;

                OrderLineDao.Create(orderLine);

                // Enlazamos las OrderLines con el UserOrder
                userOrder.OrderLines.Add(orderLine);
            }

            UserOrderDao.Update(userOrder);

            return userOrder.orderId;

        }

        #endregion IShoppingService Members

    }
}
