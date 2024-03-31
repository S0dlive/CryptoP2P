using System.Security.Cryptography;
using System.Text;

namespace CryptoP2P.Network.Security;

public static class CryptoService // TODO: IoC implementation.
{
    public static KeyPeer GenerateKeyPeer()
    {
        RSACryptoServiceProvider cryptoServiceProvider
            = new RSACryptoServiceProvider(4096);
        var publicKey = cryptoServiceProvider.ExportParameters(false);
        var privateKey = cryptoServiceProvider.ExportParameters(true);
        return new KeyPeer(Convert.ToBase64String(publicKey.Modulus),
            Convert.ToBase64String(privateKey.D));
    }

    public static bool VerifyData(byte[] data, byte[] signature, RSAParameters publicKey)
    {
        using (RSACryptoServiceProvider cryptoServiceProvider = new RSACryptoServiceProvider())
        {
            cryptoServiceProvider.ImportParameters(publicKey);
            return cryptoServiceProvider
                .VerifyData(data, signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        }
    }
}

public record KeyPeer(string PublicKey, string PrivateKey);