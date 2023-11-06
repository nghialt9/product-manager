using ManageSellProduct.Enum;
using ManageSellProduct.Models;
using ManageSellProduct.Providers;

namespace ManageSellProduct.Business
{
    public class ImportInvoiceBusiness
    {
        public static ImportInvoice[] GetImportInvoices()
        {
            ImportInvoice[] importInvoices = ImportInvoiceProvider.GetImportInvoices();

            return importInvoices;
        }

        public static ImportInvoice GetImportInvoice(string code)
        {
            return ImportInvoiceProvider.GetDetailImportInvoice(code);
        }

        public static string AddImportInvoice(ImportInvoice importInvoice)
        {
            if(ImportInvoiceProvider.IsExistImportInvoiceCode(importInvoice.Code)) {
                return CommonEnum.NotAllowAdd;
            }

            return ImportInvoiceProvider.AddImportInvoice(importInvoice);
        }

        public static string EditImportInvoice(ImportInvoice importInvoice)
        {
            string result = ImportInvoiceProvider.EditImportInvoice(importInvoice);

            return result;
        }

        public static string DeleteImportInvoice(string code)
        {
            return ImportInvoiceProvider.DeleteImportInvoice(code);
        }

        public static string DeleteDetailImportInvoice(string sellcode, string productCode)
        {
            return DetailSellProductProvider.DeleteDetailSellProduct(sellcode, productCode);
        }
    }
}
