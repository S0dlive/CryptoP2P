using System.Net.Sockets;
using Microsoft.Extensions.Logging;

namespace CryptoP2P.Network.P2P;

public abstract class Peer
{
    protected static TcpClient _client;
    protected static TcpListener _server;
    protected static ILogger _logger;
    
    public Peer()
    {
        ILoggerFactory factory = LoggerFactory
            .Create(builder => builder.AddConsole());
        _logger = factory.CreateLogger("Peer");
        ServerSetup();
        ClientSetup();
    }

    protected static void ServerSetup()
    {
        _server = TcpListener.Create(11247);
        _server.Start();
    }
    
    protected static void ClientSetup()
    {
        _client = new TcpClient();
    }
    
    
}