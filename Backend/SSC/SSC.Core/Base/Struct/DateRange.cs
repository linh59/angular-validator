using System;
using System.Collections.Generic;
using System.Text;

namespace SSC.Core.Base.Struct
{
    public struct DateRange
    {
        public DateTime Begin { get; private set; }
        public DateTime End { get; private set; }
        public DateRange(TimeSpan timeFromNow)
        {
            Begin = DateTime.Now;
            End = DateTime.Now.Add(timeFromNow);
        }
        public DateRange(DateTime begin, DateTime end)
        {
            if (begin > end)
            {
                throw new ArgumentException("Ngày bắt đầu không thể lớn hơn ngày kết thúc");
            }
            Begin = begin;
            End = end;
        }

        public bool IsInDateRange(DateTime source) => source.IsInDateRange(Begin, End);
    }
}
