using System.Text;
using System.Text.Json.Nodes;
using Infrastructure.Caches;

namespace Infrastructure.Fakes;

public class FakeCachePopulatingService : ICachePopulatingService
{
    public Task PopulateAsync()
    {
        var flags = ReadJsonArrayAsStringArray(@"Fakes\flags.json");
        var segments = ReadJsonArrayAsStringArray(@"Fakes\segments.json");

        FakeCache.Populate(
            flags.Select(x => Encoding.UTF8.GetBytes(x)),
            segments.Select(x => Encoding.UTF8.GetBytes(x))
        );

        return Task.CompletedTask;
    }

    private static IEnumerable<string> ReadJsonArrayAsStringArray(string path)
    {
        var json = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, path));
        var jArray = JsonNode.Parse(json)!.AsArray();

        return jArray.Select(x => x!.ToJsonString());
    }
}