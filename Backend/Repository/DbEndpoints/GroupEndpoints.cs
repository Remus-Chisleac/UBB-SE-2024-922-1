﻿using System.Configuration;
using Microsoft.Data.SqlClient;
using Moderation.Entities;
using Moderation.Model;
using Moderation.Serivce;

namespace Moderation.DbEndpoints
{
    internal class GroupEndpoints
    {
        private static readonly string ConnectionString = "Data Source=192.168.100.43,1235;Initial Catalog=Moderation;Persist Security Info=False;User ID=iss;Password=1234567!a;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False;Connection Timeout=30;";
        public static void CreateGroup(Group group)
        {
            using SqlConnection connection = new (ConnectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException azureazureTrialExpired)
            {
                Console.WriteLine(azureazureTrialExpired.Message);
                return;
            }

            string sql = "INSERT INTO [Group] (Id, Name, Description, Owner) " +
                         "VALUES (@Id, @Name, @Description, @Owner)";

            using SqlCommand command = new (sql, connection);
            command.Parameters.AddWithValue("@Id", group.Id);
            command.Parameters.AddWithValue("@Name", group.Name);
            command.Parameters.AddWithValue("@Description", group.Description);
            command.Parameters.AddWithValue("@Owner", group.Creator.Id);

            command.ExecuteNonQuery();
        }
        public static List<Group> ReadAllGroups()
        {
            using SqlConnection connection = new (ConnectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException azureazureTrialExpired)
            {
                Console.WriteLine(azureazureTrialExpired.Message);
                return [];
            }

            List<Group> groups = [];
            string sql = "SELECT Id, Name, Description, Owner FROM [Group]";

            using SqlCommand command = new (sql, connection);
            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                var userId = reader.GetGuid(3);
                string username = ApplicationState.Get()
                    .UserRepository?.Get(userId)?
                    .Username // if anything is null along the way throw an exception:
                    ?? throw new Exception("No username by that id");
                User user = new (userId, username);

                Group group = new (reader.GetGuid(0), reader.GetString(1), reader.GetString(2), user);
                groups.Add(group);
            }
            return groups;
        }
        public static void UpdateGroup(Group group)
        {
            using SqlConnection connection = new (ConnectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException azureazureTrialExpired)
            {
                Console.WriteLine(azureazureTrialExpired.Message);
                return;
            }
            string sql = "UPDATE Group" +
                         "SET Name = @Name, Description = @Description, Owner = @Owner" +
                         "WHERE Id = @Id";

            using SqlCommand command = new (sql, connection);
            command.Parameters.AddWithValue("@Name", group.Name);
            command.Parameters.AddWithValue("@Description", group.Description);
            command.Parameters.AddWithValue("@Owner", group.Creator.Id);

            command.ExecuteNonQuery();
        }
        public static void DeleteGroup(Guid id)
        {
            using SqlConnection connection = new (ConnectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException azureazureTrialExpired)
            {
                Console.WriteLine(azureazureTrialExpired.Message);
                return;
            }

            string sql = "DELETE FROM Group WHERE Id = @Id";

            using SqlCommand command = new (sql, connection);
            command.Parameters.AddWithValue("@Id", id);

            command.ExecuteNonQuery();
        }
    }
}
