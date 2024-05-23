using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using Moderation.Model;

namespace Moderation.DbEndpoints
{
    public class GroupEndpoints
    {
        private readonly string serverAddress;

        public GroupEndpoints(string server)
        {
            serverAddress = server;
        }

        public void CreateGroup(Group group)
        {
            string call = "/group/add";

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(serverAddress);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpContent content = new StringContent(JsonSerializer.Serialize(group), Encoding.UTF8, "application/json");

                    var response = client.PostAsync(call, content).Result;
                    response.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public List<Group> ReadAllGroups()
        {
            string call = "/group";
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
                return JsonSerializer.Deserialize<List<Group>>(strResponseValue) ?? throw new Exception("server returned empty list");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Group>();
            }
        }
        public void UpdateGroup(Group group)
        {
            string call = "/group/update";

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(serverAddress);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpContent content = new StringContent(JsonSerializer.Serialize(group), Encoding.UTF8, "application/json");

                    var response = client.PutAsync(call, content).Result;
                    response.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void DeleteGroup(Guid id)
        {
            string call = $"/group/delete/{id}";

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
