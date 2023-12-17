using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Core.Services.Shared
{
    public class VisitorSelectQuery
    {
        public Guid IdCurrentVisitor { get; set; }
        public string Filter { get; set; }
    }

    public class VisitorSelectDTO
    {
        public IEnumerable<Visitor> Visitors { get; set; }
        public int Count { get; set; }

        public class Visitor
        {
            public Guid Id { get; set; }
            public string Nome { get; set; }
            public string Cognome { get; set; }
            public string Email { get; set; }
            public string Azienda { get; set; }
            public string Referente { get; set; }
            public DateTime? TimestampEntrata { get; set; }
            public DateTime? TimestampUscita { get; set; }
        }
    }

    public class VisitorIndexQuery
    {
        public Guid IdCurrentVisitor { get; set; }
        public string Filter { get; set; }

        public Paging Paging { get; set; }
    }

    public class VisitorIndexDTO
    {
        public IEnumerable<Visitor> Visitors { get; set; }
        public int Count { get; set; }

        public class Visitor
        {
            public Guid Id { get; set; }
            public string Nome { get; set; }
            public string Cognome { get; set; }
            public string Email { get; set; }
            public string Azienda { get; set; }
            public string Referente { get; set; }
            public DateTime? TimestampEntrata { get; set; }
            public DateTime? TimestampUscita { get; set; }
        }
    }

    public class VisitorDetailQuery
    {
        public Guid Id { get; set; }
    }

    public class VisitorDetailDTO
    {
        public Guid Id { get; set; }
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
        /// <summary>
        /// Returns users for a select field
        /// </summary>
        /// <param name="qry"></param>
        /// <returns></returns>
        public async Task<VisitorSelectDTO> Handle(VisitorSelectQuery qry)
        {
            var queryable = _dbContext.Visitors
                .Where(x => x.Id != qry.IdCurrentVisitor);

            if (string.IsNullOrWhiteSpace(qry.Filter) == false)
            {
                queryable = queryable.Where(x => x.Email.Contains(qry.Filter, StringComparison.OrdinalIgnoreCase));
            }

            return new VisitorSelectDTO
            {
                Visitors = await queryable
                    .Select(x => new VisitorSelectDTO.Visitor
                    {
                        Id = x.Id,
                        Email = x.Email,
                        Nome = x.Nome,
                        Cognome = x.Cognome,
                        Azienda = x.Azienda,
                        Referente = x.Referente,
                        TimestampEntrata = x.TimestampEntrata,
                        TimestampUscita = x.TimestampUscita

                    })
                    .ToListAsync(),
                Count = await queryable.CountAsync(),
            };
        }
    }
}
