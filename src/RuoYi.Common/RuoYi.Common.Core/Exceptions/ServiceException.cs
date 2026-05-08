namespace RuoYi.Common.Core.Exceptions
{
    public class ServiceException : Exception
    {
        public int Code { get; set; }

        public ServiceException() { }

        public ServiceException(string message) : base(message) { }

        public ServiceException(string message, int code) : base(message)
        {
            Code = code;
        }

        public ServiceException(string message, Exception innerException) : base(message, innerException) { }
    }
}