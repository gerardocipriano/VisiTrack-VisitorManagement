using Microsoft.AspNetCore.Mvc;

namespace Web.Features.NewVisitor
{
    public class NewVisitor : Controller
    {
        [HttpGet("/newvisitor")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
