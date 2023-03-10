//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Es.Udc.DotNet.PracticaMaD.Model
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    
    public partial class BankCard
    {
        public BankCard()
        {
            this.UserOrders = new HashSet<UserOrder>();
        }
    
        public long cardPAN { get; set; }
        public long cardTypeId { get; set; }
        public long cardCvv { get; set; }
        public System.DateTime cardExpirationDate { get; set; }
        public bool cardDefault { get; set; }
        public long cardOwnerId { get; set; }
    
        
        /// <summary>
        /// Relationship Name (Foreign Key in ER-Model): FK_BankCard_CardOwnerId
        /// </summary>
        public virtual UserProfile UserProfile { get; set; }
        
        /// <summary>
        /// Relationship Name (Foreign Key in ER-Model): FK_BankCard_CardTypeId
        /// </summary>
        public virtual BankCardType BankCardType { get; set; }
        
        /// <summary>
        /// Relationship Name (Foreign Key in ER-Model): FK_UserOrder_OrderBankCardPAN
        /// </summary>
        public virtual ICollection<UserOrder> UserOrders { get; set; }
    
    	/// <summary>
    	/// A hash code for this instance, suitable for use in hashing algorithms and data structures 
    	/// like a hash table. It uses the Josh Bloch implementation from "Effective Java"
        /// Primary key of entity is not included in the hash calculation to avoid errors
    	/// with Entity Framework creation of key values.
    	/// </summary>
    	/// <returns>
    	/// Returns a hash code for this instance.
    	/// </returns>
    	public override int GetHashCode()
    	{
    	    unchecked
    	    {
    			int multiplier = 31;
    			int hash = GetType().GetHashCode();
    
    			hash = hash * multiplier + cardTypeId.GetHashCode();
    			hash = hash * multiplier + cardCvv.GetHashCode();
    			hash = hash * multiplier + cardExpirationDate.GetHashCode();
    			hash = hash * multiplier + cardDefault.GetHashCode();
    			hash = hash * multiplier + cardOwnerId.GetHashCode();
    
    			return hash;
    	    }
    
    	}
        
        /// <summary>
        /// Compare this object against another instance using a value approach (field-by-field) 
        /// </summary>
        /// <remarks>See http://www.loganfranken.com/blog/687/overriding-equals-in-c-part-1/ for detailed info </remarks>
    	public override bool Equals(object obj)
    	{
    
            if (ReferenceEquals(null, obj)) return false;        // Is Null?
            if (ReferenceEquals(this, obj)) return true;         // Is same object?
            if (obj.GetType() != this.GetType()) return false;   // Is same type?
    	    
            BankCard target = obj as BankCard;
    
    		return true
               &&  (this.cardPAN == target.cardPAN )       
               &&  (this.cardTypeId == target.cardTypeId )       
               &&  (this.cardCvv == target.cardCvv )       
               &&  (this.cardExpirationDate == target.cardExpirationDate )       
               &&  (this.cardDefault == target.cardDefault )       
               &&  (this.cardOwnerId == target.cardOwnerId )       
               ;
    
        }
    
    
    	public static bool operator ==(BankCard  objA, BankCard  objB)
        {
            // Check if the objets are the same BankCard entity
            if(Object.ReferenceEquals(objA, objB))
                return true;
      
            return objA.Equals(objB);
    }
    
    
    	public static bool operator !=(BankCard  objA, BankCard  objB)
        {
            return !(objA == objB);
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
    	    StringBuilder strBankCard = new StringBuilder();
    
    		strBankCard.Append("[ ");
           strBankCard.Append(" cardPAN = " + cardPAN + " | " );       
           strBankCard.Append(" cardTypeId = " + cardTypeId + " | " );       
           strBankCard.Append(" cardCvv = " + cardCvv + " | " );       
           strBankCard.Append(" cardExpirationDate = " + cardExpirationDate + " | " );       
           strBankCard.Append(" cardDefault = " + cardDefault + " | " );       
           strBankCard.Append(" cardOwnerId = " + cardOwnerId + " | " );       
            strBankCard.Append("] ");    
    
    		return strBankCard.ToString();
        }
    
    
    }
}
