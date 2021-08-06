using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Dodo.Primitives;
using FluentAssertions;
using LinqToDB;
using Memki.Components.Auth.Api.Register;
using Memki.IntegrationTests.Extensions;
using Memki.Model;
using Memki.Model.Auth.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Memki.IntegrationTests.Tests.Auth
{
    public partial class WhenRegister: TestBase
    {
        private HttpClient _httpClient;

        private string _email = "demo";
        private string _password = "12345";
        private string _name = "demo_client";
        private HttpResponseMessage _response;

        private Task not_authenticated()
        {
            _httpClient = Factory.CreateClient();
            return Task.CompletedTask;
        }

        private async Task no_user_in_db()
        {
            await using var db = GetContext();
            
            var user = await db.Users.FirstOrDefaultAsync(x => x.Email == _email);
            if (user != null)
            {
                await db.DeleteAsync(user);
            }
        }

        private async Task create_user()
        {
            var body = JsonSerializer.Serialize(new UserDto
            {
                Email = _email,
                Name = _name,
                Password = _password
            });
            _response = await _httpClient.PostAsync("/api/auth/register", 
                new StringContent(body, Encoding.UTF8, "application/json"));
        }

        private async Task user_was_created()
        {
            var response = await _response.EnsureSuccess<Response>();
            response.Token.Should().NotBeNull();
        }

        private async Task user_dup_test_in_db()
        {
            await using var db = GetContext();

            await db.InsertAsync(new User(Uuid.NewMySqlOptimized(), "dup_test", "dup_test", null));
        }

        private async Task create_user_dup_test()
        {
            var body = JsonSerializer.Serialize(new UserDto
            {
                Email = "dup_test",
                Name = "dup_test",
                Password = "_password"
            });
            _response = await _httpClient.PostAsync("/api/auth/register", 
                new StringContent(body, Encoding.UTF8, "application/json"));        }

        private async Task got_error_about_duplicated_user()
        {
            var errors = await _response.EnsureErrors();
            errors.Should().HaveCount(1);

            errors.Single().Should().Be("user_already_exist");
        }
    }
}