using System.Collections;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
namespace CryptoP2P.Network.BlockChain;

public class BlockChain
{
    private readonly ILogger _logger;
    private List<Block> Blocks { get; set; }
    public BlockChain()
    {
        Blocks = new List<Block>();
        ILoggerFactory factory = LoggerFactory
            .Create(builder => builder.AddConsole());
        _logger = factory.CreateLogger("Node");
    }

    public void AddBlock(Block block)
    {
        if (Blocks.Last().Hash != block.PreviousHash)
        {
            _logger.LogError("not good previousHash.");
            return;
        }
        
        
    }
}