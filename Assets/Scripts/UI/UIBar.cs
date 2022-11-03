using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBar : MonoBehaviour
{
    public Image bar;
    public float maxHP;
    public float curHP;

    void Update()
    {
        bar.fillAmount = curHP / maxHP;
    }
}
