using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using Moderation.Entities;
using Moderation.Model;

namespace Moderation.DbEndpoints
{
    public class TextPostEndpoints
    {
        private readonly string serverAddress;

        public TextPostEndpoints(string server)
        {
            serverAddress = server;
        }

        public void CreateTextPost(TextPost textPost)
        {
            string call = "/textpost/add";

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(serverAddress);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpContent content = new StringContent(JsonSerializer.Serialize(textPost), Encoding.UTF8, "application/json");

                    var response = client.PostAsync(call, content).Result;
                    response.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
        private List<Award> ReadAwardsForPost(Guid postId)
        {
            string call = "/award";
            string strResponseValue;

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(serverAddress);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = client.GetAsync(call).Result;
                    response.EnsureSuccessStatusCode();
                    strResponseValue = response.Content.ReadAsStringAsync().Result;
                }
                return JsonSerializer.Deserialize<List<Award>>(strResponseValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Award>();
            }
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
