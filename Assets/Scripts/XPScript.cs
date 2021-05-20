using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPScript : MonoBehaviour
{
    public Slider slider;
    public Image fill;

    public void SetXP(int xp)
    {
        slider.value = xp;
    }

    public void SetMaxXP(int xp)
    {
        slider.maxValue = xp;
        //slider.value = xp;
    }
}
