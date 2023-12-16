using Microsoft.AspNetCore.Mvc;

namespace Web.Features.NewVisitor
{
    public partial class NewVisitorController : Controller
    {
        [HttpGet("/newvisitor")]
        public virtual IActionResult Index()
        {
            return View();
        }
    }
}
