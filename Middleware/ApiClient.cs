using RestSharp;
using System.Net;
using System.Net.Http.Headers;

namespace Search.Middleware
{
    /// <summary>
    /// This has api client methods to/from, receive/send data to API
    /// </summary>
    public class ApiClient
    {
        /// <summary>
        /// Gets user search data from API
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <param name="searchStr"></param>
        /// <returns></returns>
        public async Task<string> GetUserData(string baseUrl, string searchStr)
        {
            HttpClient client = new HttpClient();

            string url = $"{baseUrl}/user/search/?searchStr={searchStr}";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return null;
        }
    }
}