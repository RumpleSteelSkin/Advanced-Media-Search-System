namespace Identity.Application.Constant;

public static class ValidationConstants
{
    public const string NormalCharacters = @"^[a-zA-ZğüşöçıİĞÜŞÖÇç ]+$";

    public const string PhoneNumber = @"^[\d\s\+\-()]+$";

    public const string PasswordRegex =
        @"^(?=.*[A-ZİŞĞÜÖÇ])(?=.*[a-zışğüöç])(?=.*\d)(?=.*[!?\*\.])[A-Za-z0-9İŞĞÜÖÇışğüöç!?\*\.]{6,100}$";

    public const string PasswordRegexErrorMessage =
        "Password must be 6-100 characters long and contain at least one uppercase letter, one lowercase letter, one digit, and one special character (! ? * .).";
}