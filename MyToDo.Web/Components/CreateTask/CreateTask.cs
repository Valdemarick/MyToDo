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
        _members.AddRange((await MemberService.GetAllAsync()).Value);

        _taskTypes.AddRange(Enum.GetValues<TaskTypeDto>());
        _priorities.AddRange(Enum.GetValues<PriorityDto>());
    }

    private async Task CreateAsync()
    { 
        var result = await TaskService.CreateAsync(_createTaskDto);
        if (result.IsFailure)
        {
            ShowErrorDialog(result.Error);
            return;
        }

        await OnClose.InvokeAsync();
    }

    private string GetTaskTypeNameInRussian(TaskTypeDto taskType) => taskType switch
    {
        TaskTypeDto.Bug => "Баг",
        TaskTypeDto.Task => "Задача",
        TaskTypeDto.Idea => "Идея",
        TaskTypeDto.NotChosen => "Не выбрано",
        _ => "Неизвестно"
    };

    private string GetPriorityNameInRussian(PriorityDto priority) => priority switch
    {
        PriorityDto.Low => "Низкий",
        PriorityDto.Normal => "Средний",
        PriorityDto.High => "Высокий",
        PriorityDto.Critical => "Критический",
        PriorityDto.NotChosen => "Не выбрано",
    };
}