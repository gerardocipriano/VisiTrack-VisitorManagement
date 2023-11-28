using System;
using System.ComponentModel.DataAnnotations;
using Template.Services.Shared;
using Template.Web.Infrastructure;

namespace Template.Web.Areas.Example.Users
{
    [TypeScriptModule("Example.Users.Server")]
    public class EditViewModel
    {
        public EditViewModel()
        {
        }

        public Guid? Id { get; set; }
        public string Email { get; set; }

        [Display(Name = "Nome")]
        public string FirstName { get; set; }
        [Display(Name = "Cognome")]
        public string LastName { get; set; }
        [Display(Name = "Nickname")]
        public string NickName { get; set; }

        public string ToJson()
        {
            return Infrastructure.JsonSerializer.ToJsonCamelCase(this);
        }

        public void SetUser(UserDetailDTO userDetailDTO)
        {
            if (userDetailDTO != null)
            {
                Id = userDetailDTO.Id;
                Email = userDetailDTO.Email;
                FirstName = userDetailDTO.FirstName;
                LastName = userDetailDTO.LastName;
                NickName = userDetailDTO.NickName;
            }
        }

        public AddOrUpdateUserCommand ToAddOrUpdateUserCommand()
        {
            return new AddOrUpdateUserCommand
            {
                Id = Id,
                Email = Email,
                FirstName = FirstName,
                LastName = LastName,
                NickName = NickName
            };
        }
    }
}