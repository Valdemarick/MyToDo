using MyToDo.Domain.ValueObjects.PagedLists;
using MyToDo.HttpContracts.Common;
using MyToDo.HttpContracts.Enums;
using MyToDo.HttpContracts.Tags;
using MyToDo.HttpContracts.Tasks;

namespace MyToDo.Web.Pages.Tasks;

public partial class Tasks
{
    private List<TaskShortInfoDto> _tasks = new();
    private PageViewDto _pageView = new PageViewDto();

    private TaskFullInfoDto _taskFullInfoDto;

    private TaskFullInfoDto _taskToUpdate;
    
    private readonly TaskPageRequestDto _parameters = new TaskPageRequestDto
    {
        PageIndex = 1,
        PageSize = 10,
    };

    private bool _isShowCreateForm;
    private bool _isShowFullInfoDialog;
    private bool _isShowUpdateDialog;
    private bool _isShowTagEditDialog;

    private Guid _taskIdToUpdateTags;

    private readonly List<TaskTypeDto> _taskTypes = new();
    private readonly List<TaskStatusDto> _taskStatuses = new();
    private readonly List<PriorityDto> _priorities = new();

    private List<TagDto> _tags = new();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await LoadDataAsync();
    
            _taskStatuses.AddRange(Enum.GetValues<TaskStatusDto>());
            _taskTypes.AddRange(Enum.GetValues<TaskTypeDto>());
            _priorities.AddRange(Enum.GetValues<PriorityDto>());
    
            _tags = (await TagService.GetAllAsync()).Value;
            
            StateHasChanged();
        }
    }

    private async Task SelectPageAsync(int page)
    {
        _parameters.PageIndex = page;
        await LoadDataAsync();
    }
    
    private async Task LoadDataAsync()
    {
        var taskPagedList = await TaskService.GetPageAsync(_parameters);
        if (taskPagedList.IsFailure)
        {
            ShowErrorDialog(taskPagedList.Error);
            return;
        }
        
        _tasks = taskPagedList.Value.Items;
        _pageView = taskPagedList.Value.PageView;
    }

    private void ShowCreateForm() => _isShowCreateForm = true;

    private async Task CloseCreateForm()
    {
        await LoadDataAsync();

        _isShowCreateForm = false;
    }

    private async Task ShowFullInfoDialog(Guid id)
    {
        var getTaskFullInfoResult = await TaskService.GetByIdAsync(id);

        _taskFullInfoDto = getTaskFullInfoResult.Value;

        _isShowFullInfoDialog = true;
    }

    private void CloseFullInfoDialog()
    {
        _taskFullInfoDto = null!;
        _isShowFullInfoDialog = false;
    }

    private async Task ShowUpdateDialog(Guid taskId)
    {
        var getTaskResult = await TaskService.GetByIdAsync(taskId);

        _taskToUpdate = getTaskResult.Value;

        _isShowUpdateDialog = true;
    }

    private async Task CloseUpdateDialog()
    {
        await LoadDataAsync();

        _taskToUpdate = null!;

        _isShowUpdateDialog = false;
    }

    private void ShowTagEditDialog(Guid taskId)
    {
        _taskIdToUpdateTags = taskId;
        _isShowTagEditDialog = true;
    }

    private void CloseTagEditDialog()
    {
        _taskIdToUpdateTags = default;
        _isShowTagEditDialog = false;
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

    private string GetTaskStatusInRussian(TaskStatusDto status) => status switch
    {
        TaskStatusDto.Open => "Открыта",
        TaskStatusDto.InProgress => "В работу",
        TaskStatusDto.Completed => "Закрыта",
        TaskStatusDto.NotChosen => "Не выбрано",
        TaskStatusDto.Reopen => "Переоткрыта"
    };
    
    private async Task ShowOnlyMyTasksAsync()
    {
        await LoadDataAsync();
    }

    private async Task SearchAsync()
    {
        await LoadDataAsync();
    }
}
