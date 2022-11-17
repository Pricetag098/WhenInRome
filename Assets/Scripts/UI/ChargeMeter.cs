using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeMeter : MonoBehaviour
{
    public CombatMeter cm;
    public Image bar,bar2;
    public GameObject filled;

    private void Start()
    {
        //bar = GetComponent<Image>();
    }
    // Update is called once per frame
    void Update()
    {
        bar.fillAmount = cm.meter / cm.maxMeter;
        bar2.fillAmount = cm.meter / cm.maxMeter;
        filled.SetActive(cm.meter >= cm.maxMeter);
    }
}
