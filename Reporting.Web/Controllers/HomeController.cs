using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reporting.Contracts.Employee;
using Reporting.Services.Interfaces;
using Reporting.Web.Models;

namespace Reporting.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebServiceManager _webServiceManager;

        public HomeController(IWebServiceManager webServiceManager)
        {
            _webServiceManager = webServiceManager;
        }

        public async Task<IActionResult> Index()
        {
            var callback = Url.Action("ReceiveArrivals", "Home", null, Request.Scheme);
            string date = new DateTime(2020, 2, 18).ToString("yyyy-MM-dd");
            var result = await _webServiceManager.Subscribe(date, callback);
            return View();
        }

        public async Task<IActionResult> ReceiveArrivals([FromBody]List<EmployeeArrivalResponse> arrivals)
        {
            return Ok();
        }

    }
}
