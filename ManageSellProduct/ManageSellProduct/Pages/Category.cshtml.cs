using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ManageSellProduct.Pages
{
    public class CategoryModel : PageModel
    {
        private readonly ILogger<CategoryModel> _logger;

        public CategoryModel(ILogger<CategoryModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}