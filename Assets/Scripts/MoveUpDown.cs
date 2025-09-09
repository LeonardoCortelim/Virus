using UnityEngine;

public class MoveUpDown : MonoBehaviour
{
    public float speed = 5f;
    public float topLimit = 5f;
    public float bottomLimit = -5f;

    void Update()
    {
        // --- Para celular ---
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Só controla movimento se tocar na metade ESQUERDA da tela
            if (touch.position.x < Screen.width / 2)
            {
                if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                {
                    if (touch.position.y > Screen.height / 2)
                    {
                        if (transform.position.y < topLimit)
                            transform.Translate(Vector3.up * speed * Time.deltaTime);
                    }
                    else
                    {
                        if (transform.position.y > bottomLimit)
                            transform.Translate(Vector3.down * speed * Time.deltaTime);
                    }
                }
            }
        }

        // --- Para PC (teste com teclado) ---
        if (Input.GetKey(KeyCode.W))
        {
            if (transform.position.y < topLimit)
                transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (transform.position.y > bottomLimit)
                transform.Translate(Vector3.down * speed * Time.deltaTime);
        }

        // Clique do mouse: mover só se clicar na esquerda da tela
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;
            if (mousePos.x < Screen.width / 2) // esquerda
            {
                if (mousePos.y > Screen.height / 2)
                {
                    if (transform.position.y < topLimit)
                        transform.Translate(Vector3.up * speed * Time.deltaTime);
                }
                else
                {
                    if (transform.position.y > bottomLimit)
                        transform.Translate(Vector3.down * speed * Time.deltaTime);
                }
            }
        }
    }
}
