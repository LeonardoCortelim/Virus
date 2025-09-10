using UnityEngine;
using System.Collections;

public class BossBeamAttack : MonoBehaviour
{
    public GameObject beamPrefab;
    public Transform slot1;
    public Transform slot2;

    public float spawnInterval = 2f;
    public Vector2 directionSlot1 = Vector2.left;
    public Vector2 directionSlot2 = Vector2.right;

    public AudioSource audioSource;

    void Start()
    {
        StartCoroutine(FireBeams());
    }

    IEnumerator FireBeams()
    {
        while (true)
        {
            SpawnBeam(slot1, directionSlot1);
            SpawnBeam(slot2, directionSlot2);

            // toca o som do laser
            if (audioSource != null)
                audioSource.Play();

            // treme a câmera no momento do disparo
            CameraShake camShake = Camera.main.GetComponent<CameraShake>();
            if (camShake != null)
                StartCoroutine(camShake.Shake(0.2f, 0.3f));

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnBeam(Transform spawnPoint, Vector2 direction)
    {
        GameObject beam = Instantiate(beamPrefab, spawnPoint.position, Quaternion.identity);
        beam.GetComponent<Beam>().SetDirection(direction);
    }
}
