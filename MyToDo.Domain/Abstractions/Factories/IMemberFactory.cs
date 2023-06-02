using MyToDo.Domain.Entities;
using MyToDo.Domain.Shared;

namespace MyToDo.Domain.Abstractions.Factories;

public interface IMemberFactory : IBaseFactory
{
    Result<Member> Create(
        string? firstName,
        string? lastName,
        string? email,
        string? hashedPassword,
        bool isActive,
        Role? role);
}
