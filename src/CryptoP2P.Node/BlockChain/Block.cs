using System.Security.Cryptography;
using System.Text;

namespace CryptoP2P.Network.BlockChain;

public class Block
{
    public string Hash { get; set; }
    public BlockType Type { get; set; }
    public Stat Stat { get; set; }
    public string PreviousHash { get; set; }
    public List<object> Data { get; set; }
    
    protected string GenerateRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public string GenerateHash()
    {
        var text = GenerateRandomString(32);
        byte[] hash;
        using (SHA512 sha512 = SHA512.Create())
        {
            hash = sha512.ComputeHash(Encoding.UTF8.GetBytes(text));
        }
        return hash.ToString();
    }
}

public enum Stat
{
    
}

public enum BlockType
{
    Transaction, 
    Wallet,
    SimpleBlock
}