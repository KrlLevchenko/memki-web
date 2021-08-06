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
                given => no_client_in_db(),
                when => create_user(),
                then => user_was_created()
            );

    }
}