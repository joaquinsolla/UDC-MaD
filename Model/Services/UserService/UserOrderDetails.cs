using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.Services.UserService
{
    /// <summary>
    /// VO Class which contains the user order details
    /// </summary>
    [Serializable()]
    public class UserOrderDetails
    {

        #region Properties Region

        public long OrderId { get; private set; }

        public DateTime OrderDate { get; private set; }

        public long OrderBankCardPAN { get; private set; }

        public string OrderPostalAddress { get; private set; }

        public decimal OrderValue { get; private set; }

        public long OrderUserId { get; private set; }

        public string OrderDescription { get; private set; }

        #endregion Properties Region

        /// <summary>
        /// Initializes a new instance of the <see cref="UserOrderDetails"/>
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderDate"></param>
        /// <param name="orderBankCardPAN"></param>
        /// <param name="orderPostalAddress"></param>
        /// <param name="orderValue"></param>
        /// <param name="orderUserId"></param>
        /// <param name="orderDescription"></param>


        public UserOrderDetails(long orderId, DateTime orderDate, long orderBankCardPAN,
            string orderPostalAddress, decimal orderValue, long orderUserId, string orderDescription)
        {
            this.OrderId = orderId;
            this.OrderDate = orderDate;
            this.OrderBankCardPAN = orderBankCardPAN;
            this.OrderPostalAddress = orderPostalAddress;
            this.OrderValue = orderValue;
            this.OrderUserId = orderUserId;
            this.OrderDescription = orderDescription;
        }

        public override bool Equals(object obj)
        {

            UserOrderDetails target = (UserOrderDetails)obj;

            return (this.OrderId == target.OrderId)
                && (this.OrderDate == target.OrderDate)
                && (this.OrderBankCardPAN == target.OrderBankCardPAN)
                && (this.OrderPostalAddress == target.OrderPostalAddress)
                && (this.OrderValue == target.OrderValue)
                && (this.OrderUserId == target.OrderUserId)
                && (this.OrderDescription == target.OrderDescription);
        }

        // The GetHashCode method is used in hashing algorithms and data 
        // structures such as a hash table. In order to ensure that it works 
        // properly, we suppose that the OrderId does not change.        
        public override int GetHashCode()
        {
            return this.OrderId.GetHashCode();
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the 
        /// current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current 
        /// <see cref="T:System.Object"></see>.
        /// </returns>
        public override String ToString()
        {
            String strUserOrderDetails;

            strUserOrderDetails =
                "[ orderId = " + OrderId + " | " +
                "[orderDate = " + OrderDate.ToString() + " | " +
                "[orderBankCardPAN = " + OrderBankCardPAN + " | " +
                "orderPostalAddress = " + OrderPostalAddress + " | " +
                "orderValue = " + OrderValue + " | " +
                "orderUserId = " + OrderUserId + " | " +
                "orderDescription = " + OrderDescription + " ]";

            return strUserOrderDetails;
        }

    }
}
