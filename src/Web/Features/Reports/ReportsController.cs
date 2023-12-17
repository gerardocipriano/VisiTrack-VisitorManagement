using Core.Services.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Web.Areas;

namespace Web.Features.Reports
{
    public partial class ReportsController : AuthenticatedBaseController
    {
        private readonly SharedService _sharedService;

        public ReportsController(SharedService sharedService)
        {
            _sharedService = sharedService;
        }

        [HttpGet("/reports")]
        public virtual IActionResult Index()
        {
            return View();
        }

        [HttpGet("/reports/getweeklyvisitors")]
        public async virtual Task<IActionResult> GetWeeklyVisitors(DateTime startDate, DateTime endDate)
        {
            // Fetch visitor count from your in-memory database here
            // This is just a placeholder
            var visitorCount = await _sharedService.GetVisitorCount(startDate, endDate);

            return Json(visitorCount);
        }
    }
}
