using UnityEngine;
using System.Collections;

public class BossSlowPower : MonoBehaviour
{
    [Header("Referências")]
    public GameObject player;       // Jogador
    public AudioSource audioSource; // Som do Slow
    public GameObject slowIcon;     // Ícone do debuff

    [Header("Configurações do Slow")]
    public float slowDuration = 3f;    // Duração do efeito
    public float slowFactor = 0.5f;    // Fator de lentidão
    public float slowCooldown = 8f;    // Tempo de espera entre ataques
    public float firstSlowDelay = 10f; // Antes do primeiro Slow

    [Header("Efeito do Slow")]
    public GameObject slowEffectPrefab; // Prefab do efeito visual (ParticleSystem)
    public Transform bossTransform;     // Ponto de origem do efeito
    public Canvas canvasPrefab;          // Canvas em World Space

    private bool isOnCooldown = false;
    private float timer = 0f;

    void Start()
    {
        if (slowIcon != null)
            slowIcon.SetActive(false);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= firstSlowDelay && !isOnCooldown)
        {
            StartCoroutine(DoSlow());
            timer = 0f;
        }
    }

    private IEnumerator DoSlow()
    {
        isOnCooldown = true;

        // Ativa ícone
        if (slowIcon != null)
            slowIcon.SetActive(true);

        // Toca som
        if (audioSource != null)
            audioSource.Play();

        // Instancia Canvas em World Space e efeito
        Canvas canvasInstance = null;
        if (canvasPrefab != null && bossTransform != null)
        {
            canvasInstance = Instantiate(canvasPrefab, bossTransform.position, Quaternion.identity);
            canvasInstance.transform.SetParent(bossTransform);
            canvasInstance.transform.localPosition = Vector3.zero;
            canvasInstance.transform.localRotation = Quaternion.identity;
            canvasInstance.transform.localScale = Vector3.one;

            if (slowEffectPrefab != null)
            {
                GameObject effect = Instantiate(slowEffectPrefab, canvasInstance.transform);
                effect.transform.localPosition = Vector3.zero;
                ParticleSystem ps = effect.GetComponent<ParticleSystem>();
                if (ps != null) ps.Play();
                Destroy(effect, ps != null ? ps.main.duration + ps.main.startLifetime.constantMax : 2f);
            }
        }

        // Aplica lentidão
        if (player != null)
        {
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 originalVelocity = rb.velocity;
                rb.velocity *= slowFactor; // reduz a velocidade atual

                yield return new WaitForSeconds(slowDuration);

                rb.velocity = originalVelocity; // volta à velocidade normal
            }
        }

        if (slowIcon != null)
            slowIcon.SetActive(false);

        if (canvasInstance != null)
            Destroy(canvasInstance.gameObject);

        yield return new WaitForSeconds(slowCooldown);
        isOnCooldown = false;
    }
}
