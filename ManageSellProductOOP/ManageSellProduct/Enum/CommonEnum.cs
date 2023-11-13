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

        public const string ErrorPath = "Dữ liệu truyền vào không hợp lệ";
        public const string Success = "Thao tác thành công";
        public const string NotAllowDelete = "Không thể xóa {0} do gàng buộc dữ liệu";
        public const string NotAllowAdd = "Không thể thêm Mã {0} do bị trùng";
        public const string NotExist = "{0} không tồn tại";
        public const string ConfirmDelete = "Bạn có muốn xóa {0} không?";

        //action
        public const string Add = "add";
        public const string Save = "save";
        public const string Edit = "edit";
        public const string Delete = "delete";
        public const string Detail = "detail";
        public const string Inventory = "inventory";
        public const string Expired = "expired";

        //Method
        public const string Get = "GET";
        public const string Post = "POST";

        public const string Key = "Key";
        public const string Title = "Title";

    }
}
