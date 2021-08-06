using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;

namespace Memki.IntegrationTests.Extensions
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task<T> EnsureSuccess<T>(this HttpResponseMessage response)
        {
            ((int)response.StatusCode).Should().BeInRange(200, 300);
            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(body, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

    }
}