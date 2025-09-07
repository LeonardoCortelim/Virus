using UnityEngine;
using TMPro; // Import necessário para TextMeshPro

public class CoinManagerTMP : MonoBehaviour
{
    public int coinCount = 0;        // Contador de moedas
    public TMP_Text coinText;        // Referência ao TextMeshPro da UI

    void Start()
    {
        UpdateUI();
    }

    public void AddCoin()
    {
        coinCount++;
        UpdateUI();
    }

    void UpdateUI()
    {
        coinText.text = "" + coinCount;
    }
}
