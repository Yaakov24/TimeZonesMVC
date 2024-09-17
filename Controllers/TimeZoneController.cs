using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TimeApp.Models;
using TimeZone.Models;


namespace TimeApp.Controllers
{
    public class TimeZoneController : Controller
    {
        private readonly ILogger<TimeZoneController> _logger;
        private readonly HttpClient _httpClient;
        private readonly TimeZoneNames _timeZoneNames;

        public TimeZoneController(ILogger<TimeZoneController> logger, HttpClient httpClient, TimeZoneNames timeZoneNames)
        {
            _logger = logger;
            _httpClient = httpClient;
            _timeZoneNames = timeZoneNames;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new TimeProps
            {
               
                    TimeZone = _timeZoneNames.GetTimeZoneList()
                
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(TimeProps model)
        {
            if (!string.IsNullOrEmpty(model.SelectedTimeZone))
            {
                //this is the calling of the API
                var response = await _httpClient.GetStringAsync($"https://timeapi.io/api/TimeZone/zone?timeZone={model.SelectedTimeZone}");

                var data = JObject.Parse(response);

                var rawTime = data["currentLocalTime"].Value<string>();
                DateTime dt = DateTime.Parse(rawTime);

                string timezone = data["timeZone"].Value<string>();
                string area = timezone.Split('/').Last();

                //I'm updating the model with the API response
                model.ApiResponseName = area;
                model.ApiResponseTime = dt.ToString("h:mm tt");
                model.ApiDayTime = dt.ToString("MM/dd/yyyy");
            }

            //populating the list again for the view

            model.TimeZone = _timeZoneNames.GetTimeZoneList();
            

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}


