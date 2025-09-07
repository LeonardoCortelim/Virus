using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    public GameObject gameOverPanel; // Arraste o painel do Game Over aqui no Inspector

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Die();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Die();
        }
    }

    void Die()
    {
        // Para o jogo (opcional)
        Time.timeScale = 0f; 

        // Mostra o painel de Game Over
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }

    // Função para o botão Reiniciar
    public void RestartGame()
    {
        Time.timeScale = 1f; // Retorna o tempo ao normal
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
