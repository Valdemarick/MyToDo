using AutoMapper;
using MyToDo.Application.CQRS.Members.Commands.UpdateMemberCommand;
using MyToDo.Domain.Entities;
using MyToDo.HttpContracts.Members;

namespace MyToDo.Application.Common.Mappings;

internal sealed class MemberProfile : Profile
{
    public MemberProfile()
    {
        CreateMap<Member, MemberDto>();

        CreateMap<UpdateMemberCommand, Member>()
            .ForMember(dest => dest.HashedPassword,
                opt => opt.Ignore())
            .ForMember(dest => dest.FullName,
                opt => opt.Ignore())
            .ForMember(dest => dest.RegisteredOn,
                opt => opt.Ignore());

        CreateMap<UpdateMemberDto, UpdateMemberCommand>();
    }
}
