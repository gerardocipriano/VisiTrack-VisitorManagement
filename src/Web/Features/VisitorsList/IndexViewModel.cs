using System.Collections.Generic;
using Core.Services.Shared;

namespace Web.Features.VisitorsList
{
    public class IndexViewModel
    {
        public List<Visitor> Visitors { get; set; }
        public int TotalItems { get; set; } // Add a setter to this property
        public string Filter { get; set; }
        public string Ricerca { get; set; } // Add this line

        public IndexViewModel()
        {
            Visitors = new List<Visitor>();
        }

        public IndexViewModel(List<Visitor> visitors)
        {
            Visitors = visitors;
        }
    }
}