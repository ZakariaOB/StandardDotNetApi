using Microsoft.AspNetCore.WebUtilities;
using StandardApi.Contracts;
using StandardApi.Contracts.Contracts.V1.Requests.Queries;
using StandardApi.Domain;
using System;

namespace StandardApi.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;

        private string _usedUri;

        public string UsedUri 
        { 
            get 
            {
                return !string.IsNullOrEmpty(_usedUri)  ? _usedUri : _baseUri;
            } 
        }

        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }
        public Uri GetMessageUri(string messageId)
        {
            return new Uri(_baseUri + ApiRoutes.Messages.Get.Replace("{messageId}", messageId));
        }

        public void Init(string routeDetail = null)
        {
            if (string.IsNullOrEmpty(routeDetail))
            {
                _usedUri = _baseUri;
            }
            else
            {
                _usedUri = string.Concat(_baseUri, routeDetail);
            }
        }

        public Uri GetAllMessagesUri(PaginationFilter paginationFilter = null)
        {
            if (paginationFilter == null)
            {
                return new Uri(UsedUri);
            }
            var modifiedUri = QueryHelpers.AddQueryString(UsedUri, "pageNumber", paginationFilter.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", paginationFilter.PageSize.ToString());

            return new Uri(modifiedUri);
        }
    }
}
