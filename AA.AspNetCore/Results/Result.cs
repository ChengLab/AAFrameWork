using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AA.AspNetCore.Results
{

    public class Result<T> : Result
    {
        public T Data { get; set; }

        public static Result<T> Response(bool isSuccess, T data, string code = "", string msg = "")
        {
            return new Result<T>
            {
                IsSuccess = isSuccess,
                Message = msg,
                Code = code,
                Data = data
            };
        }
    }
    public class Result
    {

        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string Code { get; set; }
        public Result() { }
        public Result(bool isSuccess, string code, string msg)
        {
            this.IsSuccess = isSuccess;
            this.Message = msg;
            this.Code = code;
        }
        public static Result ResponseSuccess(string msg = "", string code = "")
        {
            return new Result(true, code, msg);
        }

        public static Result ResponseError(string msg = "", string code = "")
        {
            return new Result(false, code, msg);
        }
        public static Result<T> Response<T>(bool isSuccess, T data, string code = "", string msg = "")
        {
            return new Result<T>
            {
                IsSuccess = isSuccess,
                Message = msg,
                Code = code,
                Data = data
            };
        }
    }
}
