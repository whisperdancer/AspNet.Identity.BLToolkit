using BLToolkit.Data;
using BLToolkit.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;

namespace AspNet.Identity.BLToolkit
{
    /// <summary>
    /// Class that represents the UserRoles table in the Database
    /// </summary>
    public class UserRolesTable
    {
        private DbManager db;

        /// <summary>
        /// Constructor that takes a DbManager instance 
        /// </summary>
        /// <param name="database"></param>
        public UserRolesTable(DbManager database)
        {
            db = database;
        }

        /// <summary>
        /// Returns a list of user's roles
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public List<string> FindByUserId(int userId)
        {
            List<string> roles = new List<string>();
            db.SetCommand(@"Select AspNetRoles.Name from AspNetUserRoles, AspNetRoles where AspNetUserRoles.UserId =@userId and AspNetUserRoles.RoleId = AspNetRoles.Id",
                db.Parameter("@userId", userId));

            using (IDataReader rd = db.ExecuteReader())
            {
                while (rd.Read())
                    roles.Add(rd["Name"].ToString());
            }

            return roles;
        }

        /// <summary>
        /// Deletes all roles from a user in the UserRoles table
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public void Delete(int userId)
        {
            db
                .SetCommand(@"Delete from AspNetUserRoles where Id = @id",
                    db.Parameter("@id", userId))
                .ExecuteNonQuery();
        }

        /// <summary>
        /// Inserts a new role for a user in the UserRoles table
        /// </summary>
        /// <param name="user">The User</param>
        /// <param name="roleId">The Role's id</param>
        /// <returns></returns>
        public void Insert(IdentityUser user, int roleId)
        {
            db
                .SetCommand(@"Insert into AspNetUserRoles (UserId, RoleId) values (@userId, @roleId)",
                    db.Parameter("@userId", user.Id),
                    db.Parameter("@roleId", roleId))
                .ExecuteNonQuery();
        }
    }
}
