using SSC.Core.Bussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace SSC.Core
{
    public static partial class Extension
    {
        /// <summary>
        /// Phân trang
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities">List thực thể</param>
        /// <param name="perpage">Số object mỗi trang</param>
        /// <param name="handle">Nếu true, thì khi input trang <= 0, sẽ tự động xử lí. Nếu false sẽ ném <see cref="IndexOutOfRangeException"/></param>
        /// <returns>Vật thể đã được phân trang <see cref="Pagination{T}(IEnumerable{T}, int, bool)"/></returns>
        public static Pagination<T> Pagination<T>(this IEnumerable<T> entities, int perpage, bool handle = true) => new Pagination<T>(entities, perpage, handle);

        public static string StringJoin<T>(this IEnumerable<T> joinCollection, string separator) => string.Join(separator, joinCollection);
        public static string StringJoin<T>(this IEnumerable<T> joinCollection, char separator) => string.Join(separator, joinCollection);

        public static Dictionary<TKey, int> MergeDictionary<TKey>(this Dictionary<TKey, int> source, params Dictionary<TKey, int>[] mergeDic)
        {
            return source.MergeDictionary(mergeDics: mergeDic);
        }
        public static Dictionary<TKey, int> MergeDictionary<TKey>(this Dictionary<TKey, int> source, IEnumerable<Dictionary<TKey, int>> mergeDics)
        {
            foreach (var dic in mergeDics)
            {
                foreach (var item in dic)
                {
                    if (source.ContainsKey(item.Key))
                    {
                        source[item.Key] += item.Value;
                    }
                    else
                    {
                        source.Add(item.Key, item.Value);
                    }
                }
            }
            return source;
        }


        /// <summary>
        /// Kiểm tra xem ngày tháng có nằm trong range cho phép không
        /// </summary>
        /// <param name="source">Ngày tháng cần check</param>
        /// <param name="begin">Ngày bắt đầu của range</param>
        /// <param name="end">Ngày kết thúc của range</param>
        /// <returns></returns>
        public static bool IsInDateRange(this DateTime source, DateTime begin, DateTime end)
        {
            if (begin > end)
            {
                throw new ArgumentException("Ngày bắt đầu không thể lớn hơn ngày kết thúc");
            }
            return source >= begin && source <= end;
        }

        /// <summary>
        /// Trả về ngày cần check có nằm trong cùng 1 tuần không
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <param name="weekStartsOn"></param>
        /// <returns><see cref="bool"/>. <see cref="true"/> nếu cùng 1 tuần, nếu không, <see cref="false"/></returns>
        public static bool IsSameWeekWith(this DateTime date1, DateTime date2, DayOfWeek weekStartsOn = DayOfWeek.Monday)
        {
            return date1.AddDays(-GetOffsetedDayofWeek(date1.DayOfWeek, (int)weekStartsOn)).Date == date2.AddDays(-GetOffsetedDayofWeek(date2.DayOfWeek, (int)weekStartsOn)).Date;
        }

        /// <summary>
        /// Trả về ngày cần check có nằm trong cùng 1 tuần với hôm nay không
        /// </summary>
        /// <param name="check">Ngày cần kiểm tra</param>
        /// <param name="weekStartsOn"></param>
        /// <returns><see cref="bool"/>. <see cref="true"/> nếu cùng 1 tuần, nếu không, <see cref="false"/></returns>
        public static bool IsSameWeekWithToday(this DateTime check, DayOfWeek weekStartsOn = DayOfWeek.Monday)
        {
            return check.IsSameWeekWith(DateTime.Now, weekStartsOn);
        }


        /// <summary>
        /// Trả về ngày cần check có nằm trong cùng 1 tháng không
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <param name="weekStartsOn"></param>
        /// <returns><see cref="bool"/>. <see cref="true"/> nếu cùng 1 tuần, nếu không, <see cref="false"/></returns>
        public static bool IsSameMonthWith(this DateTime date1, DateTime date2)
        {
            return date1.Month == date2.Month && date1.Year == date2.Year;
        }

        /// <summary>
        /// Trả về ngày cần check có nằm trong cùng 1 tháng với hôm nay không
        /// </summary>
        /// <param name="check">Ngày cần kiểm tra</param>
        /// <param name="weekStartsOn"></param>
        /// <returns><see cref="bool"/>. <see cref="true"/> nếu cùng 1 tuần, nếu không, <see cref="false"/></returns>
        public static bool IsSameMonthWithToday(this DateTime check)
        {
            return check.IsSameMonthWith(DateTime.Now);
        }

        /// <summary>
        /// Trả về ngày cần check có nằm trong cùng 1 năm không
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <param name="weekStartsOn"></param>
        /// <returns><see cref="bool"/>. <see cref="true"/> nếu cùng 1 tuần, nếu không, <see cref="false"/></returns>
        public static bool IsSameYearWith(this DateTime date1, DateTime date2)
        {
            return date1.Year == date2.Year;
        }

        /// <summary>
        /// Trả về ngày cần check có nằm trong cùng 1 năm với hôm nay không
        /// </summary>
        /// <param name="check">Ngày cần kiểm tra</param>
        /// <param name="weekStartsOn"></param>
        /// <returns><see cref="bool"/>. <see cref="true"/> nếu cùng 1 tuần, nếu không, <see cref="false"/></returns>
        public static bool IsSameYearWithToday(this DateTime check)
        {
            return check.IsSameYearWith(DateTime.Now);
        }

        private static int GetOffsetedDayofWeek(DayOfWeek dayOfWeek, int offsetBy)
        {
            return ((int)dayOfWeek - offsetBy + 7) % 7;
        }

        public static bool HasFlag(this int source, int check)
        {
            if ((check == 0) || ((check & (check - 1)) != 0))
            {
                throw new ArgumentException("số cần kiểm tra buộc phải là luỹ thừa của 2");
            }
            return (source | check) == source;
        }

        /// <summary>
        /// Tạo một chuỗi (<see cref="string"/>) ngẫu nhiên có độ dài cung cấp
        /// </summary>
        /// <param name="rand">Random Object</param>
        /// <param name="length">Độ dài chuỗi</param>
        /// <returns><see cref="string"/></returns>
        public static string GenerateRandomString(this Random rand, int length) => new string(Enumerable.Range(0, length).Select(x => (char)rand.Next(32, 127)).ToArray());


        /// <summary>
        /// This returns true if the current user own the document
        /// </summary>
        /// <param name="user"></param>
        /// <param name="permission"></param>
        /// <returns></returns>
        public static bool UserOwnThisDocument(this ClaimsPrincipal user, string owner)
        {
            if (user?.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value == owner) return true;

            return false;
        }

        public static string B64Encode(this string content)
        {
            if (string.IsNullOrEmpty(content.Trim()))
            {
                return string.Empty;
            }
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(content));
        }
        
        public static string B64Decode(this string b64)
        {
            if (string.IsNullOrEmpty(b64.Trim()))
            {
                return string.Empty;
            }
            return Encoding.UTF8.GetString(Convert.FromBase64String(b64));
        }
    }
}
