﻿using System.Configuration;
using Microsoft.Data.SqlClient;
using Moderation.Entities;
using Moderation.Model;

namespace Moderation.DbEndpoints
{
    public class RoleEndpoints
    {
        private static readonly string ConnectionString = "Data Source=192.168.100.43,1235;Initial Catalog=Moderation;Persist Security Info=False;User ID=iss;Password=1234567!a;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False;Connection Timeout=30;";
        public static void CreateRole(Role role)
        {
            using SqlConnection connection = new (ConnectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException azureTrialExpired)
            {
                Console.WriteLine(azureTrialExpired.Message);
                return;
            }

            string sql = "INSERT INTO UserRole VALUES (@RoleId,@Name)";
            using (SqlCommand command = new (sql, connection))
            {
                command.Parameters.AddWithValue("@RoleId", role.Id);
                command.Parameters.AddWithValue("@Name", role.Name);
                command.ExecuteNonQuery();
            }
            foreach (var permission in role.Permissions)
            {
                sql = "INSERT INTO RolePermission VALUES (@RoleId,@Permission)";
                using SqlCommand command = new (sql, connection);
                command.Parameters.AddWithValue("@RoleId", role.Id);
                command.Parameters.AddWithValue("@Permission", permission.ToString());
                command.ExecuteNonQuery();
            }
        }
        public static List<Role> ReadRole()
        {
            using SqlConnection connection = new (ConnectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException azureTrialExpired)
            {
                Console.WriteLine(azureTrialExpired.Message);
                return [];
            }
            List<Role> roles = [];
            string sql = "SELECT RoleId, Name FROM UserRole";
            using SqlCommand command = new (sql, connection);
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Role role = new (
                    reader.GetGuid(0),
                    reader.GetString(1));

                // Fetch permissions for the current role from RolePermission table
                string rolePermissionSql = "SELECT Permission FROM RolePermission WHERE RoleId = @RoleId";
                using (SqlCommand rolePermissionCommand = new (rolePermissionSql, connection))
                {
                    rolePermissionCommand.Parameters.AddWithValue("@RoleId", role.Id);

                    using SqlDataReader rolePermissionReader = rolePermissionCommand.ExecuteReader();
                    while (rolePermissionReader.Read())
                    {
                        // Map the permission string to the Permission enum
                        string permissionString = rolePermissionReader.GetString(0);
                        Permission permission = (Permission)Enum.Parse(typeof(Permission), permissionString);
                        role.Permissions.Add(permission);
                    }
                }
                roles.Add(role);
            }
            return roles;
        }
        public static void UpdateRoleName(Guid roleId, string newName)
        {
            using SqlConnection connection = new (ConnectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException azureTrialExpired)
            {
                Console.WriteLine(azureTrialExpired.Message);
                return;
            }
            connection.Open();

            string sql = "UPDATE UserRole SET Name = @NewName WHERE RoleId = @RoleId";

            using SqlCommand command = new (sql, connection);
            command.Parameters.AddWithValue("@NewName", newName);
            command.Parameters.AddWithValue("@RoleId", roleId);

            command.ExecuteNonQuery();
        }
        public static void UpdateRolePermissions(Guid roleId, List<Permission> newPermissions)
        {
            using SqlConnection connection = new (ConnectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException azureTrialExpired)
            {
                Console.WriteLine(azureTrialExpired.Message);
                return;
            }
            // Delete existing permissions for the role
            string deleteSql = "DELETE FROM RolePermission WHERE RoleId = @RoleId";
            using (SqlCommand deleteCommand = new (deleteSql, connection))
            {
                deleteCommand.Parameters.AddWithValue("@RoleId", roleId);
                deleteCommand.ExecuteNonQuery();
            }

            // Insert new permissions for the role
            string insertSql = "INSERT INTO RolePermission (RoleId, Permission) VALUES (@RoleId, @Permission)";
            foreach (Permission permission in newPermissions)
            {
                using SqlCommand insertCommand = new (insertSql, connection);
                insertCommand.Parameters.AddWithValue("@RoleId", roleId);
                insertCommand.Parameters.AddWithValue("@Permission", permission.ToString());
                insertCommand.ExecuteNonQuery();
            }
        }

        public static void DeleteRole(Guid roleId)
        {
            using SqlConnection connection = new (ConnectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException azureTrialExpired)
            {
                Console.WriteLine(azureTrialExpired.Message);
                return;
            }
            // Delete associated permissions from RolePermission table first
            string deletePermissionSql = "DELETE FROM RolePermission WHERE RoleId = @RoleId";
            using (SqlCommand deletePermissionCommand = new (deletePermissionSql, connection))
            {
                deletePermissionCommand.Parameters.AddWithValue("@RoleId", roleId);
                deletePermissionCommand.ExecuteNonQuery();
            }

            // Delete role from UserRole table after deleting associated permissions
            string deleteRoleSql = "DELETE FROM UserRole WHERE RoleId = @RoleId";
            using SqlCommand deleteRoleCommand = new (deleteRoleSql, connection);
            deleteRoleCommand.Parameters.AddWithValue("@RoleId", roleId);
            deleteRoleCommand.ExecuteNonQuery();
        }
    }
}