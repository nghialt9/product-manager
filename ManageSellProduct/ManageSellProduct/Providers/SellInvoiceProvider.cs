using ManageSellProduct.Enum;
using ManageSellProduct.Helpers;
using ManageSellProduct.Models;

namespace ManageSellProduct.Providers
{
    public class SellInvoiceProvider
    {
        private static string FilePath = @$"{CommonEnum.RootFolder}\SellInvoice\SellInvoice.txt";

        public static SellInvoice[] GetSellInvoices()
        {
            SellInvoice[] sellInvoices;
            string[] data = CommonFunction.GetData(FilePath);

            if (data.Length > 0)
            {
                sellInvoices = new SellInvoice[data.Length];
                for (int i = 0; i < data.Length; i++)
                {
                    sellInvoices[i] = StringToSellInvoice(data[i]);
                }

                return sellInvoices;
            }

            return new SellInvoice[] { };
        }

       public static SellInvoice GetSellInvoicesByCode(string code)
        {
            SellInvoice[] sellInvoices = GetSellInvoices();

            SellInvoice sellInvoice = new SellInvoice();
            if (sellInvoices.Length > 0)
            {
                for (int i = 0; i < sellInvoices.Length; i++)
                {
                    if (code == sellInvoices[i].Code)
                    {
                        sellInvoice = sellInvoices[i];
                        break;
                    }
                }
            }

            return sellInvoice;
        }

        public static SellInvoice GetDetailSellInvoice(string code)
        {
            SellInvoice sellInvoice = GetSellInvoicesByCode(code);
            sellInvoice.DetailSellProducts = DetailSellProductProvider.GetDetailSellProducts(code);

            return sellInvoice;
        }

        public static string AddSellInvoice(SellInvoice sellInvoice)
        {
            string[] data = CommonFunction.GetData(FilePath);
            string str = SellInvoiceToString(sellInvoice);

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

            DetailSellProductProvider.AddDetailSellProducts(sellInvoice.DetailSellProducts);

            return CommonEnum.Success;
        }

        public static bool IsExistSellInvoiceCode(string code)
        {
            return CommonFunction.IsExistCode(code, FilePath);
        }

        public static string EditSellInvoice(SellInvoice sellInvoice)
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
                    if(sellInvoice.Code == StringToSellInvoice(data[i]).Code)
                    {
                        data[i] = SellInvoiceToString(sellInvoice);
                    }
                }

                CommonFunction.SaveData(data, FilePath);
            }

            DetailSellProductProvider.EditDetailSellProducts(sellInvoice.DetailSellProducts);

            return CommonEnum.Success;
        }

        public static string DeleteSellInvoice(string code)
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
                    if (code != StringToSellInvoice(data[i]).Code)
                    {
                        newData[j++] = data[i];
                    }    
                }

                CommonFunction.SaveData(newData, FilePath);
            }

            DetailSellProductProvider.DeleteDetailSellProducts(code);

            return CommonEnum.Success;
        }

        private static SellInvoice StringToSellInvoice(string data)
        {
            SellInvoice sellInvoice = new SellInvoice();
            string[] arr = data.Split(CommonEnum.Separator);
            sellInvoice.Code = arr[0];
            sellInvoice.CustomerName = arr[1];
            sellInvoice.Address = arr[2];
            sellInvoice.InvoiceDate = CommonFunction.StringToDate(arr[3]);
            sellInvoice.TotalPrice = CommonFunction.StringToInt(arr[4]);

            return sellInvoice;
        }

        private static string SellInvoiceToString(SellInvoice sellInvoice, string formatDate = CommonEnum.Date)
        {
            return $"{sellInvoice.Code}{CommonEnum.Separator}" +
                $"{sellInvoice.CustomerName}{CommonEnum.Separator}" +
                $"{sellInvoice.Address}{CommonEnum.Separator}" +
                $"{CommonFunction.DateToString(sellInvoice.InvoiceDate, formatDate)}{CommonEnum.Separator}" +
                $"{GetTotalPrice(sellInvoice.DetailSellProducts)}";
        }

        private static decimal GetTotalPrice(DetailSellProduct[] detailSellProducts)
        {
            decimal totalPrice = 0;
            foreach (DetailSellProduct item in detailSellProducts)
            {
                totalPrice += item.SumPrice;
            }

            return totalPrice;
        }
    }
}
