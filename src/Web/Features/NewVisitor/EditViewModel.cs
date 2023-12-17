using Core.Services.Shared;
using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Visitors
{
    public class EditVisitorViewModel
    {
        public class NewVisitorViewModel
        {
            [Required]
            [Display(Name = "Nome")]
            public string Nome { get; set; }

            [Required]
            [Display(Name = "Cognome")]
            public string Cognome { get; set; }

            [Required]
            [Display(Name = "Azienda")]
            public string Azienda { get; set; }

            [Required]
            [Display(Name = "Mail")]
            public string Mail { get; set; }

            [Required]
            [Display(Name = "Referente")]
            public string Referente { get; set; }
        }

        public void SetVisitor(VisitorDetailDTO visitor)
        {
            // Implement model
            throw new NotImplementedException();
        }

        public AddOrUpdateVisitorCommand ToAddOrUpdateVisitorCommand()
        {
            // Implement command creation from model data
            return new AddOrUpdateVisitorCommand();
        }
    }
}