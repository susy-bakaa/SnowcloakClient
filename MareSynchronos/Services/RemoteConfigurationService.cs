using Chaos.NaCl;
using MareSynchronos.MareConfiguration;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace MareSynchronos.Services;

public sealed class RemoteConfigurationService
{

    private readonly ILogger<RemoteConfigurationService> _logger;
    private readonly RemoteConfigCacheService _configService;
    private readonly Task _initTask;

    public RemoteConfigurationService(ILogger<RemoteConfigurationService> logger, RemoteConfigCacheService configService)
    {
        _logger = logger;
        _configService = configService;
        _initTask = Task.Run(DownloadConfig);
    }
    

    private async Task DownloadConfig()
    {
        // Removed Lop's remote config code. Function exists purely to keep things clean.
        LoadConfig();
        
    }

    private void LoadConfig()
    {
        ulong ts = 1755859494;

        var configString = "{\"mainServer\":{\"api_url\":\"wss://hub.snowcloak-sync.com/\",\"hub_url\":\"wss://hub.snowcloak-sync.com/mare\"},\"repoChange\":{\"current_repo\":\"https://hub.snowcloak-sync.com/repo.json\",\"valid_repos\":[\"https://hub.snowcloak-sync.com/repo.json\"]},\"noSnap\":{\"listOfPlugins\":[\"Snapper\",\"Snappy\",\"Meddle.Plugin\"]}}";

        _configService.Current.Configuration = JsonNode.Parse(configString)!.AsObject();
        _configService.Current.Timestamp = ts;
        _configService.Save();
    }
}
