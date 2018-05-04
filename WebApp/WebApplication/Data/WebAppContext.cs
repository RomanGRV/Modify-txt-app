using System.Data.Entity;

using WebApplication.Models;

namespace WebApplication.Data
{
    public class WebAppContext : DbContext
    {
        public WebAppContext() : base("WebAppConnectionString")
        {

        }
        public DbSet<Sentence> Sentences { get; set; }
    }
}