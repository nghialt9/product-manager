using ManageSellProduct.Helpers;
using ManageSellProduct.Models;
using ManageSellProduct.Providers;

namespace ManageSellProduct.Business
{
    public class StockBusiness
    {
        public static Stock[] GetInventoryStatistics()
        {
            Stock[] stocks = new Stock[0];

            Product[] products = ProductProvider.GetProducts();
            DetailSellProduct[] detailSellProducts = DetailSellProductProvider.GetDetailSellProducts();

            if (products.Length > 0)
            {
                stocks = new Stock[products.Length];
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

        public static Stock[] GetStatisticsExpiredUse()
        {
            Stock[] stocks = new Stock[0];
            Product[] products = ProductProvider.GetProducts();
            DateTime now = DateTime.Now;

            if (products.Length > 0)
            {
                Product[] expireProducts = new Product[0];

                for (int i = 0; i < products.Length; i++)
                {
                    if (products[i].ExpiryDate < now)
                    {
                        expireProducts = CommonFunction.ArrayAddItem(expireProducts, products[i]);
                    }
                }

                stocks = new Stock[expireProducts.Length];

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
