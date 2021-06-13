namespace StandardApi.Contracts.Contracts.V1.Responses
{
    public class Response<T>
    {
        public T Date { get; set; }

        public Response() { }

        public Response(T response)
        {
            Date = response;
        }
    }
}
