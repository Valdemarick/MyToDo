using Microsoft.AspNetCore.Components;
using MyToDo.Domain.Enums;
using MyToDo.HttpContracts.Enums;
using MyToDo.HttpContracts.Tasks;
using Task = MyToDo.Domain.Entities.Task;

namespace MyToDo.Web.Components.TaskFullInfo;

public partial class TaskFullInfo
{
    [Parameter] 
    public TaskFullInfoDto Task { get; set; } = new();
    
    [Parameter] 
    public EventCallback<bool> OnClose { get; set; }

    private async System.Threading.Tasks.Task CloseTaskAsync()
    {
        var closeResult = await TaskService.CompleteAsync(Task.Id);
        if (closeResult.IsFailure)
        {
            ShowErrorDialog(closeResult.Error);
            return;
        }

        await OnClose.InvokeAsync();
    }
    
    private async System.Threading.Tasks.Task ReopenTaskAsync()
    {
        var reopenResult = await TaskService.ReopenTaskAsync(Task.Id);
        if (reopenResult.IsFailure)
        {
            ShowErrorDialog(reopenResult.Error);
            return;
        }

        await OnClose.InvokeAsync();
    }
    
    private async System.Threading.Tasks.Task StartWorkingOnTaskAsync()
    {
        var startWorkingResult = await TaskService.StartWorkingOnTaskAsync(Task.Id);
        if (startWorkingResult.IsFailure)
        {
            ShowErrorDialog(startWorkingResult.Error);
            return;
        }

        await OnClose.InvokeAsync();
    }
    
    private string GetTaskTypeInRussian(TaskTypeDto taskTypeDto) => taskTypeDto switch
    {
        TaskTypeDto.Task => "Задача",
        TaskTypeDto.Bug => "Баг",
        TaskTypeDto.Idea => "Идея",
        _ => "Неизвестно"
    };
    
    private string GetPriorityNameInRussian(PriorityDto priority) => priority switch
    {
        PriorityDto.Low => "Низкий",
        PriorityDto.Normal => "Средний",
        PriorityDto.High => "Высокий",
        PriorityDto.Critical => "Критический",
        _ => "Неизвестно"
    };

    private string GetTaskStatusInRussian(TaskStatusDto taskStatusDto) => taskStatusDto switch
    {
        TaskStatusDto.Open => "Открыта",
        TaskStatusDto.InProgress => "В работе",
        TaskStatusDto.Completed => "Закрыта",
        TaskStatusDto.Reopen => "Переоткрыта",
        _ => "Неизвестно"
    };
}