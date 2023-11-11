using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ManageSellProduct.Pages
{
    public class StockModel : PageModel
    {
        private readonly ILogger<StockModel> _logger;

        public StockModel(ILogger<StockModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}