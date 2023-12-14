using UnityEngine;
using UnityEngine.UI;

public class GameHUD : MonoBehaviour
{
    public Text coinsText;

    public void UpdateCoinsCount(int coinsCount)
    {
        coinsText.text = "Coins: " + coinsCount.ToString();
    }
}
