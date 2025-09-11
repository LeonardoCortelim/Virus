using UnityEngine;
using System.Collections;

public class BossBeamAttack : MonoBehaviour
{
    public GameObject beamPrefab;

    // Novos slots
    public Transform slot1;
    public Transform slot2;
    public Transform slot3;
    public Transform slot4;

    // Direções dos slots
    public Vector2 directionSlot1 = Vector2.left;
    public Vector2 directionSlot2 = Vector2.right;
    public Vector2 directionSlot3 = Vector2.up;
    public Vector2 directionSlot4 = Vector2.down;

    public float spawnInterval = 2f;

    public AudioSource audioSource;

    void Start()
    {
        StartCoroutine(FireBeams());
    }

    IEnumerator FireBeams()
    {
        while (true)
        {
            // Spawn dos 4 feixes
            SpawnBeam(slot1, directionSlot1);
            SpawnBeam(slot2, directionSlot2);
            SpawnBeam(slot3, directionSlot3);
            SpawnBeam(slot4, directionSlot4);

            // toca o som do laser
            if (audioSource != null)
                audioSource.Play();

            // treme a câmera
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
