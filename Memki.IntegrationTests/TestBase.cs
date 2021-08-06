using System;
using System.Threading.Tasks;
using LightBDD.XUnit2;
using Memki.Model;
using Microsoft.Extensions.DependencyInjection;

namespace Memki.IntegrationTests
{
    public class TestBase: FeatureFixture, IDisposable, IAsyncDisposable
    {
        protected readonly MemkiWebApplicationFactory Factory;

        public TestBase()
        {
            Factory = new MemkiWebApplicationFactory();
        }

        public Context GetContext() => Factory.Services.GetRequiredService<Context>();
        
        public void Dispose()
        {
            Factory.Dispose();
        }

        public ValueTask DisposeAsync()
        {
            return ValueTask.CompletedTask;
        }

        public Task nothing()
        {
            return Task.CompletedTask;
            
        }
    }
}