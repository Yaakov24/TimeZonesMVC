using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace TimeApp.Models
{
    public class TimeProps
    {
        
        

        public List<SelectListItem> TimeZone { get; set; }
        [BindProperty]
        public string SelectedTimeZone { get; set; }

        public string ApiDayTime { get; set; }
        public string ApiResponseTime { get; set; }
        public string ApiResponseName { get; set; }

    }
}
