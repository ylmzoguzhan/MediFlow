namespace MediFlow.Modules.Patients;

public class Result
{
    public Error? Error { get; }
    public bool IsSuccess { get; }

    protected Result(Error? error, bool isSuccess)
    {
        Error = error;
        IsSuccess = isSuccess;
    }

    public static Result Success() => new(null, true);
    public static Result Failure(Error error) => new(error, false);

    public static Result<TValue> Success<TValue>(TValue value) => Result<TValue>.Success(value);
    public static Result<TValue> Failure<TValue>(Error error) => Result<TValue>.Failure(error);
}

public class Result<TValue> : Result
{
    public TValue? Value { get; }

    private Result(TValue? value, Error? error, bool isSuccess)
        : base(error, isSuccess)
    {
        Value = value;
    }

    public static Result<TValue> Success(TValue value) => new(value, null, true);
    public static new Result<TValue> Failure(Error error) => new(default, error, false);
}

public record class Error(string Code, string Message);
