namespace CryptoP2P.Network.Node;

public class NodeInformation
{
    public string NodeId { get; set; }
    public string NodeName { get; set; }
    public int ConnectedClientMax { get; set; }
    public string DescriptionNode { get; set; }
    public int Port { get; set; }
}