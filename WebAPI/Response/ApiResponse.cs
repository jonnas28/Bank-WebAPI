namespace WebAPI.Response
{
    public class ApiResponse
    {
        // HTTP status code for the response
        public int StatusCode { get; }

        // Message associated with the response
        public string Message { get; }

        /// <summary>
        /// Constructs an ApiResponse with the given status code and an optional message.
        /// If no message is provided, a default message is determined based on the status code.
        /// </summary>
        /// <param name="statusCode">HTTP status code</param>
        /// <param name="message">Optional message (defaults to a predefined message for the status code)</param>
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        /// <summary>
        /// Gets the default message associated with a specific HTTP status code.
        /// </summary>
        /// <param name="statusCode">HTTP status code</param>
        /// <returns>Default message for the status code</returns>
        private static string GetDefaultMessageForStatusCode(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    return "Resource not found";
                case 401:
                    return "Unauthorized Access!";
                case 500:
                    return "An unhandled error occurred";
                case 200:
                    return "Successful";
                case 204:
                    return "Successful, No Content.";
                default:
                    return "Something went wrong!";
            }
        }
    }
}
