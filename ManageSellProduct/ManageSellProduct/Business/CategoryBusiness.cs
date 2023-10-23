using ManageSellProduct.Enum;
using ManageSellProduct.Helpers;
using ManageSellProduct.Models;
using ManageSellProduct.Providers;

namespace ManageSellProduct.Business
{
    public class CategoryBusiness
    {
        public static Category[] GetCategorys()
        {
            Category[] categorys = CategoryProvider.GetCategorys();

            return categorys;
        }

        public static Category GetCategory(string code)
        {
            Category category = CategoryProvider.GetCategory(code);

            return category;
        }

        public static string AddCategory(Category category)
        {
            if(CategoryProvider.IsExistCategoryCode(category.Code)) {
                return CommonEnum.NotAllowAdd;
            }

            return CategoryProvider.AddCategory(category);
        }

        public static string EditCategory(Category category)
        {
            string result = CategoryProvider.EditCategory(category);

            return result;
        }

        public static string DeleteCategory(string code)
        {
            if (ProductBusiness.IsExistProductByCategoryCode(code))
            {
                return CommonEnum.NotAllowDelete;
            }

            return CategoryProvider.DeleteCategory(code);
        }
    }
}
