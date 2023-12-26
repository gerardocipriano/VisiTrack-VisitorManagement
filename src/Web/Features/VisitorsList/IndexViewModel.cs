using System.Collections.Generic;
using Core.Services.Shared;

namespace Web.Features.VisitorsList
{
    public class IndexViewModel
    {
        public List<Visitor> Visitors { get; set; }
        public int TotalItems { get { return Visitors?.Count ?? 0; } }
        public string Filter { get; set; }

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
