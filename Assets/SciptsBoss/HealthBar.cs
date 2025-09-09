using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Image fill;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = Color.red; // barra começa vermelha
    }

    public void SetHealth(int health)
    {
        slider.value = health;

        fill.color = Color.red; // sempre vermelha
    }
}
