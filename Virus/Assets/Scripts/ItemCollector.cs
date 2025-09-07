using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    public CoinManagerTMP coinManager; // Arraste o CoinManagerTMP aqui no Inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            if (coinManager != null)
            {
                coinManager.AddCoin(); // Atualiza placar TMP
            }
            else
            {
                Debug.LogWarning("CoinManagerTMP não está configurado!");
            }

            Destroy(other.gameObject); // Remove a moeda
        }
    }
}
