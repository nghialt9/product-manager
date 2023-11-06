using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ManageSellProduct.Pages
{
    public class ImportInvoiceModel : PageModel
    {
        private readonly ILogger<ImportInvoiceModel> _logger;

        public ImportInvoiceModel(ILogger<ImportInvoiceModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}