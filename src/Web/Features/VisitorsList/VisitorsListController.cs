using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Services.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Areas;

namespace Web.Features.VisitorsList
{
    public partial class VisitorsListController : AuthenticatedBaseController
    {
        private readonly ILogger<VisitorsListController> _logger;
        private readonly SharedService _sharedService;

        public VisitorsListController(ILogger<VisitorsListController> logger, SharedService sharedService)
        {
            _logger = logger;
            _sharedService = sharedService;
        }
        [HttpGet("/visitorslist")]
        public async virtual Task<IActionResult> Index(IndexViewModel model)
        {
            _logger.LogInformation("VisitorsListController.Index called");
            var visitors = await _sharedService.GetVisitorsByDate(DateTime.Today);
            if (!string.IsNullOrEmpty(model.Filter))
            {
                visitors = visitors.Where(v => v.Nome.Contains(model.Filter) || v.Cognome.Contains(model.Filter)).ToList();
            }
            model.Visitors = visitors;
            return View(model);
        }


        [HttpPost]
        public async virtual Task<IActionResult> Checkout(Guid id)
        {
            _logger.LogInformation($"VisitorsListController.Checkout called with id: {id}");
            await _sharedService.CheckoutVisitor(id);
            //await _sharedService.LogDatabaseContent();
            TempData["Message"] = "Check-Out Visitatore effettuato con successo!";
            return RedirectToAction("Index");
        }
    }
}
