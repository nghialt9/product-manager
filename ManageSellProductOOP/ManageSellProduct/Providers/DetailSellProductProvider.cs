using ManageSellProduct.Enum;
using ManageSellProduct.Helpers;
using ManageSellProduct.Models;

namespace ManageSellProduct.Providers
{
    public class DetailSellProductProvider
    {
        private static string FilePath = @$"{CommonEnum.RootFolder}\DetailSellProduct\DetailSellProduct.txt";

        public static List<DetailSellProductModel> GetDetailSellProducts()
        {
            List<DetailSellProductModel> detailSellProducts = new List<DetailSellProductModel>();
            List<string> data = CommonFunction.GetData(FilePath);

            if (data.Count > 0)
            {
                foreach (string item in data)
                {
                    detailSellProducts.Add(StringToDetailSellProduct(item));
                }
            }

            return detailSellProducts;
        }

        public static List<DetailSellProductModel> GetDetailSellProducts(string sellInvoiceCode)
        {
            List<DetailSellProductModel> detailSellProducts = GetDetailSellProducts();

            return detailSellProducts.Where(d => d.SellInvoiceCode == sellInvoiceCode).ToList();
        }

        public static string AddDetailSellProducts(List<DetailSellProductModel> detailSellProducts)
        {
            List<string> data = CommonFunction.GetData(FilePath);

            foreach (DetailSellProductModel detailSellProductModel in detailSellProducts)
            {
                data.Add(DetailSellProductToString(detailSellProductModel));
            }

            CommonFunction.SaveData(data, FilePath);

            return CommonEnum.Success;
        }

        public static string EditDetailSellProducts(List<DetailSellProductModel> detailSellProducts)
        {
            List<string> data = CommonFunction.GetData(FilePath);

            if (data.Count == 0)
            {
                return CommonEnum.ErrorPath;
            }
            else
            {
                //remove old DetailSellProduct
                data = DeleteDetailSellProducts(detailSellProducts[0].SellInvoiceCode, false);

                foreach (DetailSellProductModel detailSellProduct in detailSellProducts)
                {
                    data.Add(DetailSellProductToString(detailSellProduct));
                }

                CommonFunction.SaveData(data, FilePath);
            }

            return CommonEnum.Success;
        }

        public static List<string> DeleteDetailSellProducts(string sellInvoiceCode, bool isSave = true)
        {
            List<string> data = CommonFunction.GetData(FilePath);

            if (data.Count == 0)
            {
                return data;
            }
            else
            {
                data = data.Where(d => StringToDetailSellProduct(d).SellInvoiceCode != sellInvoiceCode).ToList();

                if (isSave)
                {
                    CommonFunction.SaveData(data, FilePath);
                }   
            }

            return data;
        }

        public static string DeleteDetailSellProduct(string sellInvoiceCode, string productCode)
        {
            List<string> data = CommonFunction.GetData(FilePath);

            data = data.Where(d => IsDetailSellProduct(d, sellInvoiceCode, productCode)).ToList();
            CommonFunction.SaveData(data, FilePath);

            return CommonEnum.Success;
        }

        private static bool IsDetailSellProduct(string data, string sellInvoiceCode, string productCode)
        {
            DetailSellProductModel detailSellProduct = StringToDetailSellProduct(data);
            return (sellInvoiceCode == detailSellProduct.SellInvoiceCode && productCode == detailSellProduct.ProductCode) == false;
        }

        private static DetailSellProductModel StringToDetailSellProduct(string data)
        {
            DetailSellProductModel detailSellProduct = new DetailSellProductModel();    
            string[] arr = data.Split(CommonEnum.Separator);
            detailSellProduct.SellInvoiceCode = arr[0];
            detailSellProduct.ProductName = arr[1];
            detailSellProduct.ProductCode = arr[2];
            detailSellProduct.Quantity = CommonFunction.StringToInt(arr[3]);
            detailSellProduct.Price = CommonFunction.StringToDecimal(arr[4]);

            return detailSellProduct;
        }

        private static string DetailSellProductToString(DetailSellProductModel detailSellProduct)
        {
            return $"{detailSellProduct.SellInvoiceCode}{CommonEnum.Separator}" +
                $"{detailSellProduct.ProductName}{CommonEnum.Separator}" +
                $"{detailSellProduct.ProductCode}{CommonEnum.Separator}" +
                $"{detailSellProduct.Quantity}{CommonEnum.Separator}" +
                $"{detailSellProduct.Price}";
        }
    }
}
