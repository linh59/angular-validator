using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSC.Core.Bussiness
{
    public class Pagination<T>
    {
        private readonly IEnumerable<T> _objectCollection;
        private readonly int _countPerpage;
        private readonly bool _handle;

        public int TotalCount { get; }
        public int MaxPage { get; }

        /// <summary>
        /// Khởi tạo class phân trang
        /// </summary>
        /// <param name="entities">List thực thể</param>
        /// <param name="perpage">Số object mỗi trang</param>
        /// <param name="handle">Nếu true, thì khi input trang <= 0, sẽ tự động xử lí. Nếu false sẽ ném <see cref="IndexOutOfRangeException"/></param>
        public Pagination(IEnumerable<T> entities, int perpage, bool handle = true)
        {
            _objectCollection = entities;
            _countPerpage = perpage;
            MaxPage = ((entities.Count() - 1) / perpage) + 1;
            _handle = handle;
            TotalCount = entities.Count();
        }

        /// <summary>
        /// Trả ra các object của trang <see cref="page"/>
        /// </summary>
        /// <param name="page">Số trang</param>
        /// <returns></returns>
        public Paged<T> this[int page]
        {
            get
            {
                if (!_handle)
                {
                    if (page > MaxPage)
                    {
                        throw new IndexOutOfRangeException("Số trang nhập vào lớn hơn số trang cho phép");
                    }
                    else if (page <= 0)
                    {
                        throw new IndexOutOfRangeException("Số trang nhập vào không được nhỏ hơn hoặc bằng 0");
                    }
                }
                page = Math.Max(1, Math.Min(page, MaxPage));
                return new Paged<T>(TotalCount, MaxPage, _objectCollection.Skip((page - 1) * _countPerpage).Take(_countPerpage));
            }
        }
    }

    public class Paged<T>
    {
        public Paged(int totalCount, int maxPage, IEnumerable<T> pagedObject)
        {
            TotalCount = totalCount;
            MaxPage = maxPage;
            PagedObject = pagedObject;
        }

        public int TotalCount { get; }
        public int MaxPage { get; }
        public int PerPageCount { get => PagedObject.Count(); }
        public IEnumerable<T> PagedObject { get; }
    }
}
