using System.Net.Sockets;
using System.Text.Json;
using CryptoP2P.Network.P2P;
using Microsoft.Extensions.Logging;

namespace CryptoP2P.Network.Node;

public class Node : Peer
{
    public static NodeInformation NodeInformation { get; set; }
    public static List<Client> ConnectedClients { get; set; }
    public Node() 
        : base()
    {
        NodeInformation = new NodeInformation()
        {
            DescriptionNode = null,
            NodeId = null,
            NodeName = null,
            Port = 11247,
        };
    }

    public static async Task StartNodeAsync()
    {
        await Task.Run( async () =>
        {
            _logger.LogInformation("node is starting.");
            
            if (File.Exists("node-information.json"))
            {
                var configuration = File.ReadAllText("node-information.json");
                try
                {
                    var deserialize = JsonSerializer.Deserialize<NodeInformation>(configuration);
                    NodeInformation.NodeId = deserialize.NodeId;
                    NodeInformation.DescriptionNode = deserialize.DescriptionNode;
                    NodeInformation.NodeName = deserialize.NodeName;
                    _server.Start();
                    _logger.LogInformation("node is start.");
                    _server.BeginAcceptTcpClient(new AsyncCallback(TCPConnectionCallback), null);
                }
                catch (Exception e)
                {
                    _logger.LogError("error with the configuration file. Is surely the not good format.");
                    throw;
                }
            }
            else
            {
                using var configuration = File.Create("node-information.json");
                NodeInformation.NodeId = Guid.NewGuid().ToString();
                await File.WriteAllTextAsync("node-information.json",
                    JsonSerializer.Serialize<NodeInformation>(NodeInformation));
            }
        });
    }

    private static void TCPConnectionCallback(
        IAsyncResult result)
    {
        TcpClient client = _server.EndAcceptTcpClient(result);
        _server.BeginAcceptTcpClient(new AsyncCallback(TCPConnectionCallback), null);
        var excorceClient = new Client()
        {
            ClientId = Guid.NewGuid().ToString(),
            TCPClient = client,
            ClientName = client.Client.SocketType.ToString() + "- connected -" + DateTime.Now,
            ConnectedSince = DateTime.Now
        };
        ConnectedClients.Add(excorceClient);
    }
    
    private async Task ValideBlockAsync()
    {
        await Task.Run(() =>
        {
            throw new NotImplementedException();
        });
    }
    
    
}