namespace ApiBiblioteca.Domain.Common;

public class PagedList<T> 
{ 
    public IEnumerable<T> Data { get; set; } = new List<T>(); 
    public int TotalCount { get; set; } 
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize); 
    public int PageNumber { get; set; } 
    public int PageSize { get; set; } 
    public int CurrentPage { get; set; } 
    public bool HasNext => PageNumber < TotalPages; 
    public bool HasPrevious => PageNumber > 1; 
}