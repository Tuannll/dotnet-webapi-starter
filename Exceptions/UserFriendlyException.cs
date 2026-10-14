using System.Net;

namespace WebAPIDemo.Exceptions;

public class UserFriendlyException : Exception
{
    public ErrorCodes ErrorCode { get; set; }
    public HttpStatusCode StatusCode { get; set; }

    public UserFriendlyException(ErrorCodes errorCode, string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        : base(message)
    {
        ErrorCode = errorCode;
        StatusCode = statusCode;
    }
}
