using Es.Udc.DotNet.PracticaMaD.Model.DAOs.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.BankCardDao;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.UserOrderDao;
using Es.Udc.DotNet.PracticaMaD.Model.Exceptions;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System;
using Ninject;
using Es.Udc.DotNet.PracticaMaD.Model.DAOs.BankCardTypeDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.Services.UserService
{
    public interface IUserService
    {
        
        [Inject]
        IUserProfileDao UserProfileDao { get; set; }

        [Inject]
        IBankCardTypeDao BankCardTypeDao { get; set; }

        [Inject]
        IBankCardDao BankCardDao { get; set; }

        [Inject]
        IUserOrderDao UserOrderDao { get; }


        /** USER MANAGEMENT */

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="userProfileId"> The user profile id. </param>
        /// <param name="oldClearPassword"> The old clear password. </param>
        /// <param name="newClearPassword"> The new clear password. </param>
        /// <exception cref="IncorrectPasswordException"/>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        void ChangePassword(long userProfileId, String oldClearPassword,
            String newClearPassword);

        /// <summary>
        /// Finds the user profile details.
        /// </summary>
        /// <param name="userProfileId"> The user profile id. </param>
        /// <returns> The user profile details </returns>
        /// <exception cref="InstanceNotFoundException"/>
        UserProfileDetails FindUserProfileDetails(long userProfileId);

        /// <summary>
        /// Logins the specified login name.
        /// </summary>
        /// <param name="loginName"> Name of the login. </param>
        /// <param name="password"> The password. </param>
        /// <param name="passwordIsEncrypted"> if set to <c> true </c> [password is encrypted]. </param>
        /// <returns> LoginResult </returns>
        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="IncorrectPasswordException"/>
        [Transactional]
        LoginResult Login(String loginName, String password,
            Boolean passwordIsEncrypted);

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="loginName"> Name of the login. </param>
        /// <param name="clearPassword"> The clear password. </param>
        /// <param name="userProfileDetails"> The user profile details. </param>
        /// <exception cref="DuplicateInstanceException"/>
        [Transactional]
        long RegisterUser(String loginName, String clearPassword,
            UserProfileDetails userProfileDetails);

        /// <summary>
        /// Updates the user profile details.
        /// </summary>
        /// <param name="userProfileId"> The user profile id. </param>
        /// <param name="userProfileDetails"> The user profile details. </param>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        void UpdateUserProfileDetails(long userProfileId,
            UserProfileDetails userProfileDetails);

        /// <summary>
        /// Checks if the specified loginName corresponds to a valid user.
        /// </summary>
        /// <param name="loginName"> User loginName. </param>
        /// <returns> Boolean to indicate if the loginName exists </returns>
        bool UserExists(string loginName);

        /** USER BANK CARDS MANAGEMENT */

        /// <exception cref="InstanceNotFoundException"/>
        List<BankCardType> FindAllBankCardTypes();

        /// <exception cref="InstanceNotFoundException"/>
        List<BankCard> FindUserBankCards(long cardOwnerId);

        /// <exception cref="InstanceNotFoundException"/>
        BankCardDetails FindBankCardDetails(long cardPAN);

        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="DuplicateInstanceException"/>
        [Transactional]
        long AddBankCard(long cardOwnerId, long cardPAN, BankCardDetails bankCardDetails);

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        void UpdateBankCardDetails(long cardPAN, BankCardDetails bankCardDetails);

        /// <summary>
        /// Checks if the specified cardPAN corresponds to a valid BankCard.
        /// </summary>
        /// <returns> Boolean to indicate if the cardPAN exists </returns>
        bool BankCardExists(long cardPAN);

        /** USER ORDERS MANAGEMENT */

        /// <exception cref="InstanceNotFoundException"/>
        OrderBlock FindUserOrders(long orderUserId, int startIndex, int size); 

        /// <exception cref="InstanceNotFoundException"/>
        UserOrderDetails FindUserOrderDetails(long orderId);
        
    }
}
