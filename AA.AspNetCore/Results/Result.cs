using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AA.AspNetCore.Results
{

    public class Result<T> : Result
    {
        public T Data { get; set; }

        public static Result<T> Response(bool isSuccess, T data, string msg = "success")
        {
            return new Result<T>
            {
                IsSuccess = isSuccess,
                Message = msg,
                Data = data 
            };
        }
    }
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public Result() { }
        public Result(bool isSuccess, string msg)
        {
            this.IsSuccess = isSuccess;
            this.Message = msg;
        }
        public static Result Success(string msg = "success")
        {
            return new Result(true, msg);
        }

        public static Result Error(string message)
        {
            return new Result(false, message);
        }
    }
}
