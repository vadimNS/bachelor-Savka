public class PlayerEconomy
{
    private Inventory inventory;
    private Wallet wallet;
    public Inventory Inventory => inventory;
    public Wallet Wallet => wallet;
    public PlayerEconomy(Inventory inventory, Wallet wallet)
    {
        this.inventory = inventory;
        this.wallet = wallet;
    }

    public int SellAllInventory()
    {
        int earned = inventory.SellAll();
        wallet.AddCoins(earned);
        return earned;
    }

    public bool TryBuyPickaxe(Pickaxe pickaxe)
    {
        if (pickaxe.Price == 0 || wallet.SpendCoins(pickaxe.Price))
        {
            
            return true;
        }
        return false;
    }

    public int Coins => wallet.Coins;
    
}