using UnityEngine;

public class MagnetCollector : MonoBehaviour
{
    public GameObject magnetPrefab; // Ícone do imã
    public Transform spawnPoint;    // Onde aparecerá o ícone
    public float magnetDuration = 5f;

    private GameObject magnetInstance; // Guarda a instância do ícone

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Magnet"))
        {
            // Instancia o ícone mantendo a rotação correta do prefab
            if (magnetPrefab != null && spawnPoint != null)
            {
                magnetInstance = Instantiate(magnetPrefab, spawnPoint.position, magnetPrefab.transform.rotation);
            }

            // Ativa o magnetismo
            ActivateMagnet();

            Destroy(other.gameObject); // Remove o imã da cena
        }
    }

    void ActivateMagnet()
    {
        // Ativa magnetismo em todas as gemas
        MagnetizableGem[] gems = FindObjectsOfType<MagnetizableGem>();
        foreach (MagnetizableGem gem in gems)
        {
            gem.isMagnetActive = true;
            gem.player = transform;
        }

        // Desativa magnetismo e destrói o ícone após o tempo definido
        Invoke(nameof(DeactivateMagnet), magnetDuration);
    }

    void DeactivateMagnet()
    {
        // Para magnetismo
        MagnetizableGem[] gems = FindObjectsOfType<MagnetizableGem>();
        foreach (MagnetizableGem gem in gems)
        {
            gem.isMagnetActive = false;
            gem.player = null;
        }

        // Remove o ícone do imã da cena
        if (magnetInstance != null)
        {
            Destroy(magnetInstance);
        }
    }
}
