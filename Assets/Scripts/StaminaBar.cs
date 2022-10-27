using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Dash dash;
    public Image bar;

    // Update is called once per frame
    void Update()
    {
        bar.fillAmount = dash.stamina / dash.maxStamina;
    }
}
