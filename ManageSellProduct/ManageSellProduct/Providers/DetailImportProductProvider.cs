using ManageSellProduct.Enum;
using ManageSellProduct.Helpers;
using ManageSellProduct.Models;

namespace ManageSellProduct.Providers
{
    public class DetailImportProductProvider
    {
        private static string FilePath = @$"{CommonEnum.RootFolder}\DetailImportProduct\DetailImportProduct.txt";

        public static DetailImportProduct[] GetDetailImportProducts(string sellInvoiceCode)
        {
            DetailImportProduct[] detailImportProducts;
            string[] data = CommonFunction.GetData(FilePath);
            data = data.Where(x => x.StartsWith(sellInvoiceCode)).ToArray();

            if (data.Length > 0)
            {
                detailImportProducts = new DetailImportProduct[data.Length];
                for (int i = 0; i < data.Length; i++)
                {
                    detailImportProducts[i] = StringToDetailImportProduct(data[i]);
                }

                return detailImportProducts;
            }

            return new DetailImportProduct[] { };
        }

        public static string AddDetailImportProducts(DetailImportProduct[] detailImportProducts)
        {
            string[] data = CommonFunction.GetData(FilePath);

            if (data.Length == 0)
            {
                data = new string[detailImportProducts.Length];

                for (int i = 0; i < detailImportProducts.Length; i++)
                {
                    data[i] = DetailImportProductToString(detailImportProducts[i]);
                }
                
                CommonFunction.SaveData(data, FilePath);
            }
            else
            {
                string[] newData = new string[data.Length + detailImportProducts.Length]; 
                for (int i = 0; i < data.Length; i++)
                {
                    newData[i] = data[i];
                }

                for (int i = 0; i < detailImportProducts.Length; i++)
                {
                    newData[data.Length + i] = DetailImportProductToString(detailImportProducts[i]);
                }

                CommonFunction.SaveData(newData, FilePath);
            }          

            return CommonEnum.Success;
        }

        public static string EditDetailImportProducts(DetailImportProduct[] detailImportProducts)
        {
            string[] data = CommonFunction.GetData(FilePath);

            if (data.Length == 0)
            {
                return CommonEnum.ErrorPath;
            }
            else
            {
                //remove old DetailImportProduct
                string[] newData = DeleteDetailImportProducts(detailImportProducts[0].SellInvoiceCode, false);

                //add new DetailImportProduct
                string[] editData = new string[newData.Length + detailImportProducts.Length];
                for (int i = 0; i < newData.Length; i++)
                {
                    editData[i] = newData[i];
                }

                for (int i = 0; i < detailImportProducts.Length; i++)
                {
                    editData[newData.Length + i] = DetailImportProductToString(detailImportProducts[i]);
                }

                CommonFunction.SaveData(editData, FilePath);
            }

            return CommonEnum.Success;
        }

        public static string[] DeleteDetailImportProducts(string sellInvoiceCode, bool isSave = true)
        {
            string[] data = CommonFunction.GetData(FilePath);
            string[] newData = new string[0];

            if (data.Length == 0)
            {
                return newData;
            }
            else
            {
                DetailImportProduct[] oldDetailImportProducts = GetDetailImportProducts(sellInvoiceCode);
                newData = new string[data.Length - oldDetailImportProducts.Length];
                int j = 0;
                for (int i = 0; i < data.Length; i++)
                {
                    if (sellInvoiceCode != StringToDetailImportProduct(data[i]).SellInvoiceCode)
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

        private static DetailImportProduct StringToDetailImportProduct(string data)
        {
            DetailImportProduct detailImportProduct = new DetailImportProduct();    
            string[] arr = data.Split(CommonEnum.Separator);
            detailImportProduct.SellInvoiceCode = arr[0];
            detailImportProduct.ProductName = arr[1];
            detailImportProduct.ProductCode= arr[2];
            detailImportProduct.Quantity = CommonFunction.StringToInt(arr[3]);
            detailImportProduct.Price = CommonFunction.StringToDecimal(arr[4]);

            return detailImportProduct;
        }

        private static string DetailImportProductToString(DetailImportProduct detailImportProduct, string formatDate = CommonEnum.Date)
        {
            return $"{detailImportProduct.SellInvoiceCode}{CommonEnum.Separator}" +
                $"{detailImportProduct.ProductName}{CommonEnum.Separator}" +
                $"{detailImportProduct.ProductCode}{CommonEnum.Separator}" +
                $"{detailImportProduct.Quantity}{CommonEnum.Separator}" +
                $"{detailImportProduct.Price}";
        }
    }
}
