using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _configuration;

        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public void OnGet()
        {
            var someData = _configuration.GetValue<string>("SomeData:Value");
            ViewData["SomeData"] = someData;

            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            ViewData["Env"] = env;

            var azureConf = _configuration.GetValue<string>("test");
            ViewData["azureConf"] = azureConf;
        }
    }
}