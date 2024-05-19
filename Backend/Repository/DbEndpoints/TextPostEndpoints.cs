﻿using System.Configuration;
using Microsoft.Data.SqlClient;
using Moderation.Entities;
using Moderation.Model;

namespace Moderation.DbEndpoints
{
    public class TextPostEndpoints
    {
        private static readonly string ConnectionString = "Data Source=192.168.100.43,1235;Initial Catalog=Moderation;Persist Security Info=False;User ID=iss;Password=1234567!a;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False;Connection Timeout=30;";
        public static void CreateTextPost(TextPost textPost)
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

            string insertPostSql = "INSERT INTO Post (PostId, Content, UserId, Score, Status, IsDeleted, GroupId) " +
                                   "VALUES (@Id, @Content, @UserId, @Score, @Status, @IsDeleted, @GroupId)";

            using (SqlCommand command = new (insertPostSql, connection))
            {
                command.Parameters.AddWithValue("@Id", textPost.Id);
                command.Parameters.AddWithValue("@Content", textPost.Content);
                command.Parameters.AddWithValue("@UserId", textPost.Author.Id);
                command.Parameters.AddWithValue("@Score", textPost.Score);
                command.Parameters.AddWithValue("@Status", textPost.Status);
                command.Parameters.AddWithValue("@IsDeleted", textPost.IsDeleted);
                command.Parameters.AddWithValue("@GroupId", textPost.Author.GroupId);

                command.ExecuteNonQuery();
            }

            // Insert hardcodedAwards for the post into PostAward table
            foreach (Award award in textPost.Awards)
            {
                string insertPostAwardSql = "INSERT INTO PostAward (AwardId, Id) VALUES (@AwardId, @Id)";
                using SqlCommand awardCommand = new (insertPostAwardSql, connection);
                awardCommand.Parameters.AddWithValue("@AwardId", award.Id);
                awardCommand.Parameters.AddWithValue("@Id", textPost.Id);
                awardCommand.ExecuteNonQuery();
            }
        }
        public static List<TextPost> ReadAllTextPosts()
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
            List<TextPost> textPosts = [];

            string sql = "SELECT p.PostId, p.Content, p.GroupId, " +
                         "u.Id, u.Uid , u.GroupId " +
                         "FROM Post p " +
                         "INNER JOIN GroupUser u ON p.UserId = u.Id";

            using SqlCommand command = new (sql, connection);
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Guid postid = reader.GetGuid(0);
                string content = reader.GetString(1);
                // Guid groupId = reader.GetGuid(2);
                Guid id = reader.GetGuid(3);
                Guid userId = reader.GetGuid(4);
                Guid groupUserId = reader.GetGuid(5);

                GroupUser author = new (id, userId, groupUserId);

                List<Award> awards = ReadAwardsForPost(postid);

                TextPost textPost = new (postid, content, author, awards);

                textPosts.Add(textPost);
            }

            return textPosts;
        }
        private static List<Award> ReadAwardsForPost(Guid postId)
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
            List<Award> awards = [];

            string sql = "SELECT a.AwardId, a.Type " +
                         "FROM Award a " +
                         "INNER JOIN PostAward pa ON a.AwardId = pa.AwardId " +
                         "WHERE pa.PostId = @Id";

            using SqlCommand command = new (sql, connection);
            command.Parameters.AddWithValue("@Id", postId);

            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Guid awardId = reader.GetGuid(0);
                Award award = new () { Id = awardId, AwardTypeObj = (Award.AwardType)Enum.Parse(typeof(Award.AwardType), reader.GetString(1)) };
                awards.Add(award);
            }

            return awards;
        }
        public static void DeleteTextPost(Guid postId)
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

            // Delete from PostAward table first
            string deletePostAwardSql = "DELETE FROM PostAward WHERE Id = @Id";
            using (SqlCommand command = new (deletePostAwardSql, connection))
            {
                command.Parameters.AddWithValue("@Id", postId);
                command.ExecuteNonQuery();
            }

            // Delete from Post table
            string deletePostSql = "DELETE FROM Post WHERE PostId = @Id";
            using (SqlCommand command = new (deletePostSql, connection))
            {
                command.Parameters.AddWithValue("@Id", postId);
                command.ExecuteNonQuery();
            }
        }
        public static void UpdateTextPost(TextPost textPost)
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
            string updatePostSql = "UPDATE Post SET " +
                                   "Content = @Content, " +
                                   "UserId = @UserId, " +
                                   "Score = @Score, " +
                                   "Status = @Status, " +
                                   "IsDeleted = @IsDeleted, " +
                                   "GroupId = @GroupId " +
                                   "WHERE PostId = @Id";

            using SqlCommand command = new (updatePostSql, connection);
            command.Parameters.AddWithValue("@Id", textPost.Id);
            command.Parameters.AddWithValue("@Content", textPost.Content);
            command.Parameters.AddWithValue("@UserId", textPost.Author.Id);
            command.Parameters.AddWithValue("@Score", textPost.Score);
            command.Parameters.AddWithValue("@Status", textPost.Status);
            command.Parameters.AddWithValue("@IsDeleted", textPost.IsDeleted);
            command.Parameters.AddWithValue("@GroupId", textPost.Author.GroupId);

            command.ExecuteNonQuery();
        }
    }
}
