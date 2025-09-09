using UnityEngine;

public class WeaponShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    public AudioClip shootSound;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        // --- PC (teste) ---
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    // 🔹 Essa função pode ser chamada pelo botão
    public void Shoot()
    {
        // Instancia o projétil
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Dá velocidade
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = firePoint.right * bulletSpeed;
        }

        // Toca som
        if (shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }
}
