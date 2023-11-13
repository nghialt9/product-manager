using ManageSellProduct.Helpers;
using ManageSellProduct.Models;
using ManageSellProduct.Providers;

namespace ManageSellProduct.Business
{
    public class StockBusiness
    {
        public static StockModel[] GetInventoryStatistics()
        {
            StockModel[] stocks = new StockModel[0];

            ProductModel[] products = ProductProvider.GetProducts();
            DetailSellProductModel[] detailSellProducts = DetailSellProductProvider.GetDetailSellProducts();

            if (products.Length > 0)
            {
                stocks = new StockModel[products.Length];
                for (int i = 0; i < products.Length; i++)
                {
                    int quantity = products[i].Quantity;
                    if (detailSellProducts.Length > 0)
                    {
                        foreach (DetailSellProduct detailSellProduct in detailSellProducts)
                        {
                            if (detailSellProduct.ProductCode == products[i].Code)
                            {
                                quantity = quantity - detailSellProduct.Quantity;
                            }
                        }
                    }
                    stocks[i].ProductName = products[i].Name;
                    stocks[i].ProductCode = products[i].Code;
                    stocks[i].Quantity = quantity;
                }
            }

            return stocks;
        }

        public static StockModel[] GetStatisticsExpiredUse()
        {
            StockModel[] stocks = new StockModel[0];
            ProductModel[] products = ProductProvider.GetProducts();
            DateTime now = DateTime.Now;

            if (products.Length > 0)
            {
                ProductModel[] expireProducts = new ProductModel[0];

                for (int i = 0; i < products.Length; i++)
                {
                    if (products[i].ExpiryDate < now)
                    {
                        expireProducts = CommonFunction.ArrayAddItem(expireProducts, products[i]);
                    }
                }

                stocks = new StockModel[expireProducts.Length];

                for (int i = 0; i < expireProducts.Length; i++)
                {
                    stocks[i].ProductName = expireProducts[i].Name;
                    stocks[i].ProductCode = expireProducts[i].Code;
                    stocks[i].Quantity = expireProducts[i].Quantity;
                    stocks[i].ExpiryDate = expireProducts[i].ExpiryDate;
                }
            }

            return stocks;
        }


    }
}
