using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Threading.Tasks;
using Core.Infrastructure.AspNetCore;
using Core.Services.Shared;
using Web.Infrastructure;
using Web.SignalR;
using Web.SignalR.Hubs.Events;

namespace Web.Areas.Example.VisitorsList
{
    [Area("Example")]
    public partial class VisitorsListController : AuthenticatedBaseController
    {
        private readonly SharedService _sharedService;
        private readonly IPublishDomainEvents _publisher;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public VisitorsListController(SharedService sharedService, IPublishDomainEvents publisher, IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _sharedService = sharedService;
            _publisher = publisher;
            _sharedLocalizer = sharedLocalizer;

            ModelUnbinderHelpers.ModelUnbinders.Add(typeof(IndexVisitorsViewModel), new SimplePropertyModelUnbinder());
        }

        [HttpGet]
        public virtual async Task<IActionResult> Index(IndexVisitorsViewModel model)
        {
            var visitors = await _sharedService.Handle(model.ToVisitorsIndexQuery());
            model.SetVisitors(visitors);

            return View(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Delete(Guid id)
        {
            // Query to delete visitor

            Alerts.AddSuccess(this, "Visitatore cancellato");

            return RedirectToAction(Actions.Index());
        }
    }
}