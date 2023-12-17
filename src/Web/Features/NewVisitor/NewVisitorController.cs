using Core.Services.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Web.Features.NewVisitor
{
    public partial class NewVisitorController : Controller
    {
        private readonly SharedService _sharedService;

        public NewVisitorController(SharedService sharedService)
        {
            _sharedService = sharedService;
        }

        [HttpGet("/newvisitor")]
        public virtual IActionResult Index()
        {
            var model = new NewVisitorViewModel();
            return View(model);
        }

        [HttpPost("/newvisitor")]
        public async virtual Task<IActionResult> NewVisitor(NewVisitorViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Message"] = "Errore: verifica i dati inseriti!";
                    TempData["IsError"] = true;
                    return View("Index", model);
                }

                // Create a new AddOrUpdateVisitorCommand
                var cmd = new AddOrUpdateVisitorCommand
                {
                    Nome = model.Nome,
                    Cognome = model.Cognome,
                    Email = model.Mail,
                    Azienda = model.Azienda,
                    Referente = model.Referente,
                    TimestampEntrata = DateTime.Now
                };

                // Use your SharedService to handle the command
                await _sharedService.Handle(cmd);

                // Log the content of the database
                await _sharedService.LogDatabaseContent();

                TempData["Message"] = "Nuovo visitatore registrato con successo!";
                return RedirectToAction("Index");
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

                TempData["Message"] = "Si è verificato un errore durante la registrazione del nuovo visitatore. Riprova.";
                TempData["IsError"] = true;
                return View("Index", model);
            }
        }
    }
}
