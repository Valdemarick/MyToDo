using MyToDo.HttpContracts.Tags;

namespace MyToDo.Web.Services.Abstractions;

internal interface ITagService
{
    Task<List<TagDto>> GetAllTagsAsync(CancellationToken cancellationToken = default);

    Task<TagPagedListDto> GetPageAsync(TagPageRequestDto dto, CancellationToken cancellationToken = default);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
