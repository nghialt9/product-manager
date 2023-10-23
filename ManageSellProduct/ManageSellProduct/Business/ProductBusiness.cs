using ManageSellProduct.Enum;
using ManageSellProduct.Helpers;
using ManageSellProduct.Models;
using ManageSellProduct.Providers;

namespace ManageSellProduct.Business
{
    public class ProductBusiness
    {
        public static Product[] GetProducts()
        {
            Product[] products = ProductProvider.GetProducts();

            return products;
        }

        public static Product GetProductByCode(string code)
        {
            Product product = ProductProvider.GetProductByCode(code);

            return product;
        }

        public static bool IsExistProductByCategoryCode(string code)
        {
            return ProductProvider.IsExistProductByCategoryCode(code);
        }

        public static string AddProduct(Product product)
        {
            if(ProductProvider.IsExistProductCode(product.Code))
            {
                return CommonEnum.NotAllowAdd;
            }

            return ProductProvider.AddProduct(product);
        }

        public static string EditProduct(Product product)
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
