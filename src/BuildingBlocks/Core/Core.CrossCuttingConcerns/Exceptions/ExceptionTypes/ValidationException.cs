namespace Core.CrossCuttingConcerns.Exceptions.ExceptionTypes;

public class ValidationException : Exception
{
    public ValidationException()
    {
        Errors = [];
    }

    public ValidationException(string? message) : base(message)
    {
        Errors = [];
    }

    public ValidationException(string? message, Exception? innerException) : base(message, innerException)
    {
        Errors = [];
    }

    public ValidationException(IEnumerable<ValidationExceptionModel> errors) : base(BuildErrorMessage(errors))
    {
        Errors = errors;
    }

    public IEnumerable<ValidationExceptionModel> Errors { get; }

    private static string BuildErrorMessage(IEnumerable<ValidationExceptionModel> errors)
    {
        var arr = errors.Select(x =>
            $"{Environment.NewLine} -- {x.Property}: {string.Join(Environment.NewLine, x.Errors ?? [])}");
        return $"Validation failed: {string.Join(string.Empty, arr)}";
    }
}