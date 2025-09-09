using UnityEngine;
using TMPro;

public class CoinManagerTMP : MonoBehaviour
{
    [Header("Moedas")]
    public int coinCount = 0;        
    public TMP_Text coinText;        

    [Header("Cutscene")]
    public CutsceneManager cutsceneManager; // arraste o CutsceneManager aqui

    private bool cutsceneTriggered = false; // garante que a cutscene toque apenas uma vez

    void Start()
    {
        UpdateUI();
    }

    public void AddCoin()
    {
        coinCount++;
        UpdateUI();

        // toca cutscene quando chegar a 50 moedas, apenas uma vez
        if (coinCount >= 50 && !cutsceneTriggered)
        {
            cutsceneTriggered = true;
            if (cutsceneManager != null)
                cutsceneManager.PlayCutscene();
        }
    }

    void UpdateUI()
    {
        coinText.text = coinCount.ToString();
    }
}
