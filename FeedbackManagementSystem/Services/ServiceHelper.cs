using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackManagementSystem.Services
{
    public class ServiceHelper
    {
        private const string Baseurl = "http://localhost:5070/";
        public async  static Task<T> GetAsync<T>(string endpoint)
        {
            using var client = new HttpClient();

            client.BaseAddress = new Uri(Baseurl);
            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage Res = await client.GetAsync(endpoint);

            if (Res.IsSuccessStatusCode)
            {
                var response = Res.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<T>(response);
            }
            return default;
        }

        public async static Task PostAsync<T>(string endpoint, T data)
        {
            using var client = new HttpClient();

            client.BaseAddress = new Uri(Baseurl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(endpoint, content);
        }

        public async static Task PutAsync<T>(string endpoint, T data)
        {
            using var client = new HttpClient();

            client.BaseAddress = new Uri(Baseurl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PutAsync(endpoint, content);
        }

        public async static Task DeleteAsync(string endpoint)
        {
            using var client = new HttpClient();

            client.BaseAddress = new Uri(Baseurl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.DeleteAsync(endpoint);
        }
    }
}