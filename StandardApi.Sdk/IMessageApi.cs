using Refit;
using StandardApi.Contracts.V1.Requests;
using StandardApi.Contracts.V1.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StandardApi.Sdk
{
    [Headers("Authorization: Bearer")]
    public interface IMessageApi
    {
        [Get("/api/v1/messages")]
        Task<ApiResponse<List<MessageResponse>>> GetAllMessagesAsync();

        [Get("/api/v1/messages/{messageId}")]
        Task<ApiResponse<MessageResponse>> GetAsync(Guid messageId);

        [Post("/api/v1/messages")]
        Task<ApiResponse<MessageResponse>> CreateAsync([Body] CreateMessageRequest createMessageRequest);


        [Delete("/api/v1/messages/{messageId}")]
        Task<ApiResponse<List<string>>> DeleteAsync(Guid messageId);
    }
}
