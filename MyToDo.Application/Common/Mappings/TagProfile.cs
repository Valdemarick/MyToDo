using AutoMapper;
using MyToDo.Application.Common.Dtos.Tags;
using MyToDo.Domain.Entities;
using MyToDo.Domain.ValueObjects.Requests;

namespace MyToDo.Application.Common.Mappings;

internal sealed class TagProfile : Profile
{
    public TagProfile()
    {
        CreateMap<Tag, TagDto>();

        CreateMap<TagPageRequestDto, TagPageRequest>();
    }   
}
