using ManageSellProduct.Enum;
using ManageSellProduct.Helpers;
using ManageSellProduct.Models;

namespace ManageSellProduct.Providers
{
    public class SellInvoiceProvider
    {
        private static string FilePath = @$"{CommonEnum.RootFolder}\SellInvoice\SellInvoice.txt";

        public static List<SellInvoiceModel> GetSellInvoices()
        {
            List<SellInvoiceModel> sellInvoices = new List<SellInvoiceModel>();
            List<string> data = CommonFunction.GetData(FilePath);

            if (data.Count > 0)
            {
                foreach (string item in data)
                {
                    sellInvoices.Add(StringToSellInvoice(item));
                }
            }

            return sellInvoices;
        }

       public static SellInvoiceModel GetSellInvoicesByCode(string code)
        {
            List<SellInvoiceModel> sellInvoices = GetSellInvoices();

            SellInvoiceModel? sellInvoice = sellInvoices.FirstOrDefault(s => s.SellInvoiceCode == code);

            return sellInvoice ?? new SellInvoiceModel();
        }

        public static SellInvoiceModel GetDetailSellInvoice(string code)
        {
            SellInvoiceModel sellInvoice = GetSellInvoicesByCode(code);
            sellInvoice.DetailSellProducts = DetailSellProductProvider.GetDetailSellProducts(code);

            return sellInvoice;
        }

        public static string AddSellInvoice(SellInvoiceModel sellInvoice)
        {
            List<string> data = CommonFunction.GetData(FilePath);
            string str = SellInvoiceToString(sellInvoice);
            data.Add(str);
            CommonFunction.SaveData(data, FilePath);

            DetailSellProductProvider.AddDetailSellProducts(sellInvoice.DetailSellProducts);

            return CommonEnum.Success;
        }

        public static bool IsExistSellInvoiceCode(string code)
        {
            return CommonFunction.IsExistCode(code, FilePath);
        }

        public static string EditSellInvoice(SellInvoiceModel sellInvoice)
        {
            List<string> data = CommonFunction.GetData(FilePath);

            if (data.Count == 0)
            {
                return CommonEnum.ErrorPath;
            }
            else
            {
                data.ForEach(d =>
                {
                    if (sellInvoice.SellInvoiceCode == StringToSellInvoice(d).SellInvoiceCode)
                    {
                        d = SellInvoiceToString(sellInvoice);
                    }
                });

                CommonFunction.SaveData(data, FilePath);
            }

            DetailSellProductProvider.EditDetailSellProducts(sellInvoice.DetailSellProducts);

            return CommonEnum.Success;
        }

        public static string DeleteSellInvoice(string code)
        {
            List<string> data = CommonFunction.GetData(FilePath);

            if (data.Count == 0)
            {
                return CommonEnum.ErrorPath;
            }
            else
            {
                data = data.Where(d => StringToSellInvoice(d).SellInvoiceCode != code).ToList();

                CommonFunction.SaveData(data, FilePath);
            }

            DetailSellProductProvider.DeleteDetailSellProducts(code);

            return CommonEnum.Success;
        }

        private static SellInvoiceModel StringToSellInvoice(string data)
        {
            SellInvoiceModel sellInvoice = new SellInvoiceModel();
            string[] arr = data.Split(CommonEnum.Separator);
            sellInvoice.SellInvoiceCode = arr[0];
            sellInvoice.CustomerName = arr[1];
            sellInvoice.Address = arr[2];
            sellInvoice.InvoiceDate = CommonFunction.StringToDate(arr[3]);
            sellInvoice.TotalPrice = CommonFunction.StringToInt(arr[4]);

            return sellInvoice;
        }

        private static string SellInvoiceToString(SellInvoiceModel sellInvoice, string formatDate = CommonEnum.Date)
        {
            return $"{sellInvoice.SellInvoiceCode}{CommonEnum.Separator}" +
                $"{sellInvoice.CustomerName}{CommonEnum.Separator}" +
                $"{sellInvoice.Address}{CommonEnum.Separator}" +
                $"{CommonFunction.DateToString(sellInvoice.InvoiceDate, formatDate)}{CommonEnum.Separator}" +
                $"{GetTotalPrice(sellInvoice.DetailSellProducts)}";
        }

        private static decimal GetTotalPrice(List<DetailSellProductModel> detailSellProducts)
        {
            decimal totalPrice = 0;
            foreach (DetailSellProductModel item in detailSellProducts)
            {
                totalPrice += item.SumPrice;
            }

            return totalPrice;
        }
    }
}
