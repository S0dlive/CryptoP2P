using System.Net;
using System.Net.Sockets;

namespace CryptoP2P.Network.P2P;

public class Client
{
    public string ClientId { get; set; }
    public TcpClient TCPClient { get; set; }
    public string ClientName { get; set; }
    public DateTime ConnectedSince { get; set; }
}