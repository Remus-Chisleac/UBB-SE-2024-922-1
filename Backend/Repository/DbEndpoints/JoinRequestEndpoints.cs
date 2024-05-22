using System.Configuration;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using Microsoft.Data.SqlClient;
using Moderation.Entities;

namespace Moderation.DbEndpoints
{
    public class JoinRequestEndpoints
    {
        private readonly string serverAddress;

        public JoinRequestEndpoints(string server)
        {
            serverAddress = server;
        }

        public void CreateJoinRequest(JoinRequest joinRequest)
        {
            string call = "/joinrequest/add";

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(serverAddress);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpContent content = new StringContent(JsonSerializer.Serialize(joinRequest), Encoding.UTF8, "application/json");

                    var response = client.PostAsync(call, content).Result;
                    response.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static List<JoinRequest> ReadAllJoinRequests()
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
            List<JoinRequest> joinRequests = [];
            string sql = "SELECT Junior.Id, Junior.UserId " +
                         "FROM JoinRequest Junior ";
            using SqlCommand command = new (sql, connection);
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                JoinRequest joinRequest = new (
                    reader.GetGuid(0),
                    reader.GetGuid(1));
                joinRequests.Add(joinRequest);
            }
            return joinRequests;
        }

        public static void DeleteJoinRequest(Guid joinRequestId)
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
            string deleteJoinRequestSql = "DELETE FROM JoinRequest WHERE Id = @Id";
            using SqlCommand command = new (deleteJoinRequestSql, connection);
            command.Parameters.AddWithValue("@Id", joinRequestId);
            command.ExecuteNonQuery();
        }
    }
}
