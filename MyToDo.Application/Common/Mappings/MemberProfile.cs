using AutoMapper;
using MyToDo.Domain.Entities;
using MyToDo.Domain.ValueObjects.Requests;
using MyToDo.HttpContracts.Members;

namespace MyToDo.Application.Common.Mappings;

internal sealed class MemberProfile : Profile
{
    public MemberProfile()
    {
        CreateMap<Member, MemberDto>();
    }
}
