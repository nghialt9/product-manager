using ManageSellProduct.Enum;
using ManageSellProduct.Models;
using ManageSellProduct.Providers;

namespace ManageSellProduct.Business
{
    public class ImportInvoiceBusiness
    {
        public static ImportInvoiceModel[] GetImportInvoices()
        {
            ImportInvoiceModel[] importInvoices = ImportInvoiceProvider.GetImportInvoices();

            return importInvoices;
        }

        public static ImportInvoiceModel GetImportInvoice(string code)
        {
            return ImportInvoiceProvider.GetDetailImportInvoice(code);
        }

        public static string AddImportInvoice(ImportInvoiceModel importInvoice)
        {
            if(ImportInvoiceProvider.IsExistImportInvoiceCode(importInvoice.ImportInvoiceCode)) {
                return CommonEnum.NotAllowAdd;
            }

            return ImportInvoiceProvider.AddImportInvoice(importInvoice);
        }

        public static string EditImportInvoice(ImportInvoiceModel importInvoice)
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
