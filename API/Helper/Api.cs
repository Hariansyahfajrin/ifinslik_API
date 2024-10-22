using System.Net;

namespace API.Helper
{
    public class Api
    {
        public static BodyResponse<T> CreateResponse<T>(int Result, HttpStatusCode status, T data, string message)
        {
            return new BodyResponse<T>
            {
                Result = Result,
                Status = status,
                Message = message,
                Data = data
            };
        }
    }
}
