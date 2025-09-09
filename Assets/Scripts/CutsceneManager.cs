using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class CutsceneManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;     // Seu Video Player
    public GameObject[] uiToHide;       // UI que você quer esconder durante a cutscene
    public AudioSource backgroundMusic; // Música de fundo
    public float fadeDuration = 1f;     // Duração do fade in/out

    private bool cutscenePlaying = false;

    public bool CutscenePlaying
    {
        get { return cutscenePlaying; }
    }

    public void PlayCutscene()
    {
        if (cutscenePlaying) return;

        cutscenePlaying = true;

        // pausa o jogo
        Time.timeScale = 0;

        // pausa música
        if (backgroundMusic != null)
            backgroundMusic.Pause();

        // desativa UI
        foreach (GameObject ui in uiToHide)
            ui.SetActive(false);

        // faz fade in e toca vídeo
        StartCoroutine(FadeInAndPlay());
    }

    private IEnumerator FadeInAndPlay()
    {
        // garante que a câmera comece invisível
        videoPlayer.targetCameraAlpha = 0f;

        // fade in
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime; // usamos unscaled para não depender do Time.timeScale
            videoPlayer.targetCameraAlpha = Mathf.Clamp01(t / fadeDuration);
            yield return null;
        }

        videoPlayer.Play();
        videoPlayer.loopPointReached += EndCutscene;
    }

    private void EndCutscene(VideoPlayer vp)
    {
        StartCoroutine(FadeOutAndEnd());
    }

    private IEnumerator FadeOutAndEnd()
    {
        // fade out
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            videoPlayer.targetCameraAlpha = Mathf.Clamp01(1f - t / fadeDuration);
            yield return null;
        }

        videoPlayer.Stop(); // garante que o vídeo pare

        // retoma jogo
        Time.timeScale = 1;

        // retoma música
        if (backgroundMusic != null)
            backgroundMusic.UnPause();

        // reativa UI
        foreach (GameObject ui in uiToHide)
            ui.SetActive(true);

        cutscenePlaying = false;
    }
}
