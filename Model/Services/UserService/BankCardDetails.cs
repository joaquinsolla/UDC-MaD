using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.Services.UserService
{
    /// <summary>
    /// VO Class which contains the bank card details
    /// </summary>
    [Serializable()]
    public class BankCardDetails
    {

        #region Properties Region

        public long CardPAN { get; private set; }

        public long CardTypeId { get; private set; }

        public int CardCvv { get; private set; }

        public DateTime CardExpirationDate { get; private set; }

        public bool CardDefault { get; private set; }

        public long CardOwnerId { get; private set; }

        #endregion Properties Region

        /// <summary>
        /// Initializes a new instance of the <see cref="BankCardDetails"/>
        /// </summary>
        ///// <param name="cardPAN"></param>
        /// <param name="cardTypeId"></param>
        /// <param name="cardCvv"></param>
        /// <param name="cardExpirationDate"></param>
        /// <param name="cardDefault"></param>
        /// <param name="cardOwnerId"></param>

        public BankCardDetails(long cardTypeId, int cardCvv, 
            DateTime cardExpirationDate, bool cardDefault, long cardOwnerId)
        {
            //this.CardPAN = cardPAN;
            this.CardTypeId = cardTypeId;
            this.CardCvv = cardCvv;
            this.CardExpirationDate = cardExpirationDate;
            this.CardDefault = cardDefault;
            this.CardOwnerId = cardOwnerId;
        }

        public override bool Equals(object obj)
        {

            BankCardDetails target = (BankCardDetails)obj;

            return //(this.CardPAN == target.CardPAN)
                (this.CardTypeId == target.CardTypeId)
                && (this.CardCvv == target.CardCvv)
                && (this.CardExpirationDate == target.CardExpirationDate)
                && (this.CardDefault == target.CardDefault)
                && (this.CardOwnerId == target.CardOwnerId);
        }

        // The GetHashCode method is used in hashing algorithms and data 
        // structures such as a hash table. In order to ensure that it works 
        // properly, we suppose that the CardPAN does not change.        
        public override int GetHashCode()
        {
            return this.CardPAN.GetHashCode();
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
            String strBankCardDetails;

            strBankCardDetails =
                //"[ cardPAN = " + CardPAN + " | " +
                "[cardTypeId = " + CardTypeId + " | " +
                "cardCvv = " + CardCvv + " | " +
                "cardExpirationDate = " + CardExpirationDate + " | " +
                "cardDefault = " + CardDefault + " | " +
                "cardOwnerId = " + CardOwnerId + " ]";

            return strBankCardDetails;
        }

    }
}
