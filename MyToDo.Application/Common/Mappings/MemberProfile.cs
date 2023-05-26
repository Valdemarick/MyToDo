using AutoMapper;
using MyToDo.Application.Common.Dtos.Members;
using MyToDo.Domain.Entities;
using MyToDo.Domain.ValueObjects.PagedLists;
using MyToDo.Domain.ValueObjects.Requests;

namespace MyToDo.Application.Common.Mappings;

internal sealed class MemberProfile : Profile
{
    public MemberProfile()
    {
        CreateMap<Member, MemberDto>();

        CreateMap<MemberPageRequestDto, MemberPageRequest>();
    }
}
