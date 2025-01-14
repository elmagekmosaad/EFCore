using EFCore.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using Web.Models;
using static EFCore.BL.Constants.Permissions;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        HttpClient client = new HttpClient();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            intial();
        }
        private void intial()
        {
            client.BaseAddress = new Uri("http://localhost:5011");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<IActionResult> Index()
        {
            var getData = await client.GetAsync("api/Computer/mvc");
            if (getData.IsSuccessStatusCode)
            {
                var resultAsString = await getData.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<IEnumerable<ComputerDto>>(resultAsString);
                return View(result);
            }
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
           
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
