using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenManager : MonoBehaviour
{
    public GameObject startScreenUI; // Painel da tela inicial
    public string gameSceneName = "GameScene"; // Nome da cena do jogo

    // Chamado pelo botão "Play"
    public void StartGame()
    {
        // Esconde a tela inicial
        if (startScreenUI != null)
        {
            startScreenUI.SetActive(false);
        }

        // Carrega a cena do jogo
        SceneManager.LoadScene(gameSceneName);
    }

    // Opcional: botão para sair do jogo
    public void QuitGame()
    {
        Application.Quit();
    }
}
