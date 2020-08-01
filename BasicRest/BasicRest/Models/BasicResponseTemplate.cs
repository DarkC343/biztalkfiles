using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BasicRest.Controllers
{
    public class BasicResponseTemplate
    {
        public bool Success { get; set; }
        public string Error { get; set; }
    }
}