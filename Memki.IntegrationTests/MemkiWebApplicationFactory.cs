using LightBDD.XUnit2;
using Microsoft.AspNetCore.Mvc.Testing;

[assembly: LightBddScope]

namespace Memki.IntegrationTests
{
    public class MemkiWebApplicationFactory: WebApplicationFactory<Startup>
    {
        
    }
}