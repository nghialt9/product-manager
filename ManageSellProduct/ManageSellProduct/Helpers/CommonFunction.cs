using ManageSellProduct.Enum;
using System.Globalization;

namespace ManageSellProduct.Helpers
{
    public class CommonFunction
    {
        public static string SaveData(string[] array, string filePath)
        {
            if (array == null || string.IsNullOrWhiteSpace(filePath))
            {
                return CommonEnum.ErrorPath;
            }

            string? folder = Path.GetDirectoryName(filePath);

            if (string.IsNullOrWhiteSpace(folder) == false &&
                Directory.Exists(folder) == false)
            {
                Directory.CreateDirectory(folder);
            }

            StreamWriter writer = new StreamWriter(filePath);

            foreach (string item in array)
            {
                writer.WriteLine(item.ToString());
            }

            writer.Close();

            return CommonEnum.Success;
        }

        public static string[] GetData(string filePath)
        {
            string[] data = new string[0];
            string? folder = Path.GetDirectoryName(filePath);

            if (string.IsNullOrWhiteSpace(filePath) ||
                string.IsNullOrWhiteSpace(folder) ||
                Directory.Exists(folder) == false)
            {
                return new string[0];
            }

            StreamReader reader = new StreamReader(filePath);
            int n = File.ReadAllLines(filePath).Length;

            if (n > 0)
            {
                data = new string[n];

                for (int i = 0; i < data.Length; i++)
                {
                    string? s = reader.ReadLine();

                    if (string.IsNullOrWhiteSpace(s)) {
                        Array.Resize(ref data, data.Length - 1);
                        continue;
                    }

                    data[i] = s.Trim();
                }

                reader.Close();
            }

            reader.Close();

            return data;
        }

        public static string DateToString(DateTime dateTime, string format = CommonEnum.Date)
        {
            return dateTime.ToString(format);
        }

        public static DateTime StringToDate(string strDateTime, string format = CommonEnum.Date)
        {
            DateTime.TryParseExact(strDateTime, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateTime);

            return dateTime;
        }

        public static decimal StringToDecimal(string value, decimal defaultValues = 0)
        {
            if (decimal.TryParse(value, out decimal result) == false)
            {
                result = defaultValues;
            }

            return result;
        }

        public static int StringToInt(string value, int defaultValues = 0)
        {
            if (int.TryParse(value, out int result) == false)
            {
                result = defaultValues;
            }

            return result;
        }

        public static T[] ArrayAddItem<T>(T[] oldArray, T newValue)
        {
            T[] newArray = new T[oldArray.Length + 1];
            for (int i = 0; i < oldArray.Length; i++)
            {
                newArray[i] = oldArray[i];
            }
            newArray[newArray.Length - 1] = newValue;
            return newArray;
        }

        public static string FormatAsVietnameseCurrency(decimal amount)
        {
            return string.Format("{0:N0} đ", amount);
        }

        public static string FormatNumber(int number)
        {
            return string.Format("{0:N0}", number);
        }

        public static bool IsExistCode(string code, string filePath)
        {
            string[] data = GetData(filePath);
            bool result = false;

            if (data.Length > 0)
            {
                for (int i = 0; i < data.Length; i++)
                {
                    if (data[i].StartsWith(code))
                    {
                        result = true;
                        break;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Determines whether is all items of the specified array are null or white space.
        /// </summary>
        public static bool IsAllNullOrWhiteSpace(params string[] array)
        {
            return array.All(string.IsNullOrWhiteSpace);
        }
    }
}
