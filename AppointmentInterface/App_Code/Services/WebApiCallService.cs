using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Configuration;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace AppointmentInterface.App_Code.Services {
    public class WebApiCallService<T> {
        private HttpClient client;

        public WebApiCallService() {
            client = new HttpClient();
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["baseURI"]);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
            client.DefaultRequestHeaders.AcceptEncoding.Clear();
            client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
            client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("br"));
            client.Timeout = new TimeSpan(0,0,30);
        }

        public async Task<T> ApiPostRequest(string requestURI,StringContent content,HttpResponseMessage responseMessage = null) {
            try {
                content.Headers.Remove("Content-Type");
                content.Headers.TryAddWithoutValidation("Content-Type","application/json");

                responseMessage = client.PostAsync(requestURI,content).Result;
                var servResp = responseMessage.Content.ReadAsStringAsync().Result;
                T convertedResponse = JsonConvert.DeserializeObject<T>(servResp);

                return convertedResponse;

            } catch (Exception e) {
                Console.WriteLine(e);
                return default(T);
            }
        }
        
        public async Task<T> ApiPostRequest(string requestURI,StringContent content,string authorization,HttpResponseMessage responseMessage = null) {
            try {
                content.Headers.Remove("Content-Type");

                content.Headers.TryAddWithoutValidation("Content-Type","application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("nitrosoft",authorization);

                responseMessage = client.PostAsync(requestURI,content).Result;
                var servResp = responseMessage.Content.ReadAsStringAsync().Result;
                T convertedResponse = JsonConvert.DeserializeObject<T>(servResp);

                return convertedResponse;
            } catch (Exception e) {
                Console.WriteLine(e);
                return default(T);

            }
        }

        public async Task<T> ApiGetRequest(string requestURI,HttpResponseMessage httpResponse = null) {
            try {
                HttpResponseMessage responseMessage = client.GetAsync(requestURI).Result;
                var servResp = responseMessage.Content.ReadAsStringAsync().Result;
                T convertedResponse = JsonConvert.DeserializeObject<T>(servResp);

                return convertedResponse;
            } catch (Exception e) {
                Console.WriteLine(e);
                return default(T);
            }
        }

        public async Task<T> ApiGetRequest(string requestURI,string authorization,HttpResponseMessage responseMessage = null) {
            try {
                client.DefaultRequestHeaders.Add("Authorization",authorization);

                responseMessage = client.GetAsync(requestURI).Result;
                var servResp = responseMessage.Content.ReadAsStringAsync().Result;
                T convertedResponse = JsonConvert.DeserializeObject<T>(servResp);

                return convertedResponse;
            } catch (Exception e) {
                Console.WriteLine(e);
                return default(T);
            }
        }
    }


}