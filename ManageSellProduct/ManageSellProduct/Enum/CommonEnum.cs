namespace ManageSellProduct.Enum
{
    public struct CommonEnum
    {
        public const string RootFolder = "Data";
        public const string Separator = "|";
        public const string Date = "yyyyMMdd";
        public const string DateDisplay = "yyyy-MM-dd";
        public const string DateTime = "yyyyMMdd HHmmss";
        public const string DateTimeDisplay = "yyyy-MM-dd HH:mm:ss";

        public const string ErrorPath = "Du lieu truyen vao khong hop le";
        public const string Success = "Thao tác thanh cong";
        public const string NotAllowDelete = "Khong the xoa {0} do gang buot data";
        public const string NotAllowAdd = "Khong the them Ma {0} do bi trung";
        public const string NotExist = "{0} khong ton tai";

        //action
        public const string Add = "add";
        public const string Edit = "edit";
        public const string Delete = "delete";
        public const string Detail = "detail";

        //Method
        public const string Get = "GET";
        public const string Post = "POST";

        public const string Key = "Key";
        public const string Title = "Title";

    }
}
