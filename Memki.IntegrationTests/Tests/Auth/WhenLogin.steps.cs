using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Dodo.Primitives;
using FluentAssertions;
using LinqToDB;
using Memki.Components.Auth.Api.Login;
using Memki.IntegrationTests.Extensions;
using Memki.Model.Auth.Entities;
using Microsoft.AspNetCore.Identity;

namespace Memki.IntegrationTests.Tests.Auth
{
    public partial class WhenLogin: TestBase
    {
        private HttpClient _httpClient;

        private string _email = "existing_client_for_login_test_email";
        private string _password = "12345";
        private string _name = "existing_client_for_login_test_name";
        private HttpResponseMessage _response;

        private Task not_authenticated()
        {
            _httpClient = Factory.CreateClient();
            return Task.CompletedTask;
        }

        private async Task user_in_db()
        {
            await using var db = GetContext();
            var dupUserExistInDb = await db.Users.AnyAsync(x => x.Email == _email);
            if (!dupUserExistInDb)
            {
                var passwordHasher = new PasswordHasher<User>();
                var user = new User(Uuid.NewMySqlOptimized(), _name, _email, null);
                user.SetPasswordHash(passwordHasher.HashPassword(user, _password));
                await db.InsertAsync(user);
            }
        }

        private async Task login_user_with_correct_credentials()
        {
            var body = JsonSerializer.Serialize(new CredentialsDto
            {
                Email = _email,
                Password = _password
            });
            _response = await _httpClient.PostAsync("/api/auth/login", 
                new StringContent(body, Encoding.UTF8, "application/json"));        
        }
        
        private async Task login_user_with_incorrect_credentials()
        {
            var body = JsonSerializer.Serialize(new CredentialsDto
            {
                Email = "incorrect " + Uuid.NewTimeBased(),
                Password = "incorrect " + Uuid.NewTimeBased()
            });
            _response = await _httpClient.PostAsync("/api/auth/login", 
                new StringContent(body, Encoding.UTF8, "application/json"));        
        }

        
        private async Task user_was_logged_in()
        {
            var response = await _response.EnsureSuccess<Response>();
            response.UserInfo.Should().NotBeNull();
            response.UserInfo!.Email.Should().Be(_email);
            response.UserInfo!.Name.Should().Be(_name);
            response.UserInfo!.Token.Should().NotBeNull();
        }
        
        private async Task user_was_not_logged_in()
        {
            var response = await _response.EnsureSuccess<Response>();
            response.UserInfo.Should().BeNull();
        }

        private async Task login_user_with_empty_email()
        {
            var body = JsonSerializer.Serialize(new CredentialsDto
            {
                Email = "",
                Password = _password
            });
            _response = await _httpClient.PostAsync("/api/auth/login", 
                new StringContent(body, Encoding.UTF8, "application/json"));
        }
        
        private async Task login_user_with_empty_password()
        {
            var body = JsonSerializer.Serialize(new CredentialsDto
            {
                Email = _email,
                Password = ""
            });
            _response = await _httpClient.PostAsync("/api/auth/login", 
                new StringContent(body, Encoding.UTF8, "application/json"));
        }

        private async Task got_error_about_empty_email()
        {
            var errors = await _response.EnsureErrors();
            errors.Should().HaveCount(1);
            errors.Single().Should().Be("'Email' must not be empty.");
        }
        
        private async Task got_error_about_empty_password()
        {
            var errors = await _response.EnsureErrors();
            errors.Should().HaveCount(1);
            errors.Single().Should().Be("'Password' must not be empty.");
        }

    }
}