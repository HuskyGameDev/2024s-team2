using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public Image ChangeColor;

    public void SetMaxHealth(int Health)
    {
        slider.maxValue = Health;
        slider.value = Health;

        ChangeColor.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int Health)
    {
        slider.value = Health;

        ChangeColor.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void NewMaxHealth(int max, int current)
    {
        slider.maxValue = max;
        slider.value = current;

        ChangeColor.color = gradient.Evaluate(slider.normalizedValue);
    }
}
