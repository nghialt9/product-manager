using ManageSellProduct.Enum;
using ManageSellProduct.Helpers;
using ManageSellProduct.Models;

namespace ManageSellProduct.Providers
{
    public class ImportInvoiceProvider
    {
        private static string FilePath = @$"{CommonEnum.RootFolder}\ImportInvoice\ImportInvoice.txt";

        public static ImportInvoiceModel[] GetImportInvoices()
        {
            ImportInvoiceModel[] importInvoices;
            string[] data = CommonFunction.GetData(FilePath);

            if (data.Length > 0)
            {
                importInvoices = new ImportInvoiceModel[data.Length];
                for (int i = 0; i < data.Length; i++)
                {
                    importInvoices[i] = StringToImportInvoice(data[i]);
                }

                return importInvoices;
            }

            return new ImportInvoiceModel[] { };
        }

        public static ImportInvoiceModel GetImportInvoicesByCode(string code)
        {
            ImportInvoiceModel[] importInvoices = GetImportInvoices();

            ImportInvoiceModel importInvoice = new ImportInvoiceModel();
            if (importInvoices.Length > 0)
            {
                for (int i = 0; i < importInvoices.Length; i++)
                {
                    if (code == importInvoices[i].ImportInvoiceCode)
                    {
                        importInvoice = importInvoices[i];
                        break;
                    }
                }
            }

            return importInvoice;
        }

        public static ImportInvoiceModel GetDetailImportInvoice(string code)
        {
            ImportInvoiceModel importInvoice = GetImportInvoicesByCode(code);
            importInvoice.DetailImportProducts = DetailImportProductProvider.GetDetailImportProducts(code);

            return importInvoice;
        }

        public static string AddImportInvoice(ImportInvoiceModel importInvoice)
        {
            string[] data = CommonFunction.GetData(FilePath);
            string str = ImportInvoiceToString(importInvoice);

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

            DetailImportProductProvider.AddDetailImportProducts(importInvoice.DetailImportProducts);

            return CommonEnum.Success;
        }

        public static bool IsExistImportInvoiceCode(string code)
        {
            return CommonFunction.IsExistCode(code, FilePath);
        }

        public static string EditImportInvoice(ImportInvoiceModel importInvoice)
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
                    if(importInvoice.ImportInvoiceCode == StringToImportInvoice(data[i]).ImportInvoiceCode)
                    {
                        data[i] = ImportInvoiceToString(importInvoice);
                    }
                }

                CommonFunction.SaveData(data, FilePath);
            }

            DetailImportProductProvider.EditDetailImportProducts(importInvoice.DetailImportProducts);

            return CommonEnum.Success;
        }

        public static string DeleteImportInvoice(string code)
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
                    if (code != StringToImportInvoice(data[i]).ImportInvoiceCode)
                    {
                        newData[j++] = data[i];
                    }    
                }

                CommonFunction.SaveData(newData, FilePath);
            }

            DetailImportProductProvider.DeleteDetailImportProducts(code);

            return CommonEnum.Success;
        }

        private static ImportInvoiceModel StringToImportInvoice(string data)
        {
            ImportInvoiceModel importInvoice = new ImportInvoiceModel();    
            string[] arr = data.Split(CommonEnum.Separator);
            importInvoice.ImportInvoiceCode = arr[0];
            importInvoice.InvoiceDate = CommonFunction.StringToDate(arr[1]);
            importInvoice.Supplier = arr[2];
            importInvoice.Address = arr[3];
            importInvoice.TotalPrice = CommonFunction.StringToInt(arr[4]);

            return importInvoice;
        }

        private static string ImportInvoiceToString(ImportInvoiceModel importInvoice, string formatDate = CommonEnum.Date)
        {
            return $"{importInvoice.ImportInvoiceCode}{CommonEnum.Separator}" +
                $"{CommonFunction.DateToString(importInvoice.InvoiceDate, formatDate)}{CommonEnum.Separator}" +
                $"{importInvoice.Supplier}{CommonEnum.Separator}" +
                $"{importInvoice.Address}{CommonEnum.Separator}" +
                $"{GetTotalPrice(importInvoice.DetailImportProducts)}";
        }

        private static decimal GetTotalPrice(DetailImportProduct[] detailImportProducts)
        {
            decimal totalPrice = 0;
            foreach (DetailImportProduct item in detailImportProducts)
            {
                totalPrice += item.SumPrice;
            }

            return totalPrice;
        }
    }
}
