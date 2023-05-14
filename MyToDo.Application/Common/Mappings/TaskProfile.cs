using AutoMapper;
using MyToDo.Application.Common.Dtos.Tasks;
using Task = MyToDo.Domain.Entities.Task;

namespace MyToDo.Application.Common.Mappings;

internal sealed class TaskProfile : Profile
{
    public TaskProfile()
    {
        CreateMap<Task, TaskDto>()
            .ForCtorParam(nameof(TaskDto.CreatorName),
                opt => opt
                    .MapFrom(src => src.Creator.FullName))
            .ForCtorParam(nameof(TaskDto.ExecutorName),
                opt => opt
                    .MapFrom(src => src.Executor != null ? src.Executor.FullName : null));

        CreateMap<Task, PagedTaskDto>()
            .ForCtorParam(nameof(PagedTaskDto.CreatorName),
                opt => opt
                    .MapFrom(src => src.Creator.FullName))
            .ForCtorParam(nameof(PagedTaskDto.ExecutorName),
                opt => opt
                    .MapFrom(src => src.Executor != null ? 
                        src.Executor.FullName : null));
    }    
}
