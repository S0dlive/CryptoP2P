using System.Security.Cryptography;
using System.Text;

namespace CryptoP2P.Network.Security;

public static class CryptoService // TODO: IoC implementation.
{
    public static KeyPeer GenerateKeyPeer()
    {
        RSACryptoServiceProvider _cryptoServiceProvider
            = new RSACryptoServiceProvider(4096);
        var publicKey = _cryptoServiceProvider.ExportParameters(false);
        var privateKey = _cryptoServiceProvider.ExportParameters(true);
        return new KeyPeer(Convert.ToBase64String(publicKey.Modulus),
            Convert.ToBase64String(privateKey.D));
    }
}

public record KeyPeer(string PublicKey, string PrivateKey);