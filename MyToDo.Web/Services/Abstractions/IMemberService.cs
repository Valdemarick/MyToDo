using MyToDo.Domain.Shared;
using MyToDo.HttpContracts.Members;

namespace MyToDo.Web.Services.Abstractions;

internal interface IMemberService
{
    Task<Result<List<MemberDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    
    Task<Result<MemberDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    Task<Result<MemberPagedListDto>> GetPageAsync(MemberPageRequestDto dto, CancellationToken cancellationToken = default);

    Task<Result> UpdateActivityAsync(Guid memberId, bool isActive, CancellationToken cancellationToken = default);

    Task<Result> RegisterAsync(RegisterMemberDto dto, CancellationToken cancellationToken = default);

    Task<Result> UpdateAsync(UpdateMemberDto dto, CancellationToken cancellationToken = default);

    Task<Result<MemberSessionDto>> LoginAsync(LoginDto dto, CancellationToken cancellationToken = default);
}
