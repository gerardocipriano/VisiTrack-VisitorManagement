using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Services.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Web.Features.VisitorsList
{
    public partial class VisitorsListController : Controller
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
            return RedirectToAction("Index");
        }
    }
}
