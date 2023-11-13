using ManageSellProduct.Enum;
using ManageSellProduct.Helpers;
using ManageSellProduct.Models;
using ManageSellProduct.Providers;

namespace ManageSellProduct.Business
{
    public class ProductBusiness
    {
        public static ProductModel[] GetProducts()
        {
            ProductModel[] products = ProductProvider.GetProducts();

            return products;
        }

        public static ProductModel GetProductByCode(string code)
        {
            ProductModel product = ProductProvider.GetProductByCode(code);

            return product;
        }

        public static bool IsExistProductByCategoryCode(string code)
        {
            return ProductProvider.IsExistProductByCategoryCode(code);
        }

        public static string AddProduct(ProductModel product)
        {
            if(ProductProvider.IsExistProductCode(product.Code))
            {
                return CommonEnum.NotAllowAdd;
            }

            return ProductProvider.AddProduct(product);
        }

        public static string EditProduct(ProductModel product)
        {
            string result = ProductProvider.EditProduct(product);

            return result;
        }

        public static string DeleteProduct(string code)
        {
            string result = ProductProvider.DeleteProduct(code);

            return result;
        }
    }
}
