using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Threading.Tasks;
using Core.Infrastructure.AspNetCore;
using Core.Services.Shared;
using Web.Infrastructure;
using Web.SignalR;
using Web.SignalR.Hubs.Events;
using System.Security.Cryptography;
using System.Text;

namespace Web.Areas.Admin.Users
{
    [Area("Admin")]
    public partial class UsersController : AuthenticatedBaseController
    {
        private readonly SharedService _sharedService;
        private readonly IPublishDomainEvents _publisher;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public UsersController(SharedService sharedService, IPublishDomainEvents publisher, IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _sharedService = sharedService;
            _publisher = publisher;
            _sharedLocalizer = sharedLocalizer;

            ModelUnbinderHelpers.ModelUnbinders.Add(typeof(IndexViewModel), new SimplePropertyModelUnbinder());
        }

        [HttpGet]
        public virtual async Task<IActionResult> Index(IndexViewModel model)
        {
            var users = await _sharedService.Query(model.ToUsersIndexQuery());
            model.SetUsers(users);

            return View(model);
        }

        [HttpGet]
        public virtual IActionResult New()
        {
            return RedirectToAction(Actions.Edit());
        }

        [HttpGet]
        public virtual async Task<IActionResult> Edit(Guid? id)
        {
            var model = new EditViewModel();

            if (id.HasValue)
            {
                model.SetUser(await _sharedService.Query(new UserDetailQuery
                {
                    Id = id.Value,
                }));
            }



            return View(model);
        }

        [HttpPost]
        public async virtual Task<IActionResult> Edit(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = model.Id.HasValue ? await _sharedService.GetUserById(model.Id.Value) : new User();

                user.Email = model.Email;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.NickName = model.NickName;

                if (!string.IsNullOrWhiteSpace(model.Password))
                {
                    var sha256 = SHA256.Create();
                    user.Password = Convert.ToBase64String(sha256.ComputeHash(Encoding.ASCII.GetBytes(model.Password)));
                }

                await _sharedService.SaveUser(user);

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Delete(Guid id)
        {
            // Query to delete user

            Alerts.AddSuccess(this, "Utente cancellato");

            return RedirectToAction(Actions.Index());
        }
    }
}
