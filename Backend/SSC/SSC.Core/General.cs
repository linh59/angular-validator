using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSC.Core
{
    public static class General
    {
        public static bool IsDevelopment { get; }
#if DEBUG
            = true;
#else
            = false;
#endif
        public const string PermissionsPrefix = "Permissions";

        public const string DefaultValidationFailedMessage = "Dữ liệu không đúng với cấu trúc đã định trước.";
        public const string DefaultNotFoundMessage = "Không tìm thấy dữ liệu được yêu cầu. Vui lòng thử lại sau. Nếu vấn đề này tiếp tục tiếp diễn, vui lòng liên hệ admin.";
        public const string DefaultUnhandledExceptionMessage = "Có lỗi không xác định xảy ra, vui lòng thử lại sau. Nếu vấn đề này tiếp tục tiếp diễn, vui lòng liên hệ admin.";
        public const string EmailDefaultErrorMessage = "Email không đúng định dạng";
        public const string PhoneDefaultErrorMessage = "Số điện thoại không đúng định dạng";
        public const string LinkDefaultErrorMessage = "Đường dẫn không đúng định dạng";
        public const string DayOfWeekDefaultErrorMessage = "Ngày đã chọn không phù hợp, vui lòng chọn ngày khác";
        public const string NotOwnDocumentDefaultErrorMessage = "Không có quyền truy cập tài liệu này";
    }
}
