using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.Services.UserService
{
    /// <summary>
    /// A Custom VO which keeps the results for a login action.
    /// </summary>
    [Serializable()]
    public class LoginResult
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginResult"/> class.
        /// </summary>
        /// <param name="usrId">The user profile id.</param>
        /// <param name="enPassword">The encrypted password.</param>
        /// <param name="firstName">Users's first name.</param>
        /// <param name="lastName">Users's last name.</param>
        /// <param name="email">Users's email.</param>
        /// <param name="postalAddress">Users's postal address.</param>
        /// <param name="language">The language.</param>
        /// <param name="country">The country.</param>
        /// <param name="admin">The user type.</param>

        public LoginResult(long usrId, String enPassword, String firstName, String lastName, 
            String email, String postalAddress, String language, String country, bool admin)
        {
            this.UsrId = usrId;
            this.EnPassword = enPassword;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.PostalAddress = postalAddress;
            this.Language = language;
            this.Country = country;
            this.Admin = admin;
        }

        #region Properties Region

        /// <summary>
        /// Gets the user profile id.
        /// </summary>
        /// <value>The user profile id.</value>
        public long UsrId { get; private set; }

        /// <summary>
        /// Gets the encrypted password.
        /// </summary>
        /// <value>The <c>encryptedPassword.</c></value>
        public string EnPassword { get; private set; }

        /// <summary>
        /// Gets the first name.
        /// </summary>
        /// <value>The <c>firstName</c></value>
        public string FirstName { get; private set; }

        /// <summary>
        /// Gets the last name.
        /// </summary>
        /// <value>The <c>lastName</c></value>
        public string LastName { get; private set; }

        /// <summary>
        /// Gets the email.
        /// </summary>
        /// <value>The <c>email</c></value>
        public string Email { get; private set; }

        /// <summary>
        /// Gets the postal address.
        /// </summary>
        /// <value>The <c>lastName</c></value>
        public string PostalAddress { get; private set; }

        /// <summary>
        /// Gets the country code.
        /// </summary>
        /// <value>The country code.</value>
        public string Country { get; private set; }

        /// <summary>
        /// Gets the language code.
        /// </summary>
        /// <value>The language code.</value>
        public string Language { get; private set; }

        public bool Admin { get; set; }

        #endregion Properties Region

        public override bool Equals(object obj)
        {
            LoginResult target = (LoginResult)obj;

            return (this.UsrId == target.UsrId)
                && (this.EnPassword == target.EnPassword)
                && (this.FirstName == target.FirstName)
                && (this.LastName == target.LastName)
                && (this.Email == target.Email)
                && (this.PostalAddress == target.PostalAddress)
                && (this.Language == target.Language)
                && (this.Country == target.Country)
                && (this.Admin == target.Admin);
        }

        public override int GetHashCode()
        {
            return this.UsrId.GetHashCode();
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
            String strLoginResult;

            strLoginResult =
                "[ usrId = " + UsrId + " | " +
                "enPassword = " + EnPassword + " | " +
                "firstName = " + FirstName + " | " +
                "lastName = " + LastName + " | " +
                "email = " + Email + " | " +
                "postalAddress = " + PostalAddress + " | " + 
                "language = " + Language + " | " +
                "country = " + Country + " |" +
                "admin = " + Admin + " ]";

            return strLoginResult;
        }
    }
}
