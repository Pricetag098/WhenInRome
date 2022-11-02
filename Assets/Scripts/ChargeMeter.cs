using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeMeter : MonoBehaviour
{
    public CombatMeter exe;
    Image bar;

    private void Start()
    {
        bar = GetComponent<Image>();
    }
    // Update is called once per frame
    void Update()
    {
        bar.fillAmount = exe.meter / exe.maxMeter;
    }
}
