using System.Net;

namespace API.Helper
{
    public class BodyResponse<T>
    {
        public int Result { get; set; }
        public HttpStatusCode Status { get; set; }
        public string Message { get; set; } = string.Empty;
        public T Data { get; set; }
    }
}
