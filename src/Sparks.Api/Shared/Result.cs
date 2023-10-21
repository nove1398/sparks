namespace Sparks.Api.Shared;

public class Result<TData>
{
    public TData? Data { get; private init; }
    public ErrorModel? ErrorMessage { get; private init; }

    public bool IsSuccessful => Data is null;
    
    public static Result<TData> Success(TData data)
    {
        return new Result<TData>()
        {
            Data = data
        };
    }

    public static Result<TData> Failure(ErrorModel message)
    {
        return new Result<TData>()
        {
            ErrorMessage = message
        };
    }
}

public sealed class ErrorModel
{
    public string? ErrorCode { get; set; }
    public string ErrorMessage { get; set; }

    public ErrorModel(string message, string? errorCode = null)
    {
        ErrorMessage = message;
        ErrorCode = errorCode;
    }
} 