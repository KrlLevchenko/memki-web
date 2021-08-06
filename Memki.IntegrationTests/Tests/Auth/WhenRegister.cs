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

    }
}