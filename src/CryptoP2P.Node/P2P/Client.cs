using System.Net;
using System.Net.Sockets;

namespace CryptoP2P.Network.P2P;

public class Client
{
    public string ClientId { get; set; }
    public TcpClient TCPClient { get; set; }
    public string ClientName { get; set; }
    public NetworkStream Stream { get; set; }
    public DateTime ConnectedSince { get; set; }
    private byte[] receivedBuffer;

    public void ConnectWithServer(TcpClient client)
    {
        TCPClient.ReceiveBufferSize = 4096;
        TCPClient.SendBufferSize = 4096;
        Stream = client.GetStream();

        receivedBuffer = new byte[4096];
        Stream.BeginRead(receivedBuffer, 0, 4096, ReceivedCallback, null);
    }

    private void ReceivedCallback(IAsyncResult ar)
    {
        try
        {
            int byteLenght = Stream.EndRead(ar);
            if (byteLenght <= 0 )
            {
                // TODO: Disconnet 
                return;
            }

            byte[] data = new byte[4096];
            Stream.BeginRead(receivedBuffer, 0, 4096, ReceivedCallback, null);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}