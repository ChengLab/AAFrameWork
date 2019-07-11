using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace AA.AspNetCore.Extensions
{
   public static class AjaxRequestExtensions
    {
        /// <summary>
        /// mvc 操作是否ajax请求 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return request.Headers != null &&
                   request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }

    }
}
