using AutoMapper;
using MyToDo.Application.Common.Dtos.Tasks;
using MyToDo.Domain.ValueObjects.Requests;
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

        CreateMap<TaskPageRequestDto, TaskPageRequest>();
    }    
}
