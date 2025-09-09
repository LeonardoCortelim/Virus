using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class CutsceneManager : MonoBehaviour
{
    [Header("Cutscene Settings")]
    public VideoPlayer videoPlayer;
    public GameObject[] uiToHide;
    public AudioSource backgroundMusic;
    public float fadeDuration = 1f;
    public string nextSceneName;

    private bool cutscenePlaying = false;

    public bool CutscenePlaying => cutscenePlaying;

    public void PlayCutscene()
    {
        if (cutscenePlaying) return;

        cutscenePlaying = true;

        Time.timeScale = 0;

        if (backgroundMusic != null)
            backgroundMusic.Pause();

        foreach (GameObject ui in uiToHide)
            ui.SetActive(false);

        StartCoroutine(FadeInAndPlay());
    }

    private IEnumerator FadeInAndPlay()
    {
        videoPlayer.targetCameraAlpha = 0f;

        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            videoPlayer.targetCameraAlpha = Mathf.Clamp01(t / fadeDuration);
            yield return null;
        }

        videoPlayer.Play();
        videoPlayer.loopPointReached += EndCutscene;
    }

    private void EndCutscene(VideoPlayer vp)
    {
        StartCoroutine(FadeOutAndLoadScene());
    }

    private IEnumerator FadeOutAndLoadScene()
    {
        // cria painel preto full screen
        GameObject fadePanel = new GameObject("FadePanel");
        Canvas canvas = fadePanel.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        CanvasGroup cg = fadePanel.AddComponent<CanvasGroup>();
        cg.alpha = 0f;
        Image img = fadePanel.AddComponent<Image>();
        img.color = Color.black;

        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            float alpha = Mathf.Clamp01(t / fadeDuration);
            videoPlayer.targetCameraAlpha = 1f - alpha; // fade do vídeo
            cg.alpha = alpha;                          // painel cobre a tela
            yield return null;
        }

        // garante que tudo está visível/preto
        videoPlayer.Stop();
        Time.timeScale = 1;
        if (backgroundMusic != null)
            backgroundMusic.UnPause();

        // carrega a nova cena
        if (!string.IsNullOrEmpty(nextSceneName))
            SceneManager.LoadScene(nextSceneName);
    }
}
