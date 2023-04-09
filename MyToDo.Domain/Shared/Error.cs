namespace MyToDo.Domain.Shared;

public sealed class Error : IEquatable<Error>
{
    public static readonly Error None = new(string.Empty, string.Empty);
    
    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }
    
    public string Code { get; }

    public string Message { get; }
    
    public bool Equals(Error? other)
    {
        if (other is null)
        {
            return false;
        }

        if (other.GetType() != GetType())
        {
            return false;
        }

        return Code == other.Code;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (GetType() != obj.GetType())
        {
            return false;
        }

        if (obj is not Error error)
        {
            return false;
        }

        return Code == error.Code;
    }

    public override int GetHashCode()
    {
        return Code.GetHashCode();
    }

    public static bool operator ==(Error? first, Error? second)
    {
        if (first is null && second is null)
        {
            return true;
        }

        if (first is null || second is null)
        {
            return false;
        }

        return first.Equals(second);
    }

    public static bool operator !=(Error? first, Error? second)
    {
        return !(first == second);
    }
}
