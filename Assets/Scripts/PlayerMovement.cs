using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;

    [Header("Joystick")]
    public Joystick joystick; // Arraste o Fixed Joystick aqui no Inspector

    [Header("Axis Options")]
    public bool invertX = false;
    public bool invertY = false;
    [Range(0f, 0.5f)] public float deadZone = 0.2f;

    [Header("Physics (Optional)")]
    public bool useRigidbody2D = false;
    public Rigidbody2D rb; // Arraste o Rigidbody2D se usar física

    private Vector2 inputDir = Vector2.zero;

    void Update()
    {
        if (joystick == null)
        {
            Debug.LogError("Joystick não atribuído no inspector!");
            return;
        }

        // Lê valores do joystick
        Vector2 raw = new Vector2(joystick.Horizontal, joystick.Vertical);

        if (invertX) raw.x = -raw.x;
        if (invertY) raw.y = -raw.y;

        // Deadzone e normalização
        if (raw.magnitude < deadZone)
            inputDir = Vector2.zero;
        else
        {
            float mag = (raw.magnitude - deadZone) / (1f - deadZone);
            inputDir = raw.normalized * Mathf.Clamp01(mag);
        }

        // Movimento sem Rigidbody2D
        if (!useRigidbody2D && inputDir != Vector2.zero)
        {
            Vector3 move = new Vector3(inputDir.x, inputDir.y, 0f) * speed * Time.deltaTime;

            // Limites baseados na câmera
            Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
            Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));

            Vector3 newPos = transform.position + move;
            newPos.x = Mathf.Clamp(newPos.x, bottomLeft.x, topRight.x);
            newPos.y = Mathf.Clamp(newPos.y, bottomLeft.y, topRight.y);

            transform.position = newPos;
        }
    }

    void FixedUpdate()
    {
        // Movimento por Rigidbody2D (opcional)
        if (useRigidbody2D)
        {
            if (rb == null)
            {
                Debug.LogError("useRigidbody2D está ativo, mas 'rb' não foi atribuído.");
                return;
            }

            Vector3 move = new Vector3(inputDir.x, inputDir.y, 0f) * speed * Time.fixedDeltaTime;

            Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
            Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));

            Vector3 target = rb.position + (Vector2)move;
            target.x = Mathf.Clamp(target.x, bottomLeft.x, topRight.x);
            target.y = Mathf.Clamp(target.y, bottomLeft.y, topRight.y);

            rb.MovePosition(target);
        }
    }
}
