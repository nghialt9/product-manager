using ManageSellProduct.Enum;
using ManageSellProduct.Helpers;
using ManageSellProduct.Models;
using ManageSellProduct.Providers;

namespace ManageSellProduct.Business
{
    public class SellInvoiceBusiness
    {
        public static SellInvoice[] GetSellInvoices()
        {
            SellInvoice[] sellInvoices = SellInvoiceProvider.GetSellInvoices();

            return sellInvoices;
        }

        public static SellInvoice GetSellInvoice(string code)
        {
            return SellInvoiceProvider.GetDetailSellInvoice(code);
        }

        public static string AddSellInvoice(SellInvoice sellInvoice)
        {
            if(SellInvoiceProvider.IsExistSellInvoiceCode(sellInvoice.Code)) {
                return CommonEnum.NotAllowAdd;
            }

            return SellInvoiceProvider.AddSellInvoice(sellInvoice);
        }

        public static string EditSellInvoice(SellInvoice sellInvoice)
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
