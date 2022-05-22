namespace Task3.Model.DTOs
{
    public class CommandResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public CommandResponse(bool success, string message = "")
        {
            Success = success;
            Message = message;
        }

        public static CommandResponse Failed(string message = "") => new(false, message);
        public static CommandResponse Succeeded(string message = "") => new(true, message);
    }

    public class CommandResponse<T> : CommandResponse
       where T : class
    {
        public T? Result { get; set; }

        public CommandResponse(bool success = false, T? result = null, string message = "")
            : base(success, message)
        {
            Result = result;
        }

        public static new CommandResponse<T> Failed(string message = "")
            => new(false, null, message);
        public static CommandResponse<T> Succeeded(T result, string message = "")
            => new(true, result, message);
    }
}
