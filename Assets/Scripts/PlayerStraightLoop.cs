using UnityEngine;

public class PlayerStraightLoop : MonoBehaviour
{
    public float speed = 5f;          // Velocidade do jogador
    public float moveDuration = 5f;   // Tempo que ele anda para frente
    private Vector3 startPosition;    // Posição inicial do jogador

    private void Start()
    {
        startPosition = transform.position;
        Invoke(nameof(ReturnToStart), moveDuration);
    }

    private void Update()
    {
        // Move o jogador para frente no eixo X
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void ReturnToStart()
    {
        // Volta o jogador para a posição inicial
        transform.position = startPosition;

        // Se quiser que ele continue andando em loop, reinicia a chamada
        Invoke(nameof(ReturnToStart), moveDuration);
    }
}
