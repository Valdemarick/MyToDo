using MyToDo.HttpContracts.Common;
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
        PageSize = 10
    };

    private bool _isShowCreateForm;
    private bool _isShowFullInfoDialog;
    private bool _isShowUpdateDialog;

    protected override async Task OnInitializedAsync()
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

    private async Task SelectPageAsync(int page)
    {
        _parameters.PageIndex = page;
        await GetTaskPageAsync();
    }
    
    private async Task GetTaskPageAsync()
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
        await GetTaskPageAsync();

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
        await GetTaskPageAsync();

        _taskToUpdate = null!;

        _isShowUpdateDialog = false;
    }
}
