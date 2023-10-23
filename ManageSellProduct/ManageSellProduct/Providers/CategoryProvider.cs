using ManageSellProduct.Enum;
using ManageSellProduct.Helpers;
using ManageSellProduct.Models;

namespace ManageSellProduct.Providers
{
    public class CategoryProvider
    {
        private static string FilePath = @$"{CommonEnum.RootFolder}\Category\Category.txt";

        public static Category[] GetCategorys()
        {
            Category[] categorys;
            string[] data = CommonFunction.GetData(FilePath);

            if (data.Length > 0)
            {
                categorys = new Category[data.Length];
                for (int i = 0; i < data.Length; i++)
                {
                    categorys[i] = StringToCategory(data[i]);
                }

                return categorys;
            }

            return new Category[] { };
        }

        public static Category GetCategory(string code)
        {
            Category[] categorys = GetCategorys();
            Category category = new Category();

            if (categorys.Length > 0)
            {
                for (int i = 0; i < categorys.Length; i++)
                {
                    if (code == categorys[i].Code)
                    {
                        category = categorys[i];
                        break;
                    }
                }
            }

            return category;
        }

        public static string AddCategory(Category category)
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

        public static string EditCategory(Category category)
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
                    if(category.Code == StringToCategory(data[i]).Code)
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
                    if (code != StringToCategory(data[i]).Code)
                    {
                        newData[j++] = data[i];
                    }    
                }

                CommonFunction.SaveData(newData, FilePath);
            }

            return CommonEnum.Success;
        }

        private static Category StringToCategory(string data)
        {
            Category category = new Category();    
            string[] arr = data.Split(CommonEnum.Separator);
            category.Code = arr[0];
            category.Name = arr[1];

            return category;
        }

        private static string CategoryToString(Category category)
        {
            return $"{category.Code}{CommonEnum.Separator}{category.Name}";
        }
    }
}
