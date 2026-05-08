namespace RuoYi.Common.Core
{
    public class AjaxResult
    {
        public int Code { get; set; }
        public string? Msg { get; set; }
        public object? Data { get; set; }

        public static AjaxResult Success(object? data = null)
        {
            return new AjaxResult { Code = 200, Msg = "操作成功", Data = data };
        }

        public static AjaxResult Success(string msg, object? data = null)
        {
            return new AjaxResult { Code = 200, Msg = msg, Data = data };
        }

        public static AjaxResult Error(string msg)
        {
            return new AjaxResult { Code = 500, Msg = msg };
        }

        public static AjaxResult Error(int code, string msg)
        {
            return new AjaxResult { Code = code, Msg = msg };
        }

        public static AjaxResult Warn(string msg)
        {
            return new AjaxResult { Code = 601, Msg = msg };
        }
    }
}