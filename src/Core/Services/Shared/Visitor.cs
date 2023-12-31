using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Services.Shared
{
    public class Visitor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Email { get; set; }
        public string Azienda { get; set; }
        public string Referente { get; set; }

        [Timestamp]
        public DateTime TimestampEntrata { get; set; }
        [Timestamp]
        public DateTime? TimestampUscita { get; set; }
    }
}