using AutoMapper;
using MyToDo.Application.CQRS.Tasks.Commands.CreateTaskCommand;
using MyToDo.Domain.ValueObjects.Requests;
using MyToDo.HttpContracts.Tasks;
using Task = MyToDo.Domain.Entities.Task;

namespace MyToDo.Application.Common.Mappings;

internal sealed class TaskProfile : Profile
{
    public TaskProfile()
    {
        CreateMap<Task, TaskShortInfoDto>()
            .ForCtorParam(nameof(TaskShortInfoDto.CreatorName),
                opt => opt
                    .MapFrom(src => src.Creator.FullName));

        CreateMap<CreateTaskDto, CreateTaskCommand>()
            .ForCtorParam(nameof(CreateTaskCommand.Deadline), 
                opt => opt
                    .MapFrom(src => src.Deadline.ToUniversalTime()));
    }    
}
