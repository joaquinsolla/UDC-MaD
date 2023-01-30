using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Model.DAOs.UserProfileDao
{
    /// <summary>
    /// Specific Operations for UserProfile
    /// </summary>
    public class UserProfileDaoEntityFramework :
        GenericDaoEntityFramework<UserProfile, long>, IUserProfileDao
    {

        #region Public Constructors

        public UserProfileDaoEntityFramework()
        {
        }

        #endregion Public Constructors

        #region IUserProfileDao Members. Specific Operations

        /// <summary>
        /// Finds a UserProfile by its loginName
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        /// <exception cref="InstanceNotFoundException"></exception>
        public UserProfile FindByLoginName(string loginName)
        {
            UserProfile userProfile = null;

            #region Using Linq.

            DbSet<UserProfile> userProfiles = Context.Set<UserProfile>();

            var result =
                (from u in userProfiles
                 where u.loginName == loginName
                 select u);

            userProfile = result.FirstOrDefault();

            #endregion Using Linq.

            if (userProfile == null)
                throw new InstanceNotFoundException(loginName,
                    typeof(UserProfile).FullName);

            return userProfile;
        }

        #endregion IUserProfileDao Members
    }

}