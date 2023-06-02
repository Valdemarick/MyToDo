using MyToDo.HttpContracts.Common;
using MyToDo.HttpContracts.Tasks;

namespace MyToDo.Web.Pages.Tasks;

public partial class Tasks
{
    private List<TaskShortInfoDto> _tasks = new();
    private PageViewDto _pageView = new PageViewDto();
    
    private readonly TaskPageRequestDto _parameters = new TaskPageRequestDto
    {
        PageIndex = 1,
        PageSize = 10
    };

    private bool _isShowCreateForm = false;

    protected override async Task OnInitializedAsync()
    {
        var taskPagedList = await TaskService.GetPageAsync(_parameters);
        _tasks = taskPagedList.Value.Items;
        _pageView = taskPagedList.Value.PageView;
    }

    private async Task SelectPageAsync(int page)
    {
        _parameters.PageIndex = page;
        await GetMemberPageAsync();
    }
    
    private async Task GetMemberPageAsync()
    {
        var taskPagedList = await TaskService.GetPageAsync(_parameters);
        _tasks = taskPagedList.Value.Items;
        _pageView = taskPagedList.Value.PageView;
    }

    private void ShowCreateForm() => _isShowCreateForm = true;

    private void CloseCreateForm() => _isShowCreateForm = false;
}
