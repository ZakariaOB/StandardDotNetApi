using StandardApi.Contracts.Contracts.V1.Requests.Queries;
using StandardApi.Domain;
using System;

namespace StandardApi.Services
{
    public interface IUriService
    {
        Uri GetMessageUri(string messageId);

        Uri GetAllMessagesUri(PaginationFilter paginationFilter = null);

        void Init(string routeDetail = null);
    }
}
