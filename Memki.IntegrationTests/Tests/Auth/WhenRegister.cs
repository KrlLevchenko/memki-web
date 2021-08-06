using System.Threading.Tasks;
using LightBDD.Framework.Scenarios;
using LightBDD.XUnit2;

namespace Memki.IntegrationTests.Tests.Auth
{
    public partial class WhenRegister
    {
        [Scenario]
        public async Task Should_register_user() =>
            await Runner.RunScenarioAsync(
                given => not_authenticated(),
                given => no_user_in_db(),
                when => create_user(),
                then => user_was_created()
            );
        
        [Scenario]
        public async Task Should_not_register_user_twice() =>
            await Runner.RunScenarioAsync(
                given => not_authenticated(),
                given => user_dup_test_in_db(),
                when => create_user_dup_test(),
                then => got_error_about_duplicated_user()
            );
        
        
        [Scenario]
        public async Task Should_not_register_user_without_email() =>
            await Runner.RunScenarioAsync(
                given => not_authenticated(),
                when => create_user_with_empty_email(),
                then => got_error_about_empty_email()
            );
        
        [Scenario]
        public async Task Should_not_register_user_without_password() =>
            await Runner.RunScenarioAsync(
                given => not_authenticated(),
                when => create_user_with_empty_password(),
                then => got_error_about_empty_password()
            );
        
        [Scenario]
        public async Task Should_not_register_user_without_name() =>
            await Runner.RunScenarioAsync(
                given => not_authenticated(),
                when => create_user_with_empty_name(),
                then => got_error_about_empty_name()
            );

    }
}