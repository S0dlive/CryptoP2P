namespace CryptoP2P.Network.BlockChain;

public static class BlockChain
{
    public static DateTime LastUpdate { get; set; } 
    public static List<Chain> Chains { get; set; } = new List<Chain>();

    public static async Task UpdateLocalBlockChain()
    {
        LastUpdate = DateTime.UtcNow;
        throw new NotImplementedException();
    }

    private static void AddChains(Chain chain)
    {
        throw new NotImplementedException();
    }
}