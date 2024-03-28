using System.Net.Sockets;
using System.Text.Json;
using CryptoP2P.Network.P2P;
using Microsoft.Extensions.Logging;

namespace CryptoP2P.Network.Node;

public class Node : Peer
{
    public static NodeInformation NodeInformation { get; set; } = new NodeInformation();
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

    public static void StartNode()
    {
        ILoggerFactory factory = LoggerFactory
            .Create(builder => builder.AddConsole());
        _logger = factory.CreateLogger("Node");
        _logger.LogInformation("node is starting.");
        ServerSetup();
        ClientSetup();
        if (File.Exists("node-information.json"))
        {
            var configuration = File.ReadAllText("node-information.json");
            try
            {
                var deserialize = JsonSerializer.Deserialize<NodeInformation>(configuration);
                NodeInformation.NodeId = deserialize.NodeId;
                NodeInformation.DescriptionNode = deserialize.DescriptionNode;
                NodeInformation.NodeName = deserialize.NodeName;
                NodeInformation.Port = deserialize.Port;
                _logger.LogInformation("node is start. on port " + NodeInformation.Port);
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
            var configuration = File.Create("node-information.json");
            NodeInformation.NodeId = Guid.NewGuid().ToString();
            NodeInformation.DescriptionNode = "first-try"; // TODO: A real description
            NodeInformation.NodeName = "first"; // TODO: A real description
            NodeInformation.Port = 11247; // TODO: A real description
            NodeInformation.ConnectedClientMax = 40000; // TODO: A real description
            configuration.Dispose();
            File.WriteAllText("node-information.json",
                JsonSerializer.Serialize<NodeInformation>(NodeInformation));
        }
      
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
        _logger.LogInformation("a connextion is comming : D");
        for (int i = 0; i < NodeInformation.ConnectedClientMax; i++)
        {
            var c = ConnectedClients[i].TCPClient;
            if ( c == null)
            {
                excorceClient.ConnectWithServer(client);
                ConnectedClients[i] = excorceClient;
                return;
            }
        }
        _logger.LogError("a client was connected wrongly.");
    }
    private async Task InitiateSlotAsync()
    {
        for (var x = 0; x < NodeInformation.ConnectedClientMax; x++)
        {
            ConnectedClients.Add(new Client());
        }
    }
    private async Task ValideBlockAsync()
    {
        await Task.Run(() =>
        {
            throw new NotImplementedException();
        });
    }
    
    
}