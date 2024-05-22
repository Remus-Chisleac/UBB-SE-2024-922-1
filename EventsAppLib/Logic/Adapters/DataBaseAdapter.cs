namespace EventsApp.Logic.Adapters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading.Tasks;
    using System.Reflection;
    using System.Text.Json.Serialization;
    using System.Net.Http.Headers;
    using EventsApp.Logic.Attributes;
    using EventsApp.Logic.Managers;
    using Microsoft.Data.SqlClient;
    using Microsoft.IdentityModel.Tokens;
    using Nancy.Json;

    public class DataBaseAdapter<T>(string filePath)
        : DataAdapter<T>(filePath)
        where T : struct
    {
        private string connectionString = filePath;

        private string TableName => typeof(T).GetCustomAttributes(typeof(TableAttribute), true).Cast<TableAttribute>().FirstOrDefault().TableName;

        public enum HttpVerb
        {
            GET,
            POST,
            PUT,
            DELETE
        }

        private string baseUrl = "http://localhost:5043";

        public string ConnectionString()
        {
            return connectionString;
        }

        // replace with html call
        public override void Add(T item)
        {
            string call = $"/Add/{this.TableName}/";

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpContent content = new StringContent(JsonSerializer.Serialize(item), Encoding.UTF8, "application/json");

                    var response = client.PostAsync(call, content).Result;
                    response.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
            }
        }

        public override void Clear()
        {
            string endPoint = baseUrl + $"/Clear/{this.TableName}";
            string call = $"/Clear/{this.TableName}";

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = client.DeleteAsync(call).Result;
                    response.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
            }
        }

        public override bool Contains(Identifier id)
        {
            Dictionary<string, object> pks = id.PrimaryKeys;

            string endPoint = baseUrl + $"/Contains/{this.TableName}";
            string call = $"/Contains/{this.TableName}/";
            foreach (var pk in pks)
            {
                call += $"{pk.Value}/";
            }

            call = call.Substring(0, call.Length - 1);

            bool contains = false;
            string strResponseValue = string.Empty;

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = client.GetAsync(call).Result;
                    response.EnsureSuccessStatusCode();
                    strResponseValue = response.Content.ReadAsStringAsync().Result;
                }
                contains = JsonSerializer.Deserialize<bool>(strResponseValue);
            }
            catch (Exception ex)
            {
                strResponseValue = "{\"errorMessages\":[\"" + ex.Message.ToString() + "\"],\"errors\":{}}";
            }
            return contains;
        }

        public override void Delete(Identifier id)
        {
            Dictionary<string, object> pks = id.PrimaryKeys;

            string endPoint = baseUrl + $"/Delete/{this.TableName}";
            string call = $"/Delete/{this.TableName}/";
            foreach (var pk in pks)
            {
                call += $"{pk.Value}/";
            }
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = client.DeleteAsync(call).Result;
                    response.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
            }
        }

        public override T Get(Identifier id)
        {
            Dictionary<string, object> pks = id.PrimaryKeys;

            string endPoint = baseUrl + $"/Get/{this.TableName}";
            string call = $"/Get/{this.TableName}/";
            foreach (var pk in pks)
            {
                call += $"{pk.Value}/";
            }

            call = call.Substring(0, call.Length - 1);

            T item = new T();
            string strResponseValue = string.Empty;

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = client.GetAsync(call).Result;
                    response.EnsureSuccessStatusCode();
                    strResponseValue = response.Content.ReadAsStringAsync().Result;
                }
                item = JsonSerializer.Deserialize<T>(strResponseValue);
            }
            catch (Exception ex)
            {
                strResponseValue = "{\"errorMessages\":[\"" + ex.Message.ToString() + "\"],\"errors\":{}}";
            }
            return item;
        }

        public override List<T> GetAll()
        {
            List<T> list = new List<T>();

            string endPoint = baseUrl + $"/GetAll/{this.TableName}";
            string call = $"/GetAll/{this.TableName}";

            // replace with html call to localhost:5043/GetAll
            string strResponseValue = string.Empty;

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = client.GetAsync(call).Result;
                    response.EnsureSuccessStatusCode();
                    strResponseValue = response.Content.ReadAsStringAsync().Result;
                }
                list = JsonSerializer.Deserialize<List<T>>(strResponseValue);
            }
            catch (Exception ex)
            {
                strResponseValue = "{\"errorMessages\":[\"" + ex.Message.ToString() + "\"],\"errors\":{}}";
            }
            return list;
        }

        public override void Update(Identifier id, T item)
        {
            Dictionary<string, object> pks = id.PrimaryKeys;

            string endPoint = baseUrl + $"/Update/{this.TableName}";
            string call = $"/Update/{this.TableName}/";
            // foreach (var pk in pks)
            // {
            //    call += $"{pk.Value}/";
            // }
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpContent content = new StringContent(JsonSerializer.Serialize(item), Encoding.UTF8, "application/json");

                    var response = client.PutAsync(call, content).Result;
                    response.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
