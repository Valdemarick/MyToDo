using AutoMapper;
using MyToDo.Application.Common.Dtos.Tasks;
using Task = MyToDo.Domain.Entities.Task;

namespace MyToDo.Application.Common.Mappings;

internal sealed class TaskProfile : Profile
{
    public TaskProfile()
    {
        CreateMap<Task, TaskDto>()
            .ForMember(dest => dest.CreatorName,
                opt => opt
                    .MapFrom(src => src.Creator.FullName))
            .ForMember(dest => dest.ExecutorName,
                opt => opt
                    .MapFrom(src => src.Creator.FullName));

        CreateMap<Task, PagedTaskDto>()
            .ForMember(dest => dest.CreatorName,
                opt => opt
                    .MapFrom(src => src.Creator.FullName));
    }    
}
