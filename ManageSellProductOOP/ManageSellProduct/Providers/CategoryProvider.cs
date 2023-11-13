using ManageSellProduct.Enum;
using ManageSellProduct.Helpers;
using ManageSellProduct.Models;

namespace ManageSellProduct.Providers
{
    public class CategoryProvider
    {
        private static string FilePath = @$"{CommonEnum.RootFolder}\Category\Category.txt";

        public static CategoryModel[] GetCategorys()
        {
            CategoryModel[] categorys;
            string[] data = CommonFunction.GetData(FilePath);

            if (data.Length > 0)
            {
                categorys = new CategoryModel[data.Length];
                for (int i = 0; i < data.Length; i++)
                {
                    categorys[i] = StringToCategory(data[i]);
                }

                return categorys;
            }

            return new CategoryModel[] { };
        }

        public static CategoryModel GetCategory(string code)
        {
            CategoryModel[] categorys = GetCategorys();
            CategoryModel category = new CategoryModel();

            if (categorys.Length > 0)
            {
                for (int i = 0; i < categorys.Length; i++)
                {
                    if (code == categorys[i].CategoryCode)
                    {
                        category = categorys[i];
                        break;
                    }
                }
            }

            return category;
        }

        public static string AddCategory(CategoryModel category)
        {
            string[] data = CommonFunction.GetData(FilePath);
            string str = CategoryToString(category);

            if (data.Length == 0)
            {
                data = new string[1];
                data[0] = str;
                CommonFunction.SaveData(data, FilePath);
            }
            else
            {
                data = CommonFunction.ArrayAddItem(data, str);
                CommonFunction.SaveData(data, FilePath);
            }          

            return CommonEnum.Success;
        }

        public static bool IsExistCategoryCode(string code)
        {
            return CommonFunction.IsExistCode(code, FilePath);
        }

        public static string EditCategory(CategoryModel category)
        {
            string[] data = CommonFunction.GetData(FilePath);

            if (data.Length == 0)
            {
                return CommonEnum.ErrorPath;
            }
            else
            {
                for (int i = 0; i < data.Length; i++)
                {
                    if(category.CategoryCode == StringToCategory(data[i]).CategoryCode)
                    {
                        data[i] = CategoryToString(category);
                    }
                }

                CommonFunction.SaveData(data, FilePath);
            }

            return CommonEnum.Success;
        }

        public static string DeleteCategory(string code)
        {
            string[] data = CommonFunction.GetData(FilePath);

            if (data.Length == 0)
            {
                return CommonEnum.ErrorPath;
            }
            else
            {
                string[] newData = new string[data.Length - 1];
                int j = 0;
                for (int i = 0; i < data.Length; i++)
                {
                    if (code != StringToCategory(data[i]).CategoryCode)
                    {
                        newData[j++] = data[i];
                    }    
                }

                CommonFunction.SaveData(newData, FilePath);
            }

            return CommonEnum.Success;
        }

        private static CategoryModel StringToCategory(string data)
        {
            CategoryModel category = new CategoryModel();    
            string[] arr = data.Split(CommonEnum.Separator);
            category.CategoryCode = arr[0];
            category.CategoryName = arr[1];

            return category;
        }

        private static string CategoryToString(CategoryModel category)
        {
            return $"{category.CategoryCode}{CommonEnum.Separator}{category.CategoryName}";
        }
    }
}
