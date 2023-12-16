using Core.ViewModels;
using Core.Services.Shared;
using Microsoft.AspNetCore.Mvc;
using Core.Services;

namespace Core.Controllers
{
    public class VisitorsController : Controller
    {
        private readonly VisitrackDbContext _context;

        public VisitorsController(VisitrackDbContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(VisitorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var visitor = new Visitor
                {
                    Nome = model.Nome,
                    Cognome = model.Cognome,
                    Email = model.Email,
                    Azienda = model.Azienda,
                    Referente = model.Referente
                };

                _context.Visitors.Add(visitor);
                _context.SaveChanges();

                return RedirectToAction("Create");
            }

            return View(model);
        }
    }
}