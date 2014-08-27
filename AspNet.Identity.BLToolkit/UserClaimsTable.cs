using BLToolkit.Data;
using BLToolkit.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Claims;

namespace AspNet.Identity.BLToolkit
{
    /// <summary>
    /// Class that represents the UserClaims table in the MySQL Database
    /// </summary>
    public class UserClaimsTable
    {
        private DbManager db;

        /// <summary>
        /// Constructor that takes a DbManager instance 
        /// </summary>
        /// <param name="database"></param>
        public UserClaimsTable(DbManager database)
        {
            db = database;
        }

        /// <summary>
        /// Returns a ClaimsIdentity instance given a userId
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public ClaimsIdentity FindByUserId(int userId)
        {
            ClaimsIdentity claims = new ClaimsIdentity();

            db.SetCommand(@"Select * from AspNetUserClaims where UserId=@userId",
                db.Parameter("@userId", userId));
            using (IDataReader rd = db.ExecuteReader())
            {
                while (rd.Read())
                {
                    Claim claim = new Claim(rd["ClaimType"].ToString(), rd["ClaimValue"].ToString());
                    claims.AddClaim(claim);
                }
            }

            return claims;
        }

        /// <summary>
        /// Deletes all claims from a user given a userId
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public int Delete(int userId)
        {
            db
                .SetCommand(@"Delete from AspNetUserClaims where UserId = @userId",
                    db.Parameter("@userId", userId))
                .ExecuteNonQuery();

            return userId;
        }

        /// <summary>
        /// Inserts a new claim in UserClaims table
        /// </summary>
        /// <param name="userClaim">User's claim to be added</param>
        /// <param name="userId">User's id</param>
        /// <returns></returns>
        public int Insert(Claim userClaim, int userId)
        {
            db
                .SetCommand(@"Insert into AspNetUserClaims (ClaimValue, ClaimType, UserId) values (@value, @type, @userId)",
                    db.Parameter("value", userClaim.Value),
                    db.Parameter("type", userClaim.Type),
                    db.Parameter("userId", userId))
                .ExecuteNonQuery();

            return userId;
        }

        /// <summary>
        /// Deletes a claim from a user 
        /// </summary>
        /// <param name="user">The user to have a claim deleted</param>
        /// <param name="claim">A claim to be deleted from user</param>
        /// <returns></returns>
        public int Delete(IdentityUser user, Claim claim)
        {
            db
                .SetCommand(@"Delete from AspNetUserClaims where UserId = @userId and @ClaimValue = @value and ClaimType = @type",
                    db.Parameter("userId", user.Id),
                    db.Parameter("value", claim.Value),
                    db.Parameter("type", claim.Type))
                .ExecuteNonQuery();

            return user.Id;
        }
    }
}
