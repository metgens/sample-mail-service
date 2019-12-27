namespace MailService.Common.Exceptions
{
    public enum ErrorCode
    {
        BadRequest = 400,
        Unathorized = 401,
        Forbidden = 403,
        MethodNotAllowed = 405,
        Conflict = 409,
        NotFound = 404,
        Unexpected = 500,
        InternalServerError = 500
    }
}