using Radzen;
using RecipeManagement.UI.Constants;

namespace RecipeManagement.UI.Extensions;

public static class RadzenExtensions
{
    public static (int page, int pageSize) GetPagination(this LoadDataArgs loadDataArgs,
        int defaultLimit = PaginationConstants.DefaultPageSize)
    {
        var limit = loadDataArgs.Top ?? defaultLimit;
        var page = (loadDataArgs.Skip.HasValue ? loadDataArgs.Skip.Value / limit : 0) + 1;
        return (page == 0 ? 1 : page, limit);
    }
    
    public static string GetSortsField(this LoadDataArgs loadDataArgs)
    {
        if (!(loadDataArgs.Sorts ?? new List<SortDescriptor>()).Any())
            return string.Empty;
        var formattedSortFields =
            loadDataArgs.Sorts!.Select(s => s.SortOrder == SortOrder.Ascending ? s.Property : $"-{s.Property}");
        return string.Join(',', formattedSortFields);
    }
}