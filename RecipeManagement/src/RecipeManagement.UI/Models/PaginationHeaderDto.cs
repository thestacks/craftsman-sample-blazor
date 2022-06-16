namespace RecipeManagement.UI.Models;

public class PaginationHeaderDto
{
    public int TotalCount { get; set; }   
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}