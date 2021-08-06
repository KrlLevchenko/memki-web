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
            ((int)response.StatusCode).Should().BeInRange(200, 299);
            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(body, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        
        public static async Task<string[]> EnsureErrors(this HttpResponseMessage response)
        {
            ((int) response.StatusCode).Should().BeInRange(400, 499);
            var body = await response.Content.ReadAsStringAsync();
            var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(body, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            errorResponse.Should().NotBeNull();
            return errorResponse!.Errors;
        }

        private class ErrorResponse
        {
            public string[] Errors { get; set; }
        }

    }
}