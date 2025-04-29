namespace Application.Shared
{
    public class Result
    {
        public string Message { get; set; }
        public object? Content { get; set; }

        public Result(string message)
        {
            Message = message;
        }

        public Result(string message, object? content)
        {
            Message = message;
            Content = content;
        }
    }
}
