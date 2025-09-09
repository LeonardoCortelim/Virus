using UnityEngine;

public class BossPulse : MonoBehaviour
{
    public float pulseSpeed = 3f; // velocidade da pulsação
    public float pulseAmount = 0.1f; // quanto ele cresce e diminui

    private Vector3 originalScale;

    void Start()
    {
        // Guarda a escala original do sprite
        originalScale = transform.localScale;
    }

    void Update()
    {
        // Calcula o fator de pulsação usando Seno
        float scaleFactor = 1 + Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;

        // Aplica na escala do sprite
        transform.localScale = originalScale * scaleFactor;
    }
}
