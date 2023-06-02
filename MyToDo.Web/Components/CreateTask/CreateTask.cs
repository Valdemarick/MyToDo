using Microsoft.AspNetCore.Components;
using MyToDo.Domain.Shared;
using MyToDo.HttpContracts.Enums;
using MyToDo.HttpContracts.Members;
using MyToDo.HttpContracts.Tasks;

namespace MyToDo.Web.Components.CreateTask;

public partial class CreateTask
{
    [Parameter] 
    public EventCallback<bool> OnClose { get; set; }
    
    private CreateTaskDto _createTaskDto = new();

    private List<MemberDto> _members = new();

    private List<TaskTypeDto> _taskTypes = new();
    private List<PriorityDto> _priorities = new();

    protected override async Task OnInitializedAsync()
    {
        _members = (await MemberService.GetAllAsync()).Value;
        
        _taskTypes = Enum.GetValues<TaskTypeDto>().ToList();
        _priorities = Enum.GetValues<PriorityDto>().ToList();
    }

    private async Task CreateAsync()
    { 
        await TaskService.CreateAsync(_createTaskDto);

        await OnClose.InvokeAsync();
    }

    private string GetTaskTypeNameInRussian(TaskTypeDto taskType) => taskType switch
    {
        TaskTypeDto.Bug => "Баг",
        TaskTypeDto.Task => "Задача",
        TaskTypeDto.Idea => "Идея",
        _ => "Неизвестно"
    };

    private string GetPriorityNameInRussian(PriorityDto priority) => priority switch
    {
        PriorityDto.Low => "Низкий",
        PriorityDto.Normal => "Средний",
        PriorityDto.High => "Высокий",
        PriorityDto.Critical => "Критический"
    };
}