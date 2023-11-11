using ManageSellProduct.Enum;
using ManageSellProduct.Helpers;
using ManageSellProduct.Models;

namespace ManageSellProduct.Providers
{
    public class DetailSellProductProvider
    {
        private static string FilePath = @$"{CommonEnum.RootFolder}\DetailSellProduct\DetailSellProduct.txt";

        public static DetailSellProduct[] GetDetailSellProducts()
        {
            DetailSellProduct[] detailSellProducts;
            string[] data = CommonFunction.GetData(FilePath);

            if (data.Length > 0)
            {
                detailSellProducts = new DetailSellProduct[data.Length];
                for (int i = 0; i < data.Length; i++)
                {
                    detailSellProducts[i] = StringToDetailSellProduct(data[i]);
                }

                return detailSellProducts;
            }

            return new DetailSellProduct[] { };
        }

        public static DetailSellProduct[] GetDetailSellProducts(string sellInvoiceCode)
        {
            DetailSellProduct[] detailSellProducts;
            string[] data = CommonFunction.GetData(FilePath);
            data = data.Where(x => x.StartsWith(sellInvoiceCode)).ToArray();

            if (data.Length > 0)
            {
                detailSellProducts = new DetailSellProduct[data.Length];
                for (int i = 0; i < data.Length; i++)
                {
                    detailSellProducts[i] = StringToDetailSellProduct(data[i]);
                }

                return detailSellProducts;
            }

            return new DetailSellProduct[] { };
        }

        public static string AddDetailSellProducts(DetailSellProduct[] detailSellProducts)
        {
            string[] data = CommonFunction.GetData(FilePath);

            if (data.Length == 0)
            {
                data = new string[detailSellProducts.Length];

                for (int i = 0; i < detailSellProducts.Length; i++)
                {
                    data[i] = DetailSellProductToString(detailSellProducts[i]);
                }
                
                CommonFunction.SaveData(data, FilePath);
            }
            else
            {
                string[] newData = new string[data.Length + detailSellProducts.Length]; 
                for (int i = 0; i < data.Length; i++)
                {
                    newData[i] = data[i];
                }

                for (int i = 0; i < detailSellProducts.Length; i++)
                {
                    newData[data.Length + i] = DetailSellProductToString(detailSellProducts[i]);
                }

                CommonFunction.SaveData(newData, FilePath);
            }          

            return CommonEnum.Success;
        }

        public static string EditDetailSellProducts(DetailSellProduct[] detailSellProducts)
        {
            string[] data = CommonFunction.GetData(FilePath);

            if (data.Length == 0)
            {
                return CommonEnum.ErrorPath;
            }
            else
            {
                //remove old DetailSellProduct
                string[] newData = DeleteDetailSellProducts(detailSellProducts[0].SellInvoiceCode, false);

                //add new DetailSellProduct
                string[] editData = new string[newData.Length + detailSellProducts.Length];
                for (int i = 0; i < newData.Length; i++)
                {
                    editData[i] = newData[i];
                }

                for (int i = 0; i < detailSellProducts.Length; i++)
                {
                    editData[newData.Length + i] = DetailSellProductToString(detailSellProducts[i]);
                }

                CommonFunction.SaveData(editData, FilePath);
            }

            return CommonEnum.Success;
        }

        public static string[] DeleteDetailSellProducts(string sellInvoiceCode, bool isSave = true)
        {
            string[] data = CommonFunction.GetData(FilePath);
            string[] newData = new string[0];

            if (data.Length == 0)
            {
                return newData;
            }
            else
            {
                DetailSellProduct[] oldDetailSellProducts = GetDetailSellProducts(sellInvoiceCode);
                newData = new string[data.Length - oldDetailSellProducts.Length];
                int j = 0;
                for (int i = 0; i < data.Length; i++)
                {
                    if (sellInvoiceCode != StringToDetailSellProduct(data[i]).SellInvoiceCode)
                    {
                        newData[j++] = data[i];
                    }
                }

                if (isSave)
                {
                    CommonFunction.SaveData(newData, FilePath);
                }   
            }

            return newData;
        }

        public static string DeleteDetailSellProduct(string sellInvoiceCode, string productCode)
        {
            string[] data = CommonFunction.GetData(FilePath);
            string[] newData;

            if (data.Length == 0)
            {
                return CommonEnum.ErrorPath;
            }
            else
            {
                newData = new string[data.Length - 1];
                int j = 0;
                for (int i = 0; i < data.Length; i++)
                {
                    DetailSellProduct detailSellProduct = StringToDetailSellProduct(data[i]);

                    if (sellInvoiceCode == detailSellProduct.SellInvoiceCode && productCode == detailSellProduct.ProductCode)
                    {
                        continue;
                    }

                    newData[j++] = data[i];
                }

                CommonFunction.SaveData(newData, FilePath);
            }

            return CommonEnum.Success;
        }

        private static DetailSellProduct StringToDetailSellProduct(string data)
        {
            DetailSellProduct detailSellProduct = new DetailSellProduct();    
            string[] arr = data.Split(CommonEnum.Separator);
            detailSellProduct.SellInvoiceCode = arr[0];
            detailSellProduct.ProductName = arr[1];
            detailSellProduct.ProductCode = arr[2];
            detailSellProduct.Quantity = CommonFunction.StringToInt(arr[3]);
            detailSellProduct.Price = CommonFunction.StringToDecimal(arr[4]);

            return detailSellProduct;
        }

        private static string DetailSellProductToString(DetailSellProduct detailSellProduct, string formatDate = CommonEnum.Date)
        {
            return $"{detailSellProduct.SellInvoiceCode}{CommonEnum.Separator}" +
                $"{detailSellProduct.ProductName}{CommonEnum.Separator}" +
                $"{detailSellProduct.ProductCode}{CommonEnum.Separator}" +
                $"{detailSellProduct.Quantity}{CommonEnum.Separator}" +
                $"{detailSellProduct.Price}";
        }
    }
}
