using MyToDo.Domain.Shared;
using MyToDo.HttpContracts.Tags;

namespace MyToDo.Web.Services.Abstractions;

internal interface ITagService
{
    Task<Result<TagPagedListDto>> GetPageAsync(TagPageRequestDto dto, CancellationToken cancellationToken = default);

    Task<Result> CreateAsync(CreateTagDto dto, CancellationToken cancellationToken = default);

    Task<Result> UpdateAsync(UpdateTagDto dto, CancellationToken cancellationToken = default);

    Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
