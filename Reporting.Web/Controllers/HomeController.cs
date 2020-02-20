using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reporting.Contracts.Employee;
using Reporting.Contracts.Misc;
using Reporting.Contracts.Token;
using Reporting.Services.Interfaces;
using Reporting.Web.Models;

namespace Reporting.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebServiceManager _webServiceManager;
        private readonly ISettings _settings;
        private readonly IEmployeeArrivalsService _employeeArrivalsService;
        private readonly IHistoryEventsService _historyEventsService;
        private static TokenResponse _token;
        private static SearchFilter _filter;
        public HomeController(IWebServiceManager webServiceManager, ISettings settings, IEmployeeArrivalsService employeeArrivalsService, IHistoryEventsService historyEventsService)
        {
            _webServiceManager = webServiceManager;
            _settings = settings;
            _employeeArrivalsService = employeeArrivalsService;
            _historyEventsService = historyEventsService;
        }

        public async Task<IActionResult> Index(int? pageNumber, SearchFilter searchFilter = null)
        {
            //check if the WebService has been called today
            var calledToday = await _historyEventsService.EventExists();
            if (!calledToday)
            {
                //if not call it and save the token
                //was about to save the token in a cookie/session/tempdata, but GDPR nonesense makes it harder so for the purpose of this test app it's in a static var
                var callback = Url.Action("ReceiveArrivals", "Home", null, Request.Scheme);
                string date = (DateTime.Today).ToString("yyyy-MM-dd");
                var result = await _webServiceManager.Subscribe(date, callback);
                _token = result;
                //return an interesting waiting view, until the arrivals have been received and saved to the db
                return View("GeneratingArrivals");
            }

            if (pageNumber != null && _filter != null)
            {
                searchFilter = _filter;
            }
            if (searchFilter == null)
            {
                searchFilter = new SearchFilter();
            }
            //get arrivals from db and show them
            var employeeArrivals = await _employeeArrivalsService.GetViewArrivals(pageNumber ?? 1, _settings.PageSize, searchFilter);

            ViewData["SearchFilter"] = searchFilter;
            _filter = searchFilter;
            return View(employeeArrivals);

        }

        public async Task<IActionResult> ReceiveArrivals([FromBody]List<EmployeeArrivalRequest> arrivals)
        {
            //receive arrivals and check token
            var requestToken = Request.Headers["X-Fourth-Token"];
            if (!string.IsNullOrEmpty(requestToken) && (_token.Token.Equals(requestToken)))
            {
                //if valid, save to db and create history event for reference
                await _employeeArrivalsService.AddMany(arrivals);
                await _historyEventsService.AddEvent();
                return Ok();
            }
            else
            {
                return BadRequest("Token error");
            }

        }

        public async Task<bool> CheckIfReady()
        {
            return await _historyEventsService.EventExists();
        }
    }
}
