namespace WebAPI.Response
{
    public class ApiOkResponse<T> : ApiResponse
    {
        public T Result { get; }

        public ApiOkResponse(T result)
            : base(200)
        {
            Result = result;
        }
    }
    public class ApiOkResponse : ApiResponse
    {
        public object Result { get; }

        public ApiOkResponse(object result)
            : base(200)
        {
            Result = result;
        }
    }
}
