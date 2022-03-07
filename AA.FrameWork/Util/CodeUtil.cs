using AA.FrameWork.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AA.FrameWork.Util
{
   public static class CodeUtil
    {
        #region generate a guid code

        /// <summary>
        /// generate a guid code
        /// </summary>
        /// <returns>guid code</returns>
        public static Guid GetGuidCode()
        {
            return Guid.NewGuid();
        }

        #endregion

        #region generate a int64 number by guid

        /// <summary>
        /// generate a int64 number by guid
        /// </summary>
        /// <returns></returns>
        public static long GetInt64UniqueCode()
        {
            Guid value = Guid.NewGuid();
            return value.ToInt64();
        }

        /// <summary>
        /// generate a int32 number by guid
        /// </summary>
        /// <returns></returns>
        public static int GetInt32UniqueCode()
        {
            Guid value = Guid.NewGuid();
            int numOne = value.ToInt32();
            Guid valueTwo = Guid.NewGuid();
            int numTwo = valueTwo.ToInt32();
            return Math.Abs(numTwo + numOne);
        }

        #endregion

        #region generate a unique code by guid

        /// <summary>
        /// generate a unique code by guid
        /// </summary>
        /// <returns></returns>
        public static string GetUniqueCode()
        {
            Guid value = Guid.NewGuid();
            return value.ToUniqueCode();
        }

        #endregion

        #region generate a specified length code by uniquecode

        /// <summary>
        /// generate a specified length code by uniquecode,too ensure uniqueness,recommend to set length at least 16
        /// </summary>
        /// <param name="length">code length</param>
        /// <param name="prefix">prefix</param>
        /// <returns>unique code</returns>
        public static string GetUniqueCode(int length, string prefix = "")
        {
            string uniqueCode = prefix + GetUniqueCode().ToUpper();
            if (uniqueCode.Length == length)
            {
                return uniqueCode;
            }
            if (uniqueCode.Length > length)
            {
                uniqueCode = uniqueCode.Substring(0, length);
            }
            else
            {
                string codeString = "1a2b3c4d5e6f7g8h9i0j9k8l7m6n5o4p3q2r1s0t1u2v3w4x5y6z".ToUpper();
                int randomNum = length - uniqueCode.Length;
                int randomLength = codeString.Length;
                Random random = new Random();
                for (var r = 0; r < randomNum; r++)
                {
                    int csnum = random.Next(randomLength);
                    uniqueCode += codeString[csnum];
                }
            }
            return uniqueCode;
        }

        #endregion
    }
}
