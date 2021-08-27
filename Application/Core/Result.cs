using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core
{
    public class Result<T>
    {
        public T Value { get; private set; }
        public string ErrorMessage { get; private set; }
        public ResultTypes ResultType { get; set; }


        public static Result<T> Success(T value) => new Result<T> {   Value =value ,ResultType=ResultTypes.Success };
        public static Result<T> NotAuthorized(string message) => new Result<T> { ErrorMessage = message, ResultType = ResultTypes.NotAuthorize };
        public static Result<T> Error(string message) => new Result<T> { ErrorMessage = message, ResultType = ResultTypes.Error };
    }
}
