using System.Security.Cryptography;
using System.Text;
using CryptoP2P.Network.Security;

namespace CryptoP2P.Network.Wallet;

public class WalletContext
{
    private static List<Wallet> Wallets 
        = new List<Wallet>(); // TODO: Database for wallets.

    public (Wallet, byte[]) CreateWallet()
    {
        Wallet wallet = new Wallet();
        var id = Guid.NewGuid().ToString();
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] adress = sha256.ComputeHash(Encoding.UTF8.GetBytes(id));
            wallet.Adresse = adress;
            var keyPeer = CryptoService.GenerateKeyPeer();
            wallet.PublicKey = keyPeer.PublicKey;
            Wallets.Add(wallet);
            return (wallet, keyPeer.PrivateKey);
        }
    }
    
    public bool DeleteWallet(Wallet wallet)
    {
        return Wallets.Remove(wallet);
    }
    
}