using Microsoft.AspNetCore.Mvc;
using System;

namespace Web.Features.NewVisitor
{
    public partial class NewVisitorController : Controller
    {
        [HttpGet("/newvisitor")]
        public virtual IActionResult Index()
        {
            var model = new NewVisitorViewModel();
            return View(model);
        }

        [HttpPost("/newvisitor")]
        public virtual IActionResult NewVisitor(NewVisitorViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Message"] = "Errore: verifica i dati inseriti!";
                    return View(model);
                }

                // Insert your code here that might throw an exception

                TempData["Message"] = "Nuovo visitatore registrato con successo!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Log the exception details here
                TempData["Message"] = "Si � verificato un errore durante la registrazione del nuovo visitatore. Riprova.";
                return View(model);
            }
        }

    }
}
