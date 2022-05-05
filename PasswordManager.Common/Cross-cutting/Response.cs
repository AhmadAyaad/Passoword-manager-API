namespace PasswordManager.Common.Cross_cutting
{
    public class Response
    {
        public Response()
        {
        }
        public Response(ResponseStatus responseStatus, string message)
        {
            Status = responseStatus;
            Messages = new List<string>() { message };
        }
        public ResponseStatus Status { get; set; } = ResponseStatus.Succeeded;
        public List<string> Messages { get; set; }
        public string GetMessages() => string.Join("\n", Messages);
    }

    public class Response<T> : Response
    {
        public Response(T data)
        {
            Data = data;
            Status = ResponseStatus.Succeeded;
            Messages = new List<string>();
        }
        public Response(T data, ResponseStatus responseStatus)
        {
            Data = data;
            Status = responseStatus;
        }
        public Response(T data, ResponseStatus responseStatus, string message)
        {
            Data = data;
            Status = responseStatus;
            Messages = new List<string> { message };
        }
        public T Data { get; set; }
    }
    public enum ResponseStatus
    {
        Unauthorized,
        Succeeded,
        BadRequest,
        NotFound,
        Failed
    }
}
