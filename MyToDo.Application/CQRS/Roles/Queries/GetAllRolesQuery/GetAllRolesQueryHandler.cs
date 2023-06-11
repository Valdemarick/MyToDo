using AutoMapper;
using MyToDo.Application.Abstractions.Messaging;
using MyToDo.Domain.Abstractions.Repositories;
using MyToDo.Domain.Shared;
using MyToDo.HttpContracts.Roles;

namespace MyToDo.Application.CQRS.Roles.Queries.GetAllRolesQuery;

internal sealed class GetAllRolesQueryHandler : IQueryHandler<GetAllRolesQuery, List<RoleDto>>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper _mapper;

    public GetAllRolesQueryHandler(IRoleRepository roleRepository, IMapper mapper)
    {
        _roleRepository = roleRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<RoleDto>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await _roleRepository.GetAllAsync(cancellationToken);

        var rolesDto = _mapper.Map<List<RoleDto>>(roles);

        return Result.Success(rolesDto);
    }
}
