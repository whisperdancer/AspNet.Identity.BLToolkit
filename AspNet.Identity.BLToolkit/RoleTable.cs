using BLToolkit.Data;
using BLToolkit.DataAccess;
using System;
using System.Collections.Generic;

namespace AspNet.Identity.BLToolkit
{
    /// <summary>
    /// Class that represents the Role table in the Database
    /// </summary>
    public class RoleTable
    {
        private string _conn = Globals.ConnString;
        private DbManager db;

        /// <summary>
        /// Constructor that takes a MySQLDatabase instance 
        /// </summary>
        /// <param name="database"></param>
        public RoleTable(DbManager database)
        {
            db = database;
        }

        /// <summary>
        /// Deltes a role from the Roles table
        /// </summary>
        /// <param name="roleId">The role Id</param>
        /// <returns></returns>
        public int Delete(int roleId)
        {
            db
                .SetCommand(@"Delete from AspNetRoles where Id = @id",
                    db.Parameter("@id", roleId))
                .ExecuteNonQuery();

            return roleId;
        }

        /// <summary>
        /// Inserts a new Role in the Roles table
        /// </summary>
        /// <param name="roleName">The role's name</param>
        /// <returns></returns>
        public int Insert(IdentityRole role)
        {
            var id = db
                .SetCommand(@"Insert into AspNetRoles (Name) values (@name)
                    SELECT Cast(SCOPE_IDENTITY() as int)",
                    db.Parameter("@id", role.Id),
                    db.Parameter("@name", role.Name))
                .ExecuteScalar<int>();

            return id;
        }

        /// <summary>
        /// Returns a role name given the roleId
        /// </summary>
        /// <param name="roleId">The role Id</param>
        /// <returns>Role name</returns>
        public string GetRoleName(int roleId)
        {
            string rn;
            rn = db
                 .SetCommand(@"Select Name from AspNetRoles where Id=@roleId",
                         db.Parameter("@roleId", roleId))
                 .ExecuteScalar<string>();
            return rn;
        }

        /// <summary>
        /// Returns the role Id given a role name
        /// </summary>
        /// <param name="roleName">Role's name</param>
        /// <returns>Role's Id</returns>
        public int GetRoleId(string roleName)
        {
            int rid;
            rid = db
                 .SetCommand(@"Select Id from AspNetRoles where Name=@roleName",
                         db.Parameter("@roleName", roleName))
                 .ExecuteScalar<int>();
            return rid;
        }

        /// <summary>
        /// Gets the IdentityRole given the role Id
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IdentityRole GetRoleById(int roleId)
        {
            var roleName = GetRoleName(roleId);
            IdentityRole role = null;

            if (roleName != null)
            {
                role = new IdentityRole(roleName, roleId);
            }

            return role;
        }

        /// <summary>
        /// Gets the IdentityRole given the role name
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public IdentityRole GetRoleByName(string roleName)
        {
            var roleId = GetRoleId(roleName);
            IdentityRole role = null;

            if (roleId > 0)
            {
                role = new IdentityRole(roleName, roleId);
            }

            return role;
        }

        public int Update(IdentityRole role)
        {
            db
             .SetCommand(@"
                    UPDATE
                        AspNetRoles
                    SET
                        Name = @name
                    WHERE
                        Id = @Id",
                 db.Parameter("@name", role.Name),
                 db.Parameter("@Id", role.Id))
             .ExecuteNonQuery();

            return role.Id;
        }
    }
}
