using Core.ViewModels;
using Core.Services.Shared;
using Microsoft.AspNetCore.Mvc;
using Core.Services;

namespace Core.Controllers
{
    public partial class VisitorsController : Controller
    {
        private readonly TemplateDbContext _context;

        public VisitorsController(TemplateDbContext context)
        {
            _context = context;
        }

        public virtual IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public virtual IActionResult Create(VisitorViewModel model)
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