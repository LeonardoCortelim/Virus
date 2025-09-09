using UnityEngine;
using TMPro;

public class CoinManagerTMP : MonoBehaviour
{
    [Header("Moedas")]
    public int coinCount = 0;        
    public TMP_Text coinText;        

    [Header("Cutscene")]
    public CutsceneManager cutsceneManager; // arraste o CutsceneManager aqui

    [Header("Áudio")]
    public AudioClip coinSound;       // o som da moeda
    private AudioSource audioSource;  // componente que toca o som
    [Range(0f, 1f)]
    public float coinVolume = 0.3f;   // volume do som da moeda (30% por padrão)

    private bool cutsceneTriggered = false; // garante que a cutscene toque apenas uma vez

    void Start()
    {
        UpdateUI();

        // pega o AudioSource ou adiciona um se não existir
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void AddCoin()
    {
        coinCount++;
        UpdateUI();

        // toca o som da moeda com volume ajustado
        if (coinSound != null)
            audioSource.PlayOneShot(coinSound, coinVolume);

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
