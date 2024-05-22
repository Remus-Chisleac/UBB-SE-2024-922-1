using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
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
        public List<JoinRequest> ReadAllJoinRequests()
        {
            string call = "/joinrequest";
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
                return JsonSerializer.Deserialize<List<JoinRequest>>(strResponseValue) ?? throw new Exception("server returned empty list");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<JoinRequest>();
            }
        }

        public void DeleteJoinRequest(Guid joinRequestId)
        {
            string call = $"/joinrequest/delete/{joinRequestId}";

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(serverAddress);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = client.DeleteAsync(call).Result;
                    response.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
