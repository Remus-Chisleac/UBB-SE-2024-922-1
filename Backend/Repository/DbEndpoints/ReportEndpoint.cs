using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using Moderation.Model;

namespace Moderation.DbEndpoints
{
    public class ReportEndpoint
    {
        private readonly string serverAddress;

        public ReportEndpoint(string server)
        {
            serverAddress = server;
        }

        public void CreatePostReport(PostReport postReport)
        {
            string call = "/report/add";

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(serverAddress);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpContent content = new StringContent(JsonSerializer.Serialize(postReport), Encoding.UTF8, "application/json");

                    var response = client.PostAsync(call, content).Result;
                    response.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public List<PostReport> ReadAllPostReports()
        {
            string call = "/report";
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
                return JsonSerializer.Deserialize<List<PostReport>>(strResponseValue) ?? throw new Exception("server returned empty list");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<PostReport>();
            }
        }

        public void UpdatePostReport(Guid id, PostReport postReport)
        {
            string call = "/report/update";

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(serverAddress);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpContent content = new StringContent(JsonSerializer.Serialize(postReport), Encoding.UTF8, "application/json");

                    var response = client.PutAsync(call, content).Result;
                    response.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DeletePostReport(Guid reportId)
        {
            string call = $"/report/delete/{reportId}";

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