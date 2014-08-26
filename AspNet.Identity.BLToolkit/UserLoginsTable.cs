using BLToolkit.Data;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data;

namespace AspNet.Identity.BLToolkit
{
    /// <summary>
    /// Class that represents the UserLogins table in the Database
    /// </summary>
    public class UserLoginsTable
    {
        private string _conn = Globals.ConnString;

        private DbManager db;

        /// <summary>
        /// Constructor that takes a MySQLDatabase instance 
        /// </summary>
        /// <param name="database"></param>
        public UserLoginsTable(DbManager database)
        {
            db = database;
        }

        /// <summary>
        /// Deletes a login from a user in the UserLogins table
        /// </summary>
        /// <param name="user">User to have login deleted</param>
        /// <param name="login">Login to be deleted from user</param>
        /// <returns></returns>
        public int Delete(IdentityUser user, UserLoginInfo login)
        {
            db
                .SetCommand(@"Delete from AspNetUserLogins where UserId = @userId and LoginProvider = @loginProvider and ProviderKey = @providerKey",
                db.Parameter("UserId", user.Id),
                db.Parameter("loginProvider", login.LoginProvider),
                db.Parameter("providerKey", login.ProviderKey))
                .ExecuteNonQuery();

            return user.Id;
        }

        /// <summary>
        /// Deletes all Logins from a user in the UserLogins table
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public int Delete(int userId)
        {
            db
                .SetCommand(@"Delete from AspNetUserLogins where UserId = @userId",
                    db.Parameter("@userId", userId))
                .ExecuteNonQuery();

            return userId;
        }

        /// <summary>
        /// Inserts a new login in the UserLogins table
        /// </summary>
        /// <param name="user">User to have new login added</param>
        /// <param name="login">Login to be added</param>
        /// <returns></returns>
        public int Insert(IdentityUser user, UserLoginInfo login)
        {
            db
                .SetCommand(@"Insert into AspNetUserLogins (LoginProvider, ProviderKey, UserId) values (@loginProvider, @providerKey, @userId)",
            db.Parameter("loginProvider", login.LoginProvider),
            db.Parameter("providerKey", login.ProviderKey),
            db.Parameter("userId", user.Id))
                .ExecuteNonQuery();

            return user.Id;
        }

        /// <summary>
        /// Return a userId given a user's login
        /// </summary>
        /// <param name="userLogin">The user's login info</param>
        /// <returns></returns>
        public int FindUserIdByLogin(UserLoginInfo userLogin)
        {
            int id;
            id = db
                 .SetCommand(@"Select UserId from AspNetUserLogins where LoginProvider = @loginProvider and ProviderKey = @providerKey",
                         db.Parameter("loginProvider", userLogin.LoginProvider),
                         db.Parameter("providerKey", userLogin.ProviderKey))
                 .ExecuteScalar<int>();
            return id;
        }

        /// <summary>
        /// Returns a list of user's logins
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public List<UserLoginInfo> FindByUserId(int userId)
        {
            List<UserLoginInfo> logins = new List<UserLoginInfo>();

            db.SetCommand(@"Select * from AspNetUserLogins where UserId = @userId",
                db.Parameter("@userId", userId));

            using (IDataReader rd = db.ExecuteReader())
            {
                while (rd.Read())
                {
                    var login = new UserLoginInfo(rd["LoginProvider"].ToString(), rd["ProviderKey"].ToString());
                    logins.Add(login);
                }

            }

            return logins;
        }
    }
}
