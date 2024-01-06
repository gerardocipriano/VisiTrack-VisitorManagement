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

            model.TotalItems = await _sharedService.GetDailyVisitorsCount(DateTime.Today);

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

        [HttpGet("/search")]
        public async virtual Task<IActionResult> Search(string Ricerca)
        {
            try
            {
                // Call the Search method in your shared service
                var resultDTO = await _sharedService.Search(Ricerca);

                // Convert each VisitorSelectDTO.Visitor to a Visitor
                var results = resultDTO.SelectMany(dto => dto.Visitors.Select(visitor => new Core.Services.Shared.Visitor
                {
                    Nome = visitor.Nome,
                    Cognome = visitor.Cognome,
                    Email = visitor.Email,
                    Azienda = visitor.Azienda,
                    Referente = visitor.Referente,
                    TimestampEntrata = (DateTime)visitor.TimestampEntrata,
                })).ToList();

                // Create a new instance of your IndexViewModel
                var model = new Web.Features.VisitorsList.IndexViewModel
                {
                    Visitors = results,
                    TotalItems = resultDTO.Count
                };

                // Return the Index view with the model
                return View("Index", model);
            }
            catch (Exception ex)
            {
                // Log the exception details here
                System.Diagnostics.Debug.WriteLine($"Exception: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack Trace: {ex.StackTrace}");

                if (ex.InnerException != null)
                {
                    System.Diagnostics.Debug.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                    System.Diagnostics.Debug.WriteLine($"Inner Exception Stack Trace: {ex.InnerException.StackTrace}");
                }

                TempData["Message"] = "An error occurred while performing the search. Please try again.";
                TempData["IsError"] = true;
                return View("Index");
            }
        }
    }
}
