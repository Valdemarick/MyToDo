using AutoMapper;
using MyToDo.Application.Common.Dtos.Members;
using MyToDo.Domain.Entities;

namespace MyToDo.Application.Common.Mappings;

internal sealed class MemberProfile : Profile
{
    public MemberProfile()
    {
        CreateMap<Member, MemberDto>();
    }
}
