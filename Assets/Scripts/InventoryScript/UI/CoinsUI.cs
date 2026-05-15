using UnityEngine;
using TMPro;

public class CoinsUI : MonoBehaviour
{
    [SerializeField] private TMP_Text coinsText;
    private Wallet wallet;

    public void Initialize(Wallet wallet)
    {
        this.wallet = wallet;
        wallet.OnCoinsChanged += UpdateCoinsDisplay;
        UpdateCoinsDisplay(wallet.Coins);
    }

    private void UpdateCoinsDisplay(int coins)
    {
        coinsText.text = $"{coins}";
    }

    private void OnDestroy()
    {
        if (wallet != null)
            wallet.OnCoinsChanged -= UpdateCoinsDisplay;
    }
}
