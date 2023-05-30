using AutoMapper;
using MyToDo.Domain.Entities;
using MyToDo.HttpContracts.Roles;

namespace MyToDo.Application.Common.Mappings;

internal sealed class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<Role, RoleDto>();
    }
}
