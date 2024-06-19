using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;


namespace seclogin.Helpers
{
    public interface IApiHelper
    {

    }

    public class ApiHelper
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _configuration;

        public ApiHelper(HttpClient httpClient, IHttpContextAccessor contextAccessor, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _contextAccessor = contextAccessor;
            _configuration = configuration;
        }

        public async Task<(bool, string?)> Get(string url)
        {
            try
            {
                using var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);

                var responseMessage = await _httpClient.SendAsync(requestMessage);

                if (!responseMessage.IsSuccessStatusCode) return (false, null);

                var jsonContent = await responseMessage.Content.ReadAsStringAsync();

                var dictContent = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonContent);
                if (dictContent == null) 
                    return (false, null);
                return (true, dictContent["value"].ToString());


                return(true, jsonContent.ToString());
                
            }catch (Exception)
            {
                return (false, null);

            }
        }


        public async Task<(bool, string?)> Post(string data, string url)
        {
            try
            {
                using var requestMessage = new HttpRequestMessage(new HttpMethod("Post"), url);

                requestMessage.Content = new StringContent(data, Encoding.UTF8, "application/json");

                var responseMessage = await _httpClient.SendAsync(requestMessage);

                if (!responseMessage.IsSuccessStatusCode) return (false, null);

                var jsonContent = await responseMessage.Content.ReadAsStringAsync();

                return (true, jsonContent.ToString());
            }
            catch (Exception) { return (false, null); }

        }


        public async Task<(bool, string?)> Patch(string data, string url)
        {
            try
            {
                using var requestMessage = new HttpRequestMessage(new HttpMethod("Patch"), url);
                requestMessage.Content = new StringContent(data, Encoding.UTF8, "application/json");

                var responseMessage = await _httpClient.SendAsync(requestMessage);

                if (!responseMessage.IsSuccessStatusCode) return (false, null);

                var jsonContent = await responseMessage.Content.ReadAsStringAsync();
                return(true, jsonContent.ToString());
            }catch(Exception) { return (false, null); }
        }


        public async Task<(bool, string?)> Delete(string url)
        {
            try
            {
                using var requestMessage = new HttpRequestMessage(new HttpMethod("Delete"), url);

                var responseMessage = await _httpClient.SendAsync(requestMessage);

                if (!responseMessage.IsSuccessStatusCode) return (false, null);

                var jsonContent = await responseMessage.Content.ReadAsStringAsync();
                return(true, jsonContent.ToString());
            }catch(Exception) { return (false, null); }   
        }

    }
}
