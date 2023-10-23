using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ManageSellProduct.Pages
{
    public class SellInvoiceModel : PageModel
    {
        private readonly ILogger<SellInvoiceModel> _logger;

        public SellInvoiceModel(ILogger<SellInvoiceModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}