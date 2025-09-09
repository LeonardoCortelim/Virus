using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    public float lifeTime = 3f;
    public GameObject hitEffect;
    public AudioClip hitSound;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        bool acertou = false;

        // Inimigo normal
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            acertou = true;
        }

        // Boss
        BossHealth boss = other.GetComponent<BossHealth>();
        if (boss != null)
        {
            boss.TakeDamage(damage);
            acertou = true;
        }

        // Se acertou algo que tem vida
        if (acertou)
        {
            if (hitEffect != null)
            {
                // efeito aparece na posição da bala (rebote)
                GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
                Destroy(effect, 0.3f); // ajusta a duração
            }

            if (hitSound != null)
            {
                AudioSource.PlayClipAtPoint(hitSound, transform.position);
            }

            Destroy(gameObject); // destrói a bala
        }
    }
}
