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

    protected override async Task OnInitializedAsync()
    {
        var taskPagedList = await TaskService.GetPageAsync(_parameters);
        _tasks = taskPagedList.Items;
        _pageView = taskPagedList.PageView;
    }

    private async Task SelectPageAsync(int page)
    {
        _parameters.PageIndex = page;
        await GetMemberPageAsync();
    }
    
    private async Task GetMemberPageAsync()
    {
        var taskPagedList = await TaskService.GetPageAsync(_parameters);
        _tasks = taskPagedList.Items;
        _pageView = taskPagedList.PageView;
    } 
}
