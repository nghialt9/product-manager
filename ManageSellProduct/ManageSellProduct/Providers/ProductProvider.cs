using ManageSellProduct.Enum;
using ManageSellProduct.Helpers;
using ManageSellProduct.Models;

namespace ManageSellProduct.Providers
{
    public class ProductProvider
    {
        private static string FilePath = @$"{CommonEnum.RootFolder}\Product\Product.txt";

        public static Product[] GetProducts()
        {
            Product[] products;
            string[] data = CommonFunction.GetData(FilePath);

            if (data.Length > 0)
            {
                products = new Product[data.Length];
                for (int i = 0; i < data.Length; i++)
                {
                    products[i] = StringToProduct(data[i]);
                }

                return products;
            }

            return new Product[] { };
        }

        public static Product GetProductByCode(string productCode)
        {
            Product[] products = GetProducts();
            Product product = new Product();
            if (products.Length > 0)
            {
                for (int i = 0; i < products.Length; i++)
                {
                    if (productCode == products[i].Code)
                    {
                        product = products[i];
                        break;
                    }
                }
            }

            return product;
        }

        public static bool IsExistProductByCategoryCode(string categoryCode)
        {
            Product[] products = GetProducts();
            bool result = false;
            if (products.Length > 0)
            {
                for (int i = 0; i < products.Length; i++)
                {
                    if (categoryCode == products[i].CategoryCode)
                    {
                        result = true;
                        break;
                    }
                }
            }

            return result;
        }

        public static bool IsExistProductCode(string code)
        {
            return CommonFunction.IsExistCode(code, FilePath);
        }

        public static string AddProduct(Product product)
        {
            string[] data = CommonFunction.GetData(FilePath);
            string str = ProductToString(product);

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

        public static string EditProduct(Product product)
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
                    if(product.Code == StringToProduct(data[i]).Code)
                    {
                        data[i] = ProductToString(product);
                    }
                }

                CommonFunction.SaveData(data, FilePath);
            }

            return CommonEnum.Success;
        }

        public static string DeleteProduct(string code)
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
                    if (code != StringToProduct(data[i]).Code)
                    {
                        newData[j++] = data[i];
                    }    
                }

                CommonFunction.SaveData(newData, FilePath);
            }

            return CommonEnum.Success;
        }

        private static Product StringToProduct(string data)
        {
            Product product = new Product();    
            string[] arr = data.Split(CommonEnum.Separator);
            product.Code = arr[0];
            product.Name = arr[1];
            product.NumberDaysUse = CommonFunction.StringToInt(arr[2]);
            product.Manufacturer = arr[3];
            product.ManufactureDate = CommonFunction.StringToDate(arr[4]);
            product.CategoryCode = arr[5];
            product.CategoryName = GetNameCategory(arr[5]);
            product.Price = CommonFunction.StringToDecimal(arr[6]);
            product.Quantity = CommonFunction.StringToInt(arr[7]);

            return product;
        }

        private static string ProductToString(Product product, string formatDate = CommonEnum.Date)
        {
            return $"{product.Code}{CommonEnum.Separator}" +
                $"{product.Name}{CommonEnum.Separator}" +
                $"{product.NumberDaysUse}{CommonEnum.Separator}" +
                $"{product.Manufacturer}{CommonEnum.Separator}" +
                $"{CommonFunction.DateToString(product.ManufactureDate, formatDate)}{CommonEnum.Separator}" +
                $"{product.CategoryCode}{CommonEnum.Separator}" +
                $"{product.Price}{CommonEnum.Separator}{product.Quantity}";
        }

        private static string GetNameCategory(string code)
        {
            Category[] categories = CategoryProvider.GetCategorys();
            if (categories.Length > 0)
            {
                for (int i = 0; i < categories.Length; i++)
                {
                    if (code == categories[i].Code) {
                        code = categories[i].Name;
                        break;
                    }
                }
            }

            return code;
        }
    }
}
