using ManageSellProduct.Enum;
using ManageSellProduct.Helpers;
using ManageSellProduct.Models;
using ManageSellProduct.Providers;

namespace ManageSellProduct.Business
{
    public class CategoryBusiness
    {
        public static CategoryModel[] GetCategorys()
        {
            CategoryModel[] categorys = CategoryProvider.GetCategorys();

            return categorys;
        }

        public static CategoryModel GetCategory(string code)
        {
            CategoryModel category = CategoryProvider.GetCategory(code);

            return category;
        }

        public static string AddCategory(CategoryModel category)
        {
            if(CategoryProvider.IsExistCategoryCode(category.CategoryCode)) {
                return CommonEnum.NotAllowAdd;
            }

            return CategoryProvider.AddCategory(category);
        }

        public static string EditCategory(CategoryModel category)
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
