using System;
using Es.Udc.DotNet.PracticaMaD.Model.Services.ShoppingService;

namespace Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session
{
    public class UserSession
    {

        private long userProfileId;
        private String firstName;
        private String lastName;
        private String email;
        private String postalAddress;
        private bool admin;
        //private ShoppingCart cart;

        public long UserProfileId
        {
            get { return userProfileId; }
            set { userProfileId = value; }
        }

        public String FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public String LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public String Email
        {
            get { return email; }
            set { email = value; }
        }

        public String PostalAddress
        {
            get { return postalAddress; }
            set { postalAddress = value; }
        }

        public bool Admin
        {
            get { return admin; }
            set { admin = value; }
        }


        //public ShoppingCart Cart
        //{
        //    get { return cart; }
        //    set { cart = value; }
        //}

    }
}


