public class Wallet
{
    public System.Action<int> OnCoinsChanged;
    private int coins;
    public int Coins => coins;

    public void AddCoins(int amount)
    {
        if (amount > 0)
            coins += amount;
        OnCoinsChanged?.Invoke(coins);
    }

    public bool SpendCoins(int amount)
    {
        if (amount <= 0) return true;
        if (coins >= amount)
        {
            coins -= amount;
            OnCoinsChanged?.Invoke(coins);
            return true;
        }

        return false;
    }
    public void SetCoins(int amount)
    {
        coins = UnityEngine.Mathf.Max(0, amount);
        OnCoinsChanged?.Invoke(coins);
    }
}