using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Shared
{
    public class AddOrUpdateVisitorCommand
    {
        public Guid? Id { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Email { get; set; }
        public string Azienda { get; set; }
        public string Referente { get; set; }
        public DateTime? TimestampEntrata { get; set; }
        public DateTime? TimestampUscita { get; set; }

    }

    public partial class SharedService
    {
        public async Task<Guid> Handle(AddOrUpdateVisitorCommand cmd)
        {
            var visitor = await _dbContext.Visitors
                .Where(x => x.Id == cmd.Id)
                .FirstOrDefaultAsync();

            if (visitor == null)
            {
                visitor = new Visitor
                {
                    Email = cmd.Email,
                };
                _dbContext.Visitors.Add(visitor);
            }

            visitor.Nome = cmd.Nome;
            visitor.Cognome = cmd.Cognome;
            visitor.Email = cmd.Email;
            visitor.Azienda = cmd.Azienda;
            visitor.Referente = cmd.Referente;

            if (cmd.TimestampEntrata.HasValue)
            {
                visitor.TimestampEntrata = cmd.TimestampEntrata.Value;
            }

            if (cmd.TimestampUscita.HasValue)
            {
                visitor.TimestampUscita = cmd.TimestampUscita.Value;
            }

            await _dbContext.SaveChangesAsync();

            return visitor.Id;
        }
    }
}
