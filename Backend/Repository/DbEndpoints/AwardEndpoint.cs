using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using Moderation.Entities;

namespace Moderation.DbEndpoints
{
    public class AwardEndpoint
    {
        private readonly string serverAddress;

        public AwardEndpoint(string server)
        {
            serverAddress = server;
        }

        public void CreateAward(Award award)
        {
            string call = "/award/add";

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(serverAddress);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpContent content = new StringContent(JsonSerializer.Serialize(award), Encoding.UTF8, "application/json");

                    var response = client.PostAsync(call, content).Result;
                    response.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public List<Award> ReadAwards()
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
                return JsonSerializer.Deserialize<List<Award>>(strResponseValue) ?? throw new Exception("server returned empty list");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Award>();
            }
        }

        public void UpdateAward(Award award)
        {
            string call = "/award/update";

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(serverAddress);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpContent content = new StringContent(JsonSerializer.Serialize(award), Encoding.UTF8, "application/json");

                    var response = client.PutAsync(call, content).Result;
                    response.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DeleteAward(Guid id)
        {
            string call = $"/award/delete/{id}";

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
