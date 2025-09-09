using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public GameObject projectilePrefab;   
    public Transform[] firePoints;        
    public float projectileSpeed = 5f;    
    public float fireRate = 2f;           
    public int projectilesPerShot = 3;    
    public float spreadAngle = 30f;       

    [Header("Audio")]
    public AudioClip shootSound;          // som do tiro
    private AudioSource audioSource;

    private float fireTimer = 0f;

    void Start()
    {
        // Pega ou adiciona AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireRate)
        {
            fireTimer = 0f;
            Shoot();
        }
    }

    void Shoot()
    {
        for (int i = 0; i < projectilesPerShot; i++)
        {
            int index = Random.Range(0, firePoints.Length);
            Transform chosenPoint = firePoints[index];

            GameObject proj = Instantiate(projectilePrefab, chosenPoint.position, Quaternion.identity);
            Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                float angle = (i - (projectilesPerShot - 1)/2f) * spreadAngle;
                Vector2 dir = Quaternion.Euler(0, 0, angle) * Vector2.left;
                rb.velocity = dir * projectileSpeed;
            }
        }

        // Toca o som do tiro
        if (shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }
}
