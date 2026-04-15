using ApiBiblioteca.Domain.Common;
using System.Text.Json;

namespace ApiBiblioteca.API.Header;

public static class HeaderPagination
{
    public static void AppendPaginationHeader<T>(this HttpResponse response, PagedList<T> metadata)
    {
        response.Headers.Append("X-Pagination", JsonSerializer.Serialize(new
        {
            metadata.TotalCount,
            metadata.PageSize,
            metadata.PageNumber,
            metadata.TotalPages,
            metadata.HasNext,
            metadata.HasPrevious
        }));
    }
}
