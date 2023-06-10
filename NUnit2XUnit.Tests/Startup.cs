using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Text.Diff;
using Xunit.Extensions.Essentials;

namespace NUnit2XUnit.Tests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services, HostBuilderContext hostContext) => services
            .AddXunitEssentials(hostContext)
            .AddSingleton<ITextDiff, TextDiff>();
    }
}
