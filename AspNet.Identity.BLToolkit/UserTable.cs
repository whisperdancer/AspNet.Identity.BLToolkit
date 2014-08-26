using BLToolkit.Data;
using BLToolkit.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;

namespace AspNet.Identity.BLToolkit
{
    /// <summary>
    /// Class that represents the Users table in the Database
    /// </summary>
    public class UserTable<TUser>
        where TUser : IdentityUser
    {
        private string _conn = Globals.ConnString;
        private DbManager db;

        /// <summary>
        /// Constructor that takes a MySQLDatabase instance 
        /// </summary>
        /// <param name="database"></param>
        public UserTable(DbManager database)
        {
            db = database;
        }

        /// <summary>
        /// Returns the user's name given a user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetUserName(int userId)
        {
            string un;
            un = db
                 .SetCommand(@"Select Name from AspNetUsers where Id=@userId",
                         db.Parameter("@userId", userId))
                 .ExecuteScalar<string>();
            return un;
        }

        /// <summary>
        /// Returns a User ID given a user name
        /// </summary>
        /// <param name="userName">The user's name</param>
        /// <returns></returns>
        public int GetUserId(string userName)
        {
            int uid;
            uid = db
                 .SetCommand(@"Select Id from AspNetUsers where UserName=@userName",
                         db.Parameter("@userName", userName))
                 .ExecuteScalar<int>();
            return uid;
        }

        /// <summary>
        /// Returns an TUser given the user's id
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public TUser GetUserById(int userId)
        {
            TUser user = null;
            db.SetCommand(@"Select * from AspNetUsers where Id=@userId",
                db.Parameter("@userId", userId));

            using (IDataReader rd = db.ExecuteReader())
            {
                while (rd.Read())
                {
                    user = (TUser)Activator.CreateInstance(typeof(TUser));
                    user.Id = Int32.Parse(rd["Id"].ToString());
                    user.UserName = rd["UserName"].ToString();
                    user.PasswordHash = string.IsNullOrEmpty(rd["PasswordHash"].ToString()) ? null : rd["PasswordHash"].ToString();
                    user.SecurityStamp = string.IsNullOrEmpty(rd["SecurityStamp"].ToString()) ? null : rd["SecurityStamp"].ToString();
                    user.Email = string.IsNullOrEmpty(rd["Email"].ToString()) ? null : rd["Email"].ToString();
                    user.EmailConfirmed = rd["EmailConfirmed"].ToString() == "1" ? true : false;
                    user.PhoneNumber = string.IsNullOrEmpty(rd["PhoneNumber"].ToString()) ? null : rd["PhoneNumber"].ToString();
                    user.PhoneNumberConfirmed = rd["PhoneNumberConfirmed"].ToString() == "1" ? true : false;
                    user.LockoutEnabled = rd["LockoutEnabled"].ToString() == "1" ? true : false;
                    user.LockoutEndDateUtc = string.IsNullOrEmpty(rd["LockoutEndDateUtc"].ToString()) ? DateTime.Now : DateTime.Parse(rd["LockoutEndDateUtc"].ToString());
                    user.AccessFailedCount = string.IsNullOrEmpty(rd["AccessFailedCount"].ToString()) ? 0 : int.Parse(rd["AccessFailedCount"].ToString());
                }
            }
            return user;
        }

        /// <summary>
        /// Returns a list of TUser instances given a user name
        /// </summary>
        /// <param name="userName">User's name</param>
        /// <returns></returns>
        public List<TUser> GetUserByName(string userName)
        {
            List<TUser> users = new List<TUser>();

            db.SetCommand(@"Select * from AspNetUsers where UserName=@userName",
                db.Parameter("@userName", userName));

            using (IDataReader rd = db.ExecuteReader())
            {
                while (rd.Read())
                {
                    TUser user = (TUser)Activator.CreateInstance(typeof(TUser));
                    user.Id = Int32.Parse(rd["Id"].ToString());
                    user.UserName = rd["UserName"].ToString();
                    user.PasswordHash = string.IsNullOrEmpty(rd["PasswordHash"].ToString()) ? null : rd["PasswordHash"].ToString();
                    user.SecurityStamp = string.IsNullOrEmpty(rd["SecurityStamp"].ToString()) ? null : rd["SecurityStamp"].ToString();
                    user.Email = string.IsNullOrEmpty(rd["Email"].ToString()) ? null : rd["Email"].ToString();
                    user.EmailConfirmed = rd["EmailConfirmed"].ToString() == "1" ? true : false;
                    user.PhoneNumber = string.IsNullOrEmpty(rd["PhoneNumber"].ToString()) ? null : rd["PhoneNumber"].ToString();
                    user.PhoneNumberConfirmed = rd["PhoneNumberConfirmed"].ToString() == "1" ? true : false;
                    user.LockoutEnabled = rd["LockoutEnabled"].ToString() == "1" ? true : false;
                    user.LockoutEndDateUtc = string.IsNullOrEmpty(rd["LockoutEndDateUtc"].ToString()) ? DateTime.Now : DateTime.Parse(rd["LockoutEndDateUtc"].ToString());
                    user.AccessFailedCount = string.IsNullOrEmpty(rd["AccessFailedCount"].ToString()) ? 0 : int.Parse(rd["AccessFailedCount"].ToString());
                    users.Add(user);
                }

                return users;
            }
        }

        public List<TUser> GetUserByEmail(string email)
        {
            return null;
        }

        /// <summary>
        /// Return the user's password hash
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public string GetPasswordHash(int userId)
        {
            string ph;
            ph = db
                 .SetCommand(@"Select PasswordHash from AspNetUsers where Id = @userId",
                         db.Parameter("@userId", userId))
                 .ExecuteScalar<string>();
            return ph;
        }

        /// <summary>
        /// Sets the user's password hash
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        public int SetPasswordHash(int userId, string passwordHash)
        {
            db
             .SetCommand(@"
                    UPDATE
                        AspNetUsers
                    SET
                        PasswordHash = @pwdHash
                    WHERE
                        Id = @Id",
                 db.Parameter("@pwdHash", passwordHash),
                 db.Parameter("@Id", userId))
             .ExecuteNonQuery();

            return userId;
        }

        /// <summary>
        /// Returns the user's security stamp
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetSecurityStamp(int userId)
        {
            string ss;
            ss = db
                 .SetCommand(@"Select SecurityStamp from AspNetUsers where Id = @userId",
                         db.Parameter("@userId", userId))
                 .ExecuteScalar<string>();
            return ss;
        }

        /// <summary>
        /// Inserts a new user in the Users table
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int Insert(TUser user)
        {
            int _id;
            _id = db
                   .SetCommand(@"
                            Insert into AspNetUsers
                                (UserName,  PasswordHash, SecurityStamp,Email,EmailConfirmed,PhoneNumber,PhoneNumberConfirmed, AccessFailedCount,LockoutEnabled,LockoutEndDateUtc,TwoFactorEnabled)
                            values (@name, @pwdHash, @SecStamp,@email,@emailconfirmed,@phonenumber,@phonenumberconfirmed,@accesscount,@lockoutenabled,@lockoutenddate,@twofactorenabled)
                            SELECT Cast(SCOPE_IDENTITY() as int)",
           db.Parameter("@name", user.UserName),
                //  db.Parameter("@id", user.Id),
           db.Parameter("@pwdHash", user.PasswordHash),
           db.Parameter("@SecStamp", user.SecurityStamp),
           db.Parameter("@email", user.Email),
           db.Parameter("@emailconfirmed", user.EmailConfirmed),
           db.Parameter("@phonenumber", user.PhoneNumber),
           db.Parameter("@phonenumberconfirmed", user.PhoneNumberConfirmed),
           db.Parameter("@accesscount", user.AccessFailedCount),
           db.Parameter("@lockoutenabled", user.LockoutEnabled),
           db.Parameter("@lockoutenddate", user.LockoutEndDateUtc),
           db.Parameter("@twofactorenabled", user.TwoFactorEnabled))
           .ExecuteScalar<int>();

            user.Id = _id; // set userid
            return _id;
        }

        /// <summary>
        /// Deletes a user from the Users table
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        private int Delete(int userId)
        {
            db
                .SetCommand(@"Delete from AspNetUsers where Id = @id",
                    db.Parameter("@id", userId))
                .ExecuteNonQuery();

            return userId;
        }

        /// <summary>
        /// Deletes a user from the Users table
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int Delete(TUser user)
        {
            return Delete(user.Id);
        }

        /// <summary>
        /// Updates a user in the Users table
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int Update(TUser user)
        {
            db
              .SetCommand(@"
                            Update AspNetUsers set UserName = @userName, PasswordHash = @pswHash, SecurityStamp = @secStamp, 
                Email=@email, EmailConfirmed=@emailconfirmed, PhoneNumber=@phonenumber, PhoneNumberConfirmed=@phonenumberconfirmed,
                AccessFailedCount=@accesscount, LockoutEnabled=@lockoutenabled, LockoutEndDateUtc=@lockoutenddate, TwoFactorEnabled=@twofactorenabled  
                WHERE Id = @userId",
                db.Parameter("@userName", user.UserName),
                db.Parameter("@pswHash", user.PasswordHash),
                db.Parameter("@secStamp", user.SecurityStamp),
                db.Parameter("@userId", user.Id),
                db.Parameter("@email", user.Email),
                db.Parameter("@emailconfirmed", user.EmailConfirmed),
                db.Parameter("@phonenumber", user.PhoneNumber),
                db.Parameter("@phonenumberconfirmed", user.PhoneNumberConfirmed),
                db.Parameter("@accesscount", user.AccessFailedCount),
                db.Parameter("@lockoutenabled", user.LockoutEnabled),
                db.Parameter("@lockoutenddate", user.LockoutEndDateUtc),
                db.Parameter("@twofactorenabled", user.TwoFactorEnabled)
           )
           .ExecuteNonQuery();

            return user.Id;
        }
    }
}
