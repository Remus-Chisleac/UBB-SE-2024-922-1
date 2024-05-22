using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using Moderation.Entities;

namespace Moderation.DbEndpoints
{
    public class UserEndpoints
    {
        private readonly string serverAddress;

        public UserEndpoints(string server)
        {
            serverAddress = server;
        }

        public void CreateUser(User user)
        {
            string call = "/user/add";

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(serverAddress);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpContent content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");

                    var response = client.PostAsync(call, content).Result;
                    response.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public List<User> ReadAllUsers()
        {
            string call = "/user";
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
                return JsonSerializer.Deserialize<List<User>>(strResponseValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<User>();
            }
        }
        public void UpdateUser(User newValues)
        {
            string call = "/user/update";

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(serverAddress);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpContent content = new StringContent(JsonSerializer.Serialize(newValues), Encoding.UTF8, "application/json");

                    var response = client.PutAsync(call, content).Result;
                    response.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void DeleteUser(Guid id)
        {
            string call = $"/user/delete/{id}";

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