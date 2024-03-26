using Microsoft.Extensions.Logging;
namespace CryptoP2P.Network.BlockChain;

public class BlockChain
{
    private readonly ILogger _logger;
    public BlockChain()
    {
        ILoggerFactory factory = LoggerFactory
            .Create(builder => builder.AddConsole());
        _logger = factory.CreateLogger("BlockChain");
        InitializeBlockChainAsync();
    }
    public DateTime LastUpdate { get; set; } 
    public List<Chain> Chains { get; set; } 
    
    public async Task UpdateLocalBlockChainAsync()
    {
        LastUpdate = DateTime.UtcNow;
        throw new NotImplementedException();
    }

    private async Task InitializeBlockChainAsync()
    {
        _logger.LogInformation("Blockchain initialization is underway. . .");
        Chains = new List<Chain>();
        // TODO: Request with P2P network and implementation
        _logger.LogInformation("Blockchain initialization is finish.");
    }
    private void AddChains(Chain chain)
    {
        throw new NotImplementedException();
    }
}