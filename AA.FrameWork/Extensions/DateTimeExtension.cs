using System;

namespace AA.FrameWork.Extensions
{
    public static class DateTimeExtension
    {
        public static DateTime DBNull = "1900-01-01 00:00:00".TryDateTime();

        /// <summary>
        ///     判断DateTime在数据库中是否存储为象征意义的null
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static bool IsDBNull(this DateTime dateTime)
        {
            return dateTime == DBNull;
        }

        /// <summary>
        ///     判断DateTime在数据库中是否存储为象征意义的null
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static bool IsDBNull(this DateTime? dateTime)
        {
            return dateTime == null || dateTime == DBNull;
        }

        /// <summary>
        ///     格式化为字符串
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string Format(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }


        public static string ToFriendTime(this DateTime dt)
        {
            TimeSpan span = DateTime.Now - dt;

            if (span.TotalSeconds < 60)
            {
                return Math.Round((span.TotalSeconds < 5 ? 5 : span.TotalSeconds), 0) + "秒钟前";
            }
            if (span.TotalMinutes < 60)
            {
                return Math.Round(span.TotalMinutes, 0) + "分钟前";
            }
            if (span.TotalHours < 5)
            {
                if (span.Minutes > 0)
                {
                    return span.Hours + "小时" + span.Minutes + "分钟前";
                }
                else
                {
                    return span.Hours + "小时前";
                }
            }
            if (span.TotalDays < 1 && DateTime.Now.Day == dt.Day)
            {
                return "今天 " + dt.ToString("HH:mm") + "";
            }
            if (span.TotalDays < 1 && DateTime.Now.Day > dt.Day)
            {
                return "昨天 " + dt.ToString("HH:mm") + "";
            }
            if (span.TotalDays < 2 && span.TotalDays >= 1 && (DateTime.Now.Day - dt.Day) == 1)
            {
                return "昨天 " + dt.ToString("HH:mm") + "";
            }
            if (span.TotalDays < 3 && (DateTime.Now.Day - dt.Day) == 2)
            {
                return "前天 " + dt.ToString("HH:mm") + "";
            }
            //if (span.TotalDays <= 30 && dt.Year == DateTime.Now.Year)
            //{
            //    return dt.ToString("MM月dd日");
            //}

            return dt.ToString("yyyy-MM-dd HH:mm") + "";
        }
    }
}