using StandardApi.Contracts.Contracts.V1.Responses;
using StandardApi.Domain;
using StandardApi.Services;
using System.Collections.Generic;
using System.Linq;

namespace StandardApi.Helpers
{
    public static class PaginationHelper
    {
        public static PagedResponse<T> CreatePaginatedResponse<T>(IUriService uriService, PaginationFilter pagination, IEnumerable<T> response)
        {
            string nextPage = pagination.PageNumber >= 1
                            ? uriService.GetAllMessagesUri(
                                  new PaginationFilter
                                  {
                                      PageNumber = pagination.PageNumber + 1,
                                      PageSize = pagination.PageSize
                                  }).ToString()
                            : null;

            string previousPage = pagination.PageNumber - 1 >= 1
                            ? uriService.GetAllMessagesUri(
                                  new PaginationFilter
                                  {
                                      PageNumber = pagination.PageNumber - 1,
                                      PageSize = pagination.PageSize
                                  }).ToString()
                            : null;

            return new PagedResponse<T>
            {
                Data = response,
                PageNumber = pagination.PageNumber >= 1 ? pagination.PageNumber : (int?)null,
                PageSize = pagination.PageSize >= 1 ? pagination.PageSize : (int?)null,
                NextPage = response.Any() ? nextPage : null,
                PreviousPage = previousPage
            };
        }
    }
}
