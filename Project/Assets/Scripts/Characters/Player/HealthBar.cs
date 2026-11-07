using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#region Class Description
/*
 * 
 */
#endregion

public class HealthBar : MonoBehaviour
{
    #region Fields

    public Slider slider;
    public Gradient gradient;
    public Image fill;
    #endregion

    #region Set values

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
    #endregion
}
