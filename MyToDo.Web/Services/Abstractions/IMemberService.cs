using MyToDo.Application.Common.Dtos.Members;

namespace MyToDo.Web.Services.Abstractions;

internal interface IMemberService
{
    Task<MemberPagedListDto> GetPageAsync(MemberPageRequestDto dto, CancellationToken cancellationToken = default);

    Task UpdateActivityAsync(Guid memberId, bool isActive, CancellationToken cancellationToken = default);
}
