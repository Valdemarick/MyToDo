using MyToDo.Domain.Entities;
using MyToDo.HttpContracts.Members;

namespace MyToDo.Application.Abstractions.Security;

public interface IJwtProvider
{
    Task<MemberSessionDto> GenerateTokenAsync(Member member);
}
