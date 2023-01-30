using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.Services.UserService
{
    public class OrderBlock
    {
        public List<UserOrder> Orders { get; private set; }
        public bool ExistMoreOrders { get; private set; }

        public OrderBlock(List<UserOrder> orders, bool existMoreOrders)
        {
            this.Orders = orders;
            this.ExistMoreOrders = existMoreOrders;
        }
    }
}
