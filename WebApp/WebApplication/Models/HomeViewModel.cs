using System.Collections.Generic;

namespace WebApplication.Models
{
    public class HomeViewModel
    {
        public IEnumerable<Sentence> Sentences { get; set; }
        public SearchViewModel Search { get; set; }
    }
}