using Es.Udc.DotNet.PracticaMaD.Model.DAOs.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.BankCardDao;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.UserOrderDao;
using Es.Udc.DotNet.PracticaMaD.Model.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.Services.UserService.Util;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using Ninject;
using System;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.BankCardTypeDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.Services.UserService
{

    public class UserService : IUserService
    {

        [Inject]
        public IUserProfileDao UserProfileDao { get; set; }

        [Inject]
        public IBankCardDao BankCardDao { get; set; }

        [Inject]
        public IBankCardTypeDao BankCardTypeDao { get; set; }

        [Inject]
        public IUserOrderDao UserOrderDao { get; set; }

        #region IUserService Members

        /// <exception cref="IncorrectPasswordException"/>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public void ChangePassword(long userProfileId, String oldClearPassword, String newClearPassword)
        {

            UserProfile userProfile = UserProfileDao.Find(userProfileId);
            String storedPassword = userProfile.enPassword;

            if (!PasswordEncrypter.IsClearPasswordCorrect(oldClearPassword, storedPassword))
            {
                throw new IncorrectPasswordException(userProfile.loginName);
            }

            userProfile.enPassword = PasswordEncrypter.Crypt(newClearPassword);

            UserProfileDao.Update(userProfile);
        }

        /// <exception cref="InstanceNotFoundException"/>
        public UserProfileDetails FindUserProfileDetails(long userProfileId)
        {
            UserProfile userProfile = UserProfileDao.Find(userProfileId);

            UserProfileDetails userProfileDetails = new UserProfileDetails(userProfile.firstName, userProfile.lastName,
                userProfile.email, userProfile.postalAddress, userProfile.language, userProfile.country, userProfile.admin);

            return userProfileDetails;
        }

        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="IncorrectPasswordException"/>
        [Transactional]
        public LoginResult Login(String loginName, String password, Boolean passwordIsEncrypted)
        {
            UserProfile userProfile = UserProfileDao.FindByLoginName(loginName);

            String storedPassword = userProfile.enPassword;

            if (passwordIsEncrypted)
            {
                if (!password.Equals(storedPassword))
                {
                    throw new IncorrectPasswordException(loginName);
                }
            }
            else
            {
                if (!PasswordEncrypter.IsClearPasswordCorrect(password, storedPassword))
                {
                    throw new IncorrectPasswordException(loginName);
                }
            }
            return new LoginResult(userProfile.usrId, storedPassword, userProfile.firstName, userProfile.lastName, userProfile.email,
                userProfile.postalAddress, userProfile.language, userProfile.country, userProfile.admin);
        }

        /// <exception cref="DuplicateInstanceException"/>
        [Transactional]
        public long RegisterUser(String loginName, String clearPassword, UserProfileDetails userProfileDetails)
        {
            try
            {
                UserProfileDao.FindByLoginName(loginName);
                throw new DuplicateInstanceException(loginName, typeof(UserProfile).FullName);
            }
            catch (InstanceNotFoundException)
            {
                String encryptedPassword = PasswordEncrypter.Crypt(clearPassword);

                UserProfile userProfile = new UserProfile();

                userProfile.loginName = loginName;
                userProfile.enPassword = encryptedPassword;
                userProfile.firstName = userProfileDetails.FirstName;
                userProfile.lastName = userProfileDetails.LastName;
                userProfile.email = userProfileDetails.Email;
                userProfile.postalAddress = userProfileDetails.PostalAddress;
                userProfile.language = userProfileDetails.Language;
                userProfile.country = userProfileDetails.Country;

                UserProfileDao.Create(userProfile);

                return userProfile.usrId;
            }
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public void UpdateUserProfileDetails(long userProfileId, UserProfileDetails userProfileDetails)
        {
            UserProfile userProfile = UserProfileDao.Find(userProfileId);

            userProfile.firstName = userProfileDetails.FirstName;
            userProfile.lastName = userProfileDetails.LastName;
            userProfile.email = userProfileDetails.Email;
            userProfile.postalAddress = userProfileDetails.PostalAddress;
            userProfile.language = userProfileDetails.Language;
            userProfile.country = userProfileDetails.Country;
            userProfile.admin = userProfileDetails.Admin;

            UserProfileDao.Update(userProfile);
        }

        public bool UserExists(string loginName)
        {

            try
            {
                UserProfile userProfile = UserProfileDao.FindByLoginName(loginName);
            }
            catch (InstanceNotFoundException)
            {
                return false;
            }
            return true;
        }

        /// <exception cref="InstanceNotFoundException"/>
        public List<BankCardType> FindAllBankCardTypes() {

            return BankCardTypeDao.GetAllElements();

        }

        /// <exception cref="InstanceNotFoundException"/>
        public List<BankCard> FindUserBankCards(long cardOwnerId)
        {
            return BankCardDao.FindAllByCardOwnerId(cardOwnerId);
        }

        /// <exception cref="InstanceNotFoundException"/>
        public BankCardDetails FindBankCardDetails(long cardPAN)
        {

            BankCard bankCard = BankCardDao.Find(cardPAN);

            BankCardDetails bankCardDetails = new BankCardDetails(bankCard.cardTypeId, ((int)bankCard.cardCvv),
                bankCard.cardExpirationDate, bankCard.cardDefault, bankCard.cardOwnerId);

            return bankCardDetails;
        }

        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="DuplicateInstanceException"/>
        [Transactional]
        public long AddBankCard(long cardOwnerId, long cardPAN, BankCardDetails bankCardDetails)
        {
            try
            {
                BankCardDao.Find(cardPAN);
                throw new DuplicateInstanceException(cardPAN, typeof(BankCard).FullName);
            }

            catch (InstanceNotFoundException)
            {
                if (bankCardDetails.CardDefault)
                {
                    BankCard oldDefaultCard = BankCardDao.FindDefaultCardByCardOwnerId(cardOwnerId);
                    oldDefaultCard.cardDefault = false;
                    BankCardDao.Update(oldDefaultCard);
                }

                BankCard bankCard = new BankCard();

                bankCard.cardPAN = cardPAN;
                bankCard.cardTypeId = bankCardDetails.CardTypeId;
                bankCard.cardCvv = bankCardDetails.CardCvv;
                bankCard.cardExpirationDate = bankCardDetails.CardExpirationDate;
                bankCard.cardOwnerId = cardOwnerId;

                int ownerCards = BankCardDao.FindAllByCardOwnerId(cardOwnerId).Count();
                if (ownerCards < 1) bankCard.cardDefault = true;
                else bankCard.cardDefault = bankCardDetails.CardDefault;

                BankCardDao.Create(bankCard);

                return bankCard.cardPAN;
            }
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public void UpdateBankCardDetails(long cardPAN, BankCardDetails bankCardDetails)
        {

            BankCard bankCard = BankCardDao.Find(cardPAN);

            bankCard.cardTypeId = bankCardDetails.CardTypeId;
            bankCard.cardCvv = bankCardDetails.CardCvv;
            bankCard.cardExpirationDate = bankCardDetails.CardExpirationDate;
            bankCard.cardDefault = bankCardDetails.CardDefault;
            bankCard.cardOwnerId = bankCardDetails.CardOwnerId;

            BankCardDao.Update(bankCard);
        }

        public bool BankCardExists(long cardPAN)
        {
            try
            {
                BankCard bankCard = BankCardDao.Find(cardPAN);
            }
            catch (InstanceNotFoundException)
            {
                return false;
            }
            return true;
        }

        /// <exception cref="InstanceNotFoundException"/>
        public OrderBlock FindUserOrders(long orderUserId, int startIndex, int size)
        {
            List<UserOrder> orders = UserOrderDao.FindAllByOrderUserId(orderUserId, startIndex, size+1);
            bool existMoreOrders = (orders.Count == size + 1);

            if (existMoreOrders)
                orders.RemoveAt(size);

            return new OrderBlock(orders, existMoreOrders);
        }


        /// <exception cref="InstanceNotFoundException"/>
        public UserOrderDetails FindUserOrderDetails(long orderId)
        {
            UserOrder userOrder = UserOrderDao.Find(orderId);

            UserOrderDetails userOrderDetails = new UserOrderDetails(userOrder.orderId, userOrder.orderDate, userOrder.orderBankCardPAN, userOrder.orderPostalAddress,
                userOrder.orderValue, userOrder.orderUserId, userOrder.orderDescription);

            return userOrderDetails;
        }

        #endregion IUserService Members

    }
}
