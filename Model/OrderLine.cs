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
    
    public partial class OrderLine
    {
        public long lineId { get; set; }
        public long lineOrderId { get; set; }
        public long lineProductId { get; set; }
        public decimal lineUnitaryPrice { get; set; }
        public long lineQuantity { get; set; }
    
        
        /// <summary>
        /// Relationship Name (Foreign Key in ER-Model): FK_OrderLine_LineOrderId
        /// </summary>
        public virtual UserOrder UserOrder { get; set; }
        
        /// <summary>
        /// Relationship Name (Foreign Key in ER-Model): FK_OrderLine_LineProductId
        /// </summary>
        public virtual Product Product { get; set; }
    
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
    
    			hash = hash * multiplier + lineOrderId.GetHashCode();
    			hash = hash * multiplier + lineProductId.GetHashCode();
    			hash = hash * multiplier + lineUnitaryPrice.GetHashCode();
    			hash = hash * multiplier + lineQuantity.GetHashCode();
    
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
    	    
            OrderLine target = obj as OrderLine;
    
    		return true
               &&  (this.lineId == target.lineId )       
               &&  (this.lineOrderId == target.lineOrderId )       
               &&  (this.lineProductId == target.lineProductId )       
               &&  (this.lineUnitaryPrice == target.lineUnitaryPrice )       
               &&  (this.lineQuantity == target.lineQuantity )       
               ;
    
        }
    
    
    	public static bool operator ==(OrderLine  objA, OrderLine  objB)
        {
            // Check if the objets are the same OrderLine entity
            if(Object.ReferenceEquals(objA, objB))
                return true;
      
            return objA.Equals(objB);
    }
    
    
    	public static bool operator !=(OrderLine  objA, OrderLine  objB)
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
    	    StringBuilder strOrderLine = new StringBuilder();
    
    		strOrderLine.Append("[ ");
           strOrderLine.Append(" lineId = " + lineId + " | " );       
           strOrderLine.Append(" lineOrderId = " + lineOrderId + " | " );       
           strOrderLine.Append(" lineProductId = " + lineProductId + " | " );       
           strOrderLine.Append(" lineUnitaryPrice = " + lineUnitaryPrice + " | " );       
           strOrderLine.Append(" lineQuantity = " + lineQuantity + " | " );       
            strOrderLine.Append("] ");    
    
    		return strOrderLine.ToString();
        }
    
    
    }
}
