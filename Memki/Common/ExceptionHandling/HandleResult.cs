using System.Net;

namespace Memki.Common.ExceptionHandling
{
    public class HandleResult
    {
        public HttpStatusCode StatusCode { get; init; }

        public string Body { get; init; } = "";
    }
}