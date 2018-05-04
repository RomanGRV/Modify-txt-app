using System.Web;

namespace WebApplication.Models
{
    public class Upload
    {
        public string Search { get; set; }
        public HttpPostedFileBase File { get; set; }
    }
}