﻿using System.Configuration;
using Microsoft.Data.SqlClient;
using Moderation.Entities;
using Moderation.Model;

namespace Moderation.DbEndpoints
{
    public class GroupUserEndpoints
    {
        private static readonly string ConnectionString = "Data Source=192.168.100.43,1235;Initial Catalog=Moderation;Persist Security Info=False;User ID=iss;Password=1234567!a;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False;Connection Timeout=30;";
        public static void CreateGroupUser(GroupUser user)
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
            string sql = "INSERT INTO GroupUser (Id, Uid, GroupId, PostScore, MarketplaceScore, StatusRestriction, StatusRestrictionDate, StatusMessage, RoleId) " +
                         "VALUES (@Id, @Uid, @GroupId, @PostScore, @MarketplaceScore, @StatusRestriction, @StatusRestrictionDate, @StatusMessage, @RoleId)";

            using SqlCommand command = new (sql, connection);
            command.Parameters.AddWithValue("@Id", user.Id);
            command.Parameters.AddWithValue("@Uid", user.UserId);
            command.Parameters.AddWithValue("@GroupId", user.GroupId);
            command.Parameters.AddWithValue("@PostScore", user.PostScore);
            command.Parameters.AddWithValue("@MarketplaceScore", user.MarketplaceScore);
            command.Parameters.AddWithValue("@StatusRestriction", (int)user.Status.Restriction);
            command.Parameters.AddWithValue("@StatusRestrictionDate", user.Status.RestrictionDate);
            command.Parameters.AddWithValue("@StatusMessage", user.Status.Message);
            command.Parameters.AddWithValue("@RoleId", user.RoleId);

            command.ExecuteNonQuery();
        }
        public static List<GroupUser> ReadAllGroupUsers()
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
            List<GroupUser> users = [];

            string sql = "SELECT Id, Uid, Groupid, PostScore, MarketplaceScore, StatusRestriction, StatusRestrictionDate, StatusMessage, RoleId FROM GroupUser";

            using SqlCommand command = new (sql, connection);
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                // UserRestriction restriction = (UserRestriction)reader.GetInt32(5);
                // UserStatus status = new(restriction, reader.GetDateTime(6), reader.GetString(7));
                GroupUser user = new (reader.GetGuid(0), reader.GetGuid(1), reader.GetGuid(2), reader.GetInt32(3), reader.GetInt32(4), new UserStatus(UserRestriction.None, DateTime.Now), reader.GetGuid(8));
                users.Add(user);
            }

            return users;
        }
        public static void UpdateGroupUser(GroupUser user)
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

            string sql = "UPDATE GroupUser SET" +
                         "Uid = @Uid, " +
                         "GroupId = @GroupId, " +
                         "PostScore = @PostScore, " +
                         "MarketplaceScore = @MarketplaceScore, " +
                         "StatusRestriction = @StatusRestriction, " +
                         "StatusRestrictionDate = @StatusRestrictionDate, " +
                         "StatusMessage = @StatusMessage, " +
                         "RoleId = @RoleId" +
                         "WHERE Id = @Id";

            using SqlCommand command = new (sql, connection);
            command.Parameters.AddWithValue("@Uid", user.UserId);
            command.Parameters.AddWithValue("@GroupId", user.GroupId);
            command.Parameters.AddWithValue("@PostScore", user.PostScore);
            command.Parameters.AddWithValue("@MarketplaceScore", user.MarketplaceScore);
            command.Parameters.AddWithValue("@StatusRestriction", (int)user.Status.Restriction);
            command.Parameters.AddWithValue("@StatusRestrictionDate", user.Status.RestrictionDate);
            command.Parameters.AddWithValue("@StatusMessage", user.Status.Message);
            command.Parameters.AddWithValue("@RoleId", user.RoleId);

            command.ExecuteNonQuery();
        }
        public static void DeleteGroupUser(Guid id)
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
            string sql = "DELETE FROM GroupUser WHERE Id = @Id";

            using SqlCommand command = new (sql, connection);
            command.Parameters.AddWithValue("@Id", id);

            command.ExecuteNonQuery();
        }
    }
}