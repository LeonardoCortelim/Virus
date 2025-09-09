using UnityEngine;

public class BossBobbing : MonoBehaviour
{
    public float floatSpeed = 2f;   // velocidade do movimento
    public float floatAmount = 0.5f; // altura da subida/descida

    private Vector3 initialPosition;

    void Start()
    {
        // Pega a posição inicial do boss
        initialPosition = transform.position;
    }

    void Update()
    {
        // Mantém X e Z, só altera Y para o bobbing
        Vector3 newPos = transform.position;
        newPos.y = initialPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatAmount;
        transform.position = newPos;
    }
}
