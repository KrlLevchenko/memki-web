using System.Threading.Tasks;
using LightBDD.Framework.Scenarios;
using LightBDD.XUnit2;

namespace Memki.IntegrationTests.Tests.Auth
{
    public partial class WhenLogin
    {
        [Scenario]
        public async Task Should_login_user() =>
            await Runner.RunScenarioAsync(
                given => not_authenticated(),
                given => user_in_db(),
                when => login_user_with_correct_credentials(),
                then => user_was_logged_in()
            );
        
        [Scenario]
        public async Task Should_not_login_user_with_incorrect_credentials() =>
            await Runner.RunScenarioAsync(
                given => not_authenticated(),
                given => user_in_db(),
                when => login_user_with_incorrect_credentials(),
                then => user_was_not_logged_in()
            );

        
      
        
        
        [Scenario]
        public async Task Should_not_login_user_without_email() =>
            await Runner.RunScenarioAsync(
                given => not_authenticated(),
                when => login_user_with_empty_email(),
                then => got_error_about_empty_email()
            );
        
        [Scenario]
        public async Task Should_not_login_user_without_password() =>
            await Runner.RunScenarioAsync(
                given => not_authenticated(),
                when => login_user_with_empty_password(),
                then => got_error_about_empty_password()
            );
    }
}