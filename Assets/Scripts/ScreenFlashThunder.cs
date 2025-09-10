using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFlashThunder : MonoBehaviour
{
    public Image flashImage;       // seu painel branco
    public AudioSource thunderSound;
    public float flashDuration = 0.2f;
    public float interval = 15f;

    void Start()
    {
        // garante que o painel começa invisível
        if (flashImage != null)
            flashImage.color = new Color(1,1,1,0);

        StartCoroutine(FlashRoutine());
    }

    IEnumerator FlashRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);

            // toca o som
            if (thunderSound != null)
                thunderSound.Play();

            // faz o flash visível
            if (flashImage != null)
            {
                flashImage.color = new Color(1,1,1,1); // totalmente visível
                // espera o tempo de flash
                yield return new WaitForSeconds(flashDuration);
                // volta a invisível
                flashImage.color = new Color(1,1,1,0);
            }
        }
    }
}
