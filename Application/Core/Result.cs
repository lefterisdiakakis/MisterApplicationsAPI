namespace Application.Core
{
    public class Result<T>
    {
        public T Value { get; private set; }
        public string ErrorMessage { get; private set; }
        public ResultTypes ResultType { get; set; }


        public static Result<T> Success(T value) => new() {   Value =value ,ResultType=ResultTypes.Success };
        public static Result<T> NotAuthorized(string message) => new() { ErrorMessage = message, ResultType = ResultTypes.NotAuthorize };
        public static Result<T> Error(string message) => new() { ErrorMessage = message, ResultType = ResultTypes.Error };
    }
}
