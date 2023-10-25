using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Search.Configuration;
using Search.Middleware;
using Search.Models;
using Search.Models.Users;
using System.Diagnostics;

namespace Search.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApiConfiguration _apiConfiguration;

        public UserController(ILogger<HomeController> logger, IOptions<ApiConfiguration> options)
        {
            _logger = logger;
            _apiConfiguration = options.Value;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }

        /// <summary>
        /// This method searches through userdata and returns matching users with FirstName/LastName/Email
        /// </summary>
        /// <param name="searchStr"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Search(string searchStr)
        {
            try
            {
                if (!string.IsNullOrEmpty(searchStr))
                {
                    var searchResult = await GetUserSearchData(searchStr);
                    if (searchResult != null)
                    {
                        return Json(searchResult);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return Json(null);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// This method calls the api(middleware) to get the user data for matched criteria
        /// </summary>
        /// <param name="searchStr"></param>
        /// <returns></returns>
        private async Task<List<User>> GetUserSearchData(string searchStr)
        {
            var users = new List<User>();

            ApiClient apiClient = new ApiClient();
            var userData = await apiClient.GetUserData(_apiConfiguration.BaseURL, searchStr);

            if (userData != null)
            {
                users = JsonConvert.DeserializeObject<List<User>>(userData);
            }
            return users;
        }
    }
}