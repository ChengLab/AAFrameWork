using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AA.FrameWork.Extensions
{
    public static class GuidExtension
    {
        #region verify Guid is empty or not

        /// <summary>
        /// verify Guid is empty or not
        /// </summary>
        /// <param name="value">Guid value</param>
        /// <returns>is empty or not</returns>
        public static bool IsEmpty(this Guid value)
        {
            return value == Guid.Empty;
        }

        #endregion

        #region convert Guid to Int64

        /// <summary>
        /// convert Guid to Int64
        /// </summary>
        /// <param name="value">Guid value</param>
        /// <returns>Int64 value</returns>
        public static long ToInt64(this Guid value)
        {
            byte[] bytes = value.ToByteArray();
            return BitConverter.ToInt64(bytes, 0);
        }

        #endregion

        #region convert Guid to Int32

        /// <summary>
        /// convert Guid to Int32
        /// </summary>
        /// <param name="value">Guid value</param>
        /// <returns>Int32 value</returns>
        public static int ToInt32(this Guid value)
        {
            byte[] bytes = value.ToByteArray();
            return BitConverter.ToInt32(bytes, 0);
        }

        #endregion

        #region convert guid to uniquecode(formatting to upper)

        /// <summary>
        /// convert guid to uniquecode(Upper)
        /// </summary>
        /// <param name="value">Guid value</param>
        /// <returns>uniquecode value</returns>
        public static string ToUniqueCode(this Guid value)
        {
            long i = 1;
            byte[] bytes = value.ToByteArray();
            foreach (byte b in bytes)
            {
                i *= ((int)b + 1);
            }
            string code = string.Format("{0:x}", i - DateTime.Now.Ticks);
            return code;
        }

        #endregion
    }
}
