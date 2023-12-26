using Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Core.Services.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Example.VisitorsList
{
    public class IndexVisitorsListViewModel : PagingViewModel
    {
        public IndexVisitorsListViewModel()
        {
            OrderBy = nameof(VisitorIndexViewModel.Email);
            OrderByDescending = false;
            Visitors = Array.Empty<VisitorIndexViewModel>();
        }

        [Display(Name = "Cerca")]
        public string Filter { get; set; }

        public IEnumerable<VisitorIndexViewModel> Visitors { get; set; }

        internal void SetVisitors(VisitorIndexDTO visitorIndexDTO)
        {
            Visitors = visitorIndexDTO.Visitors.Select(x => new VisitorIndexViewModel(x)).ToArray();
            TotalItems = visitorIndexDTO.Count;
        }



        public VisitorIndexQuery ToVisitorIndexQuery()
        {
            return new VisitorIndexQuery
            {
                Filter = Filter,
                Paging = new Core.Infrastructure.Paging
                {
                    OrderBy = OrderBy,
                    OrderByDescending = OrderByDescending,
                    Page = Page,
                    PageSize = PageSize
                }
            };
        }

        public override IActionResult GetRoute() => MVC.Example.Visitors.Index(this).GetAwaiter().GetResult();

        public string OrderbyUrl<TProperty>(IUrlHelper url, System.Linq.Expressions.Expression<Func<VisitorIndexViewModel, TProperty>> expression) => base.OrderbyUrl(url, expression);

        public string OrderbyCss<TProperty>(HttpContext context, System.Linq.Expressions.Expression<Func<VisitorIndexViewModel, TProperty>> expression) => base.OrderbyCss(context, expression);

        public string ToJson()
        {
            return JsonSerializer.ToJsonCamelCase(this);
        }
    }

    public class VisitorIndexViewModel
    {
        public VisitorIndexViewModel(VisitorIndexDTO.Visitor visitorIndexDTO)
        {
            this.Id = visitorIndexDTO.Id;
            this.Email = visitorIndexDTO.Email;
            this.Nome = visitorIndexDTO.Nome;
            this.Cognome = visitorIndexDTO.Cognome;
            this.Azienda = visitorIndexDTO.Azienda;
            this.Referente = visitorIndexDTO.Referente;
            this.TimestampEntrata = visitorIndexDTO.TimestampEntrata;
            this.TimestampUscita = visitorIndexDTO.TimestampUscita;
        }

        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Azienda { get; set; }
        public string Referente { get; set; }
        public DateTime? TimestampEntrata { get; set; }
        public DateTime? TimestampUscita { get; set; }
    }
}