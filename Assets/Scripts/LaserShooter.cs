using UnityEngine;

public class LaserShooter : MonoBehaviour
{
    public GameObject laserObject; // Arraste o objeto Laser (já na cena, filho da arma)

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            laserObject.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            laserObject.SetActive(false);
        }
    }
}
