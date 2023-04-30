using MyToDo.Domain.Entities;

namespace MyToDo.Application.Abstractions.Security;

public interface IJwtProvider
{
    Task<string> GenerateTokenAsync(Member member);
}
