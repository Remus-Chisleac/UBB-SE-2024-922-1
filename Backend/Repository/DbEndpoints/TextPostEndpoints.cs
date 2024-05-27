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
        public List<TextPost> ReadAllTextPosts()
        {
            string call = "/textpost";
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
                return JsonSerializer.Deserialize<List<TextPost>>(strResponseValue) ?? throw new Exception("server returned empty list");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<TextPost>();
            }
        }
        private List<Award> ReadAwardsForPost(Guid postId)
        {
            string call = "/textpost/award";
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
        public void UpdateTextPost(TextPost textPost)
        {
            string call = "/textpost/update";

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(serverAddress);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpContent content = new StringContent(JsonSerializer.Serialize(textPost), Encoding.UTF8, "application/json");

                    var response = client.PutAsync(call, content).Result;
                    response.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DeleteTextPost(Guid postId)
        {
            string call = $"/textpost/delete/{postId}";

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
