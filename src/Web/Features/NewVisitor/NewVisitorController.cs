using Microsoft.AspNetCore.Mvc;

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
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "Errore: verifica i dati inseriti!";
                return View(model);
            }

            // Insert breakpoint here
            TempData["Message"] = "Nuovo visitatore registrato con successo!";
            return RedirectToAction("Index");
        }
    }
}
