using ManageSellProduct.Enum;
using ManageSellProduct.Models;
using ManageSellProduct.Providers;

namespace ManageSellProduct.Business
{
    public class SellInvoiceBusiness
    {
        public static SellInvoiceModel[] GetSellInvoices()
        {
            SellInvoiceModel[] sellInvoices = SellInvoiceProvider.GetSellInvoices();

            return sellInvoices;
        }

        public static SellInvoiceModel GetSellInvoice(string code)
        {
            return SellInvoiceProvider.GetDetailSellInvoice(code);
        }

        public static string AddSellInvoice(SellInvoiceModel sellInvoice)
        {
            if(SellInvoiceProvider.IsExistSellInvoiceCode(sellInvoice.SellInvoiceCode)) {
                return CommonEnum.NotAllowAdd;
            }

            return SellInvoiceProvider.AddSellInvoice(sellInvoice);
        }

        public static string EditSellInvoice(SellInvoiceModel sellInvoice)
        {
            string result = SellInvoiceProvider.EditSellInvoice(sellInvoice);

            return result;
        }

        public static string DeleteSellInvoice(string code)
        {
            return SellInvoiceProvider.DeleteSellInvoice(code);
        }

        public static string DeleteDetailSellInvoice(string sellcode, string productCode)
        {
            return DetailSellProductProvider.DeleteDetailSellProduct(sellcode, productCode);
        }
    }
}
