using Microsoft.Extensions.DependencyInjection;
using Text.Diff;
using XUnit.Extensions.Essentials;

namespace NUnit2XUnit.Tests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services) => services
            .AddXUnitEssentials()
            .AddSingleton<ITextDiff, TextDiff>();
    }
}
