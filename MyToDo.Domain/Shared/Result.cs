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

public class Result : BaseResult
{
    private Result(bool isSuccess, Error error) : base(isSuccess, error)
    {
    }
    
    public static Result Success() => new(true, Error.None);

    public static Result Failure(Error error) => new(false, error);
}

public class Result<TValue> : BaseResult
{
    private readonly TValue? _value;
    
    private Result(TValue value, bool isSuccess, Error error) : base(isSuccess, error)
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

    public static implicit operator Result<TValue>(Result result) => new Result<TValue>(result);

    private static Result<TValue> Success(TValue value) => new(value, true, null!);

    private static Result<TValue> Failure(Error error) => new(default(TValue)!, false, error);
}
