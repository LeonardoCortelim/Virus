using UnityEngine;

public class Beam : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 2f;
    private Vector2 moveDirection;

    public void SetDirection(Vector2 dir)
    {
        moveDirection = dir.normalized;
    }

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.position += (Vector3)moveDirection * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Hit kill simples: destruir o jogador
            Destroy(other.gameObject);

            // Opcional: mostrar mensagem no console
            Debug.Log("Player morreu! Hit kill ativado.");
        }
    }
}
