using System.ComponentModel.DataAnnotations;

namespace Web.Features.NewVisitor
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
}