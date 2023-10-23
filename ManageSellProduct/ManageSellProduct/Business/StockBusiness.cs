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

            if(products.Length > 0)
            {
                stocks = new Stock[products.Length];
                for (int i = 0; i < products.Length; i++)
                {
                    stocks[i].ProductName = products[i].Name;
                    stocks[i].ProductCode = products[i].Code;
                    stocks[i].Quantity = products[i].Quantity;
                }
            }

            return stocks;
        }

        public static Stock GetInventoryStatistics(string productCode)
        {
            Stock[] stocks = GetInventoryStatistics();
            Stock stock = new Stock();

            if (stocks.Length > 0)
            {
                for (int i = 0; i < stocks.Length; i++)
                {
                    if (productCode == stocks[i].ProductCode)
                    {
                        stock = stocks[i];
                        break;
                    }
                }
            }

            return stock;
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
                }
            }

            return stocks;
        }

        
    }
}
