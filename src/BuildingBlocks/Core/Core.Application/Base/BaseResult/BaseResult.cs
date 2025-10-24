using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace Core.Application.Base.BaseResult;

[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public class BaseResult<T>
{
    public T? Data { get; init; }

    public IEnumerable<Error>? Errors { get; init; }

    [JsonIgnore] public bool IsSuccess => Errors == null || !Errors.Any();
    [JsonIgnore] public bool IsFailure => !IsSuccess;

    public static BaseResult<T> Success(T data)
    {
        return new BaseResult<T> { Data = data };
    }

    public static BaseResult<T> Fail()
    {
        return new BaseResult<T>
        {
            Errors = new List<Error>
            {
                new() { ErrorMessage = "There was an error processing your request." }
            }
        };
    }

    public static BaseResult<T> Fail(string error)
    {
        return new BaseResult<T>
        {
            Errors = new List<Error> { new() { ErrorMessage = error } }
        };
    }

    public static BaseResult<T> Fail(IEnumerable<IdentityError> errors)
    {
        return new BaseResult<T>
        {
            Errors = errors.Select(e => new Error { PropertyName = e.Code, ErrorMessage = e.Description }).ToList()
        };
    }

    public static BaseResult<T> Fail(IEnumerable<string> errors)
    {
        return new BaseResult<T>
        {
            Errors = errors.Select(e => new Error { ErrorMessage = e }).ToList()
        };
    }

    public static BaseResult<T> NotFound(string message)
    {
        return new BaseResult<T>
        {
            Errors = new List<Error> { new() { ErrorMessage = message } }
        };
    }
}