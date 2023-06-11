using Microsoft.AspNetCore.Components;
using MyToDo.HttpContracts.Common;

namespace MyToDo.Web.Shared.Pagination;

public partial class Pagination
{
    [Parameter]
    public PageViewDto PageView { get; set; }
    
    [Parameter] 
    public int Spread { get; set; }
    
    [Parameter]
    public EventCallback<int> SelectedPage { get; set; }
    
    private List<PagingLink> _links;
    
    protected override void OnParametersSet()
    {
        CreatePaginationLinks();
    }
    
    private void CreatePaginationLinks()
    {
        _links = new List<PagingLink>();
        _links.Add(new PagingLink(PageView.CurrentPage - 1, PageView.HasPrevious, "Предыдущая"));
        for (var i = 1; i <= PageView.TotalPages; i++)
        {
            if (i >= PageView.CurrentPage - Spread && i <= PageView.CurrentPage + Spread)
            {
                _links.Add(new PagingLink(i, true, i.ToString()) { Active = PageView.CurrentPage == i });
            }
        }
        
        _links.Add(new PagingLink(PageView.CurrentPage + 1, PageView.HasNext, "Следующая"));
    }
    
    private async Task OnSelectedPage(PagingLink link)
    {
        if (link.Page == PageView.CurrentPage || !link.Enabled)
        {
            return;
        }
        
        PageView.CurrentPage = link.Page;
        await SelectedPage.InvokeAsync(link.Page);
    }
}
