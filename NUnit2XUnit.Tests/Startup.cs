using Microsoft.Extensions.DependencyInjection;
using Xunit;
using XUnit.Extensions.Essentials;

namespace NUnit2XUnit.Tests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services) => services
            .AddXUnitEssentials()
            .AddTransient<IDiff, Diff>();
    }
}
