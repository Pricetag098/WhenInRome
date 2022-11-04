using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeMeter : MonoBehaviour
{
    public CombatMeter cm;
    public Image bar,filled;

    private void Start()
    {
        bar = GetComponent<Image>();
    }
    // Update is called once per frame
    void Update()
    {
        bar.fillAmount = cm.meter / cm.maxMeter;
        filled.enabled = cm.meter >= cm.maxMeter;
    }
}
