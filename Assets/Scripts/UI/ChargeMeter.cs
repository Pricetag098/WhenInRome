using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeMeter : MonoBehaviour
{
    public CombatMeter cm;
    public Image bar,bar2;
    public Image filled;
    public CanvasGroup glow;
    public CanvasGroup vinete;
    public float vineteFillSpeed;
    public float freq;
    
    private void Start()
    {
        //bar = GetComponent<Image>();
    }
    // Update is called once per frame
    void Update()
    {
        bar.fillAmount = cm.meter / cm.maxMeter;
        bar2.fillAmount = cm.meter / cm.maxMeter;
        filled.enabled = (cm.meter >= cm.maxMeter);

        float goal = cm.meter >= cm.maxMeter ? 1 : 0;
        vinete.alpha = Mathf.Lerp(vinete.alpha, goal, vineteFillSpeed * Time.deltaTime);
        glow.alpha = goal == 1?Mathf.Sin(Time.time * freq): 0;
    }
}
s