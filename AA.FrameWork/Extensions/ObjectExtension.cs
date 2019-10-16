using AA.FrameWork.ObjectMapping;
using System;

namespace AA.FrameWork.Extensions
{
    public static class ObjectExtension
    {
        /// <summary>
        /// map an object to an other object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TDestination MapTo<TDestination>(this object source)
        {
            return ObjectMapManager.ObjectMapper.Map<TDestination>(source);
        }

        public static TDestination MapTo<TSource, TDestination>(this TSource source)
        {
            return ObjectMapManager.ObjectMapper.Map<TSource, TDestination>(source);
        }

        /// <summary>
        ///     转换为short，默认值：short.MinValue
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        public static short TryShort(this Object strText)
        {
            return TryShort(strText, short.MinValue);
        }

        /// <summary>
        ///     转换为short
        /// </summary>
        /// <param name="strText"></param>
        /// <param name="defValue"></param>
        /// <returns></returns>
        public static short TryShort(this Object strText, short defValue)
        {
            short result = 0;
            return short.TryParse(strText + "", out result) ? result : defValue;
        }

        /// <summary>
        ///     转换为short
        /// </summary>
        /// <param name="strText"></param>
        /// <param name="defValue"></param>
        /// <returns></returns>
        public static short? TryShort(this Object strText, short? defValue)
        {
            short result = 0;
            return short.TryParse(strText + "", out result) ? result : defValue;
        }

        /// <summary>
        ///     转换为Int，默认值：int.MinValue
        /// </summary>
        public static int TryInt(this Object strText)
        {
            return TryInt(strText, int.MinValue);
        }

        /// <summary>
        ///     转换为Int
        /// </summary>
        /// <param name="strText"></param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static int TryInt(this Object strText, int defValue)
        {
            int temp = int.MinValue;
            return int.TryParse(strText + "", out temp) ? temp : defValue;
        }

        /// <summary>
        ///     转换为Int
        /// </summary>
        /// <param name="strText"></param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static int? TryInt(this Object strText, int? defValue)
        {
            int temp = int.MinValue;
            return int.TryParse(strText + "", out temp) ? temp : defValue;
        }

        /// <summary>
        ///     转换为Double，默认值：double.MinValue
        /// </summary>
        public static double TryDouble(this Object strText)
        {
            return TryDouble(strText, double.MinValue);
        }

        /// <summary>
        ///     转换为Double
        /// </summary>
        /// <param name="strText"></param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static double TryDouble(this Object strText, double defValue)
        {
            double temp = double.MinValue;
            return double.TryParse(strText + "", out temp) ? temp : defValue;
        }

        /// <summary>
        ///     转换为Double
        /// </summary>
        /// <param name="strText"></param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static double? TryDouble(this Object strText, double? defValue)
        {
            double temp = double.MinValue;
            return double.TryParse(strText + "", out temp) ? temp : defValue;
        }

        /// <summary>
        ///     转换为Decimal，默认值：decimal.MinValue
        /// </summary>
        public static decimal TryDecimal(this Object strText)
        {
            return TryDecimal(strText, decimal.MinValue);
        }

        /// <summary>
        ///     转换为Decimal
        /// </summary>
        /// <param name="strText"></param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static decimal TryDecimal(this Object strText, decimal defValue)
        {
            decimal temp = decimal.MinValue;
            return decimal.TryParse(strText + "", out temp) ? temp : defValue;
        }

        /// <summary>
        ///     转换为Decimal
        /// </summary>
        /// <param name="strText"></param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static decimal? TryDecimal(this Object strText, decimal? defValue)
        {
            decimal temp = decimal.MinValue;
            return decimal.TryParse(strText + "", out temp) ? temp : defValue;
        }

        /// <summary>
        ///     转换为long，默认值：long.MinValue
        /// </summary>
        public static long TryLong(this Object strText)
        {
            return TryLong(strText, long.MinValue);
        }

        /// <summary>
        ///     转换为long
        /// </summary>
        /// <param name="strText"></param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static long TryLong(this Object strText, long defValue)
        {
            long temp = long.MinValue;
            return long.TryParse(strText + "", out temp) ? temp : defValue;
        }

        /// <summary>
        ///     转换为long
        /// </summary>
        /// <param name="strText"></param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static long? TryLong(this Object strText, long? defValue)
        {
            long temp = long.MinValue;
            return long.TryParse(strText + "", out temp) ? temp : defValue;
        }

        /// <summary>
        ///     转换为Boolean，默认值：false
        /// </summary>
        public static Boolean TryBool(this Object strText)
        {
            return TryBool(strText, false);
        }

        /// <summary>
        ///     转换为Boolean
        /// </summary>
        /// <param name="strText"></param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static Boolean TryBool(this Object strText, bool defValue)
        {
            if (strText.TryInt(0) == 1)
                return true;
            var temp = false;
            return bool.TryParse(strText + "", out temp) ? temp : defValue;
        }

        /// <summary>
        ///     转换为Boolean
        /// </summary>
        /// <param name="strText"></param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static Boolean? TryBool(this Object strText, bool? defValue)
        {
            var i = strText.TryInt(null);
            if (i.HasValue)
                return i == 1;
            bool temp = false;
            return bool.TryParse(strText + "", out temp) ? temp : defValue;
        }

        /// <summary>
        ///     转换为Boolean,等于返回null,1为true,0为false
        /// </summary>
        /// <param name="strInt"></param>
        /// <returns></returns>
        //public static Boolean? TryBool(this Object strInt)
        //{
        //    switch (strInt.TryInt())
        //    {
        //        case -1:
        //            return null;
        //        case 1:
        //            return true;
        //        case 0:
        //            return false;
        //        default:
        //            return false;
        //    }
        //}

        /// <summary>
        ///     转换为DateTime，默认值：DateTimeExtension.DBNull
        /// </summary>
        public static DateTime TryDateTime(this Object strText)
        {
            return TryDateTime(strText, DateTimeExtension.DBNull);
        }

        /// <summary>
        ///     转换为DateTime
        /// </summary>
        /// <param name="strText"></param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static DateTime TryDateTime(this Object strText, DateTime defValue)
        {
            DateTime temp = DateTimeExtension.DBNull;
            return DateTime.TryParse(strText + "", out temp) ? temp : defValue;
        }

        /// <summary>
        ///     转换为DateTime
        /// </summary>
        /// <param name="strText"></param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static DateTime? TryDateTime(this Object strText, DateTime? defValue)
        {
            DateTime temp = DateTimeExtension.DBNull;
            return DateTime.TryParse(strText + "", out temp) ? temp : defValue;
        }

        /// <summary>
        ///     为NULL 和 DBNull的返回String.Empty
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string TryString(this Object str)
        {
            return TryString(str, "");
        }

        /// <summary>
        ///     转换为""
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string TryString(this Object str, string defvalue)
        {
            return str == null ? defvalue : str.ToString();
        }


        /// <summary>
        ///     转换十六进制
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static string ConvertX8(this int i)
        {
            return Convert.ToString(i, 16);
        }
    }
}