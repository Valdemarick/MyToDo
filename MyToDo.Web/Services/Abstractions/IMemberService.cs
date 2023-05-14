using MyToDo.Application.Common.Dtos.Members;

namespace MyToDo.Web.Services.Abstractions;

internal interface IMemberService
{
    Task<List<MemberDto>> GetAllAsync(CancellationToken cancellationToken = default);
}
