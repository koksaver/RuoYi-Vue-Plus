namespace RuoYi.Common.Core.Exceptions
{
    public class GlobalException : Exception
    {
        public int Code { get; set; }

        public GlobalException() { }

        public GlobalException(string message) : base(message) { }

        public GlobalException(string message, int code) : base(message)
        {
            Code = code;
        }

        public GlobalException(string message, Exception innerException) : base(message, innerException) { }
    }
}