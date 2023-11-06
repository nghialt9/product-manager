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
        ///     Indicates whether a specified string is null, empty, or consists only of white-space characters.
        /// </summary>
        public static bool IsNullOrWhiteSpace(params string[] array)
        {
            return array.Any(string.IsNullOrWhiteSpace);
        }

        /// <summary>
        /// Determines whether is all items of the specified array are null or white space.
        /// </summary>
        public static bool IsAllNullOrWhiteSpace(params string[] array)
        {
            return array.All(string.IsNullOrWhiteSpace);
        }

        private static readonly string[] Digit = { "không ", "một ", "hai ", "ba ", "bốn ", "năm ", "sáu ", "bảy ", "tám ", "chín " };
        private static readonly string[] Money = { "", "nghìn", "triệu", "tỷ", "nghìn", "triệu" };
        private static readonly string[] Ext = { "linh ", "mốt ", "lăm ", "mươi ", "mười ", "trăm " };

        public static string NumberToWordsVn(string num)
        {
            if (!long.TryParse(num, out long number))
            {
                return "Số tiền âm !";
            }

            long[] location = new long[6];
            string result = "";
            if (number < 0)
            {
                return "Số tiền âm !";
            }

            if (number == 0)
            {
                return "Không đồng !";
            }

            if (number > 8999999999999999)
            {
                return "Số quá lớn!";
            }

            location[5] = number / 1000000000000000;
            number -= location[5] * 1000000000000000;

            location[4] = number / 1000000000000;
            number -= location[4] * 1000000000000;

            location[3] = number / 1000000000;
            number -= location[3] * 1000000000;

            location[2] = number / 1000000;
            location[1] = (number % 1000000) / 1000;
            location[0] = number % 1000;


            int times;
            if (location[5] > 0)
            {
                times = 5;
            }
            else if (location[4] > 0)
            {
                times = 4;
            }
            else if (location[3] > 0)
            {
                times = 3;
            }
            else if (location[2] > 0)
            {
                times = 2;
            }
            else if (location[1] > 0)
            {
                times = 1;
            }
            else
            {
                times = 0;
            }

            for (int i = times; i >= 0; i--)
            {
                string tmp = ReadThreeNumber(location[i]);
                if (i == times && tmp.StartsWith(Digit[0]))
                {
                    tmp = tmp.Replace(Digit[0], "").Replace(Ext[0], "").Replace(Ext[5], "");
                }

                result += tmp;

                if (location[i] > 0)
                {
                    result += Money[i];
                }

                if (i > 0 && tmp.Length > 0)
                {
                    result += " ";
                }
            }

            if (times > 3 && result.Trim().Split(' ').Length <= 5)
            {
                result += Money[3];
            }

            if (result.Substring(result.Length - 1) == ",")
            {
                result = result.Substring(0, result.Length - 1);
            }

            result = result.Substring(0, 1).ToUpper() + result.Substring(1);
            return result + "đồng";
        }

        private static string ReadThreeNumber(long threeNumber)
        {
            string result = "";
            long hundred = threeNumber / 100;
            long dozen = (threeNumber % 100) / 10;
            long unit = threeNumber % 10;

            if (hundred == 0 && dozen == 0 && unit == 0)
                return "";

            result += Digit[hundred] + Ext[5];

            if (dozen == 0 && unit != 0)
                result += Ext[0];

            if (dozen != 0 && dozen != 1)
            {
                result += Digit[dozen] + Ext[3];

                if (dozen == 0 && unit != 0)
                    result += Ext[0];
            }

            if (dozen == 1)
                result += Ext[4];

            switch (unit)
            {
                case 1:
                    if (dozen != 0 && dozen != 1)
                    {
                        result += Ext[1];
                    }
                    else
                    {
                        result += Digit[unit];
                    }

                    break;
                case 5:
                    if (dozen == 0)
                    {
                        result += Digit[unit];
                    }
                    else
                    {
                        result += Ext[2];
                    }

                    break;
                default:
                    if (unit != 0)
                    {
                        result += Digit[unit];
                    }

                    break;
            }

            return result;
        }

        /// <summary>
        ///     Indicates whether a specified string is in array.
        /// </summary>
        public static bool IsInArray(string value, params string[] array)
        {
            return array.Any(data => value == data);
        }
    }    
}
