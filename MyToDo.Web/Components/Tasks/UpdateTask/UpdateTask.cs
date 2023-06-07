using Microsoft.AspNetCore.Components;
using MyToDo.HttpContracts.Enums;
using MyToDo.HttpContracts.Members;
using MyToDo.HttpContracts.Tasks;

namespace MyToDo.Web.Components.Tasks.UpdateTask;

public partial class UpdateTask
{
    [Parameter]
    public EventCallback<bool> OnClose { get; set; }
    
    [Parameter]
    public TaskFullInfoDto Task { get; set; }

    private UpdateTaskDto _updateTaskDto = new();
    
    private List<MemberDto> _members = new();

    private List<TaskTypeDto> _taskTypes = new();
    private List<PriorityDto> _priorities = new();

    protected override async Task OnInitializedAsync()
    {
        _members.AddRange((await MemberService.GetAllAsync()).Value);

        _taskTypes.AddRange(Enum.GetValues<TaskTypeDto>());
        _priorities.AddRange(Enum.GetValues<PriorityDto>());
    }

    protected override Task OnParametersSetAsync()
    {
        _updateTaskDto.Id = Task.Id;
        _updateTaskDto.Title = Task.Title;
        _updateTaskDto.Description = Task.Description;
        _updateTaskDto.TaskType = Task.TaskType;
        _updateTaskDto.Deadline = Task.Deadline;
        _updateTaskDto.Priority = Task.Priority;
        _updateTaskDto.ExecutorId = Task.ExecutorId;

        return System.Threading.Tasks.Task.CompletedTask;
    }

    private async Task UpdateAsync()
    {
        var updateResult = await TaskService.UpdateAsync(_updateTaskDto);
        if (updateResult.IsFailure)
        {
            ShowErrorDialog(updateResult.Error);
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
        PriorityDto.NotChosen => "Не выбрано"
    };
}