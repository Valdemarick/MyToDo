using AutoMapper;
using MyToDo.Domain.Entities;
using MyToDo.Domain.ValueObjects.Requests;
using MyToDo.HttpContracts.Tags;

namespace MyToDo.Application.Common.Mappings;

internal sealed class TagProfile : Profile
{
    public TagProfile()
    {
        CreateMap<Tag, TagDto>();
    }   
}
