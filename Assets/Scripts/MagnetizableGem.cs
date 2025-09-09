using UnityEngine;

public class MagnetizableGem : MonoBehaviour
{
    [HideInInspector] public bool isMagnetActive = false;
    [HideInInspector] public Transform player; 
    public float speed = 5f;

    void Update()
    {
        if (isMagnetActive && player != null)
        {
            // Move a gema em direção ao jogador
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }
}
