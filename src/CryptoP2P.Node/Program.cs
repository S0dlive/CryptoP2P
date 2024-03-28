using CryptoP2P.Network.BlockChain;
using Microsoft.Extensions.Logging;

public class Program
{
    public static void Main()
    {
        ILoggerFactory factory = LoggerFactory
            .Create(builder => builder.AddConsole());
        
        ILogger logger = factory.CreateLogger("Start");
        logger.LogInformation("start of the connection with the crypto network. . .");
        
    }
}