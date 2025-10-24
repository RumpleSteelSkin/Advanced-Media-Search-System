namespace Core.CrossCuttingConcerns.Exceptions.ExceptionTypes;

public class AuthorizationException : Exception
{
    public AuthorizationException(string message) : base(message)
    {
        Errors.Add(message);
    }

    public AuthorizationException(List<string> errors) : base(BuildErrorMessage(errors))
    {
        Errors = errors;
    }

    public List<string> Errors { get; set; } = [];

    private static string BuildErrorMessage(List<string> errors)
    {
        return string.Join("\n", errors);
    }
}