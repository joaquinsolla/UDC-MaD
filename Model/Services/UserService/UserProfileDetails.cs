using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.Services.UserService
{
    /// <summary>
    /// VO Class which contains the user details
    /// </summary>
    [Serializable()]
    public class UserProfileDetails
    {

        #region Properties Region

        public long UsrId { get; private set; }

        public string LoginName { get; private set; }

        public string EnPassword { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string Email { get; private set; }

        public string PostalAddress { get; private set; }

        public string Country { get; private set; }

        public string Language { get; private set; }

        public bool Admin { get; set; }

        #endregion Properties Region

        /// <summary>
        /// Initializes a new instance of the <see cref="UserProfileDetails"/>
        /// </summary>
        /// <param name="firstName">Users's first name.</param>
        /// <param name="lastName">Users's last name.</param>
        /// <param name="email">Users's email.</param>
        /// <param name="postalAddress">Users's postal address.</param>
        /// <param name="language">The language.</param>
        /// <param name="country">The country.</param>
        /// <param name="admin">The user type.</param>

        public UserProfileDetails(String firstName, String lastName, String email,
            String postalAddress, String language, String country, bool admin)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.PostalAddress = postalAddress;
            this.Language = language;
            this.Country = country;
            this.Admin = admin;
        }

        public override bool Equals(object obj)
        {

            UserProfileDetails target = (UserProfileDetails)obj;

            return (this.FirstName == target.FirstName)
                && (this.LastName == target.LastName)
                && (this.Email == target.Email)
                && (this.PostalAddress == target.PostalAddress)
                && (this.Language == target.Language)
                && (this.Country == target.Country)
                && (this.Admin == target.Admin);
        }

        // The GetHashCode method is used in hashing algorithms and data 
        // structures such as a hash table. In order to ensure that it works 
        // properly, we suppose that the FirstName does not change.        
        public override int GetHashCode()
        {
            return this.FirstName.GetHashCode(); //solo firstName ??
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
            String strUserProfileDetails;

            strUserProfileDetails =
                "[ firstName = " + FirstName + " | " +
                "lastName = " + LastName + " | " +
                "email = " + Email + " | " +
                "postalAddress = " + PostalAddress + " | " +
                "language = " + Language + " | " +
                "country = " + Country + " | " +
                "admin = " + Admin + " ]";

            return strUserProfileDetails;
        }

    }
}
