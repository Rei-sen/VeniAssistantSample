public class ErrorResponseException : Exception
{
    public ErrorResponseException(ErrorDetails error) : base(error.Message)
    {
        Error = error;
    }

    public ErrorDetails Error { get; }
}
