using System.Resources;

namespace CryptoP2P.Network.Node;

public static class NodeSender
{
    private static void SendData(string clientId,
        Packet packet)
    {
        packet.WriteLength();
        Program.node.ConnectedClients.FirstOrDefault(t =>
            t.ClientId == clientId).SendData(packet);
    }

    private static void SendToAllConnectedClient(Packet packet)
    {
        packet.WriteLength();
        foreach (var client in Program.node.ConnectedClients)
        {
            client.SendData(packet);
        }
    }
    
    private static void SendToAllConnectedClient(string exceptedClientById, Packet packet)
    {
        packet.WriteLength();
        foreach (var client in Program.node.ConnectedClients)
        {
            if (client.ClientId != exceptedClientById)
            {
                client.SendData(packet);
            }
        }
    }

    public static void SendWelcomeMessage(string id, string message)
    {
        using (Packet packet = new Packet((int)ServerPackets.welcome))
        {
            packet.Write(message);
            packet.Write(id);
            SendData(id, packet);
        }
    }
}