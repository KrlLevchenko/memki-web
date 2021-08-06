using System.Net;

namespace Memki.Common.ExceptionHandling
{
    public class HandleResult
    {
        public HttpStatusCode StatusCode { get; init; }

        public string[] Errors { get; init; } = new string[0];
    }
}