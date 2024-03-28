using CryptoP2P.Network.BlockChain;
using CryptoP2P.Network.Node;
using Microsoft.Extensions.Logging;

public class Program
{
    public static async Task Main()
    {
        ILoggerFactory factory = LoggerFactory
            .Create(builder => builder.AddConsole());
        ILogger logger = factory.CreateLogger("Start");
        logger.LogInformation("start of the connection with the crypto network. . .");
        Node.StartNode();
        Console.ReadKey();
    }
}