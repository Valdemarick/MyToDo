namespace MyToDo.Domain.Shared;

public abstract class BaseResult
{
    protected Error ErrorField;
    
    protected BaseResult(bool isSuccess, Error error)
    {
        switch (isSuccess)
        {
            case true when error != Error.None:
                throw new InvalidOperationException();
            case false when error == Error.None:
                throw new InvalidOperationException();
            default:
                IsSuccess = isSuccess;
                ErrorField = error;
                break;
        }
    }

    protected BaseResult()
    {
    }
    
    public bool IsSuccess { get; protected init; }

    public bool IsFailure => !IsSuccess;

    public Error Error
    {
        get
        {
            if (!IsFailure)
            {
                throw new InvalidOperationException("Cannot get access to Error for Success result");
            }

            return ErrorField;
        }
    }
}

public sealed class Result : BaseResult
{
    private Result(bool isSuccess, Error error) : base(isSuccess, error)
    {
    }
    
    public static Result Success() => new(true, Error.None);

    public static Result Failure(Error error) => new(false, error);
    
    
    public static Result<TValue> Success<TValue>(TValue value) => new Result<TValue>(value, true, null!);

    public static Result<TValue> Failure<TValue>(Error error) => new Result<TValue>(default(TValue)!, false, error);
}

public sealed class Result<TValue> : BaseResult
{
    private readonly TValue? _value;
    
    internal Result(TValue value, bool isSuccess, Error error) : base(isSuccess, error)
    {
        _value = value;
    }

    private Result(Result result)
    {
        _value = default(TValue);
        IsSuccess = result.IsSuccess;
        ErrorField = result.Error;
    }

    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("You attempted to get value of failure result");

    public static implicit operator Result<TValue>(Result result) => new(result);
}
