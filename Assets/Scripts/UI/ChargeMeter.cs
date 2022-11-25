using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeMeter : MonoBehaviour
{
    public CombatMeter cm;
    public Image bar,bar2;
    public GameObject filled;
    public Image filledGlow;
    public float fadeTime;

    private Color colour;

    private void Start()
    {
        //bar = GetComponent<Image>();

        colour = filledGlow.color;

        filledGlow.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        bar.fillAmount = cm.meter / cm.maxMeter;
        bar2.fillAmount = cm.meter / cm.maxMeter;
        filled.SetActive(cm.meter >= cm.maxMeter);
        //colour.a = Mathf.PingPong(Time.deltaTime, 1);

        if (cm.meter >= cm.maxMeter)
        {
            filledGlow.gameObject.SetActive(true);
            
            StartCoroutine(FadeOutAndIn()); 
        }

        else
        {
            filledGlow.gameObject.SetActive(false);
        }
           
    }

    IEnumerator FadeOutAndIn()
    {
        // loop over 1 second backwards
        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            // set color with i as alpha
            filledGlow.color = new Color(1, 1, 1, i);
            yield return null;
            Debug.Log(colour.a);
        }

        //Temp to Fade In
        yield return new WaitForSeconds(1);
        Debug.Log("Fade in");

        // loop over 1 second
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            // set color with i as alpha
            filledGlow.color = new Color(1, 1, 1, i);
            yield return null;
        }
    }

    /*IEnumerator FadeIn()
    {
        for (float a = 0; a<= fadeTime; a += Time.deltaTime)
        {
            filledGlow.color = new Color(colour.r, colour.g, colour.b, a);
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        for (float a = fadeTime; a >= 0; a-= Time.deltaTime)
        {
            filledGlow.color = new Color(colour.r, colour.g, colour.b, a);
            yield return null;
        }
    }*/
}
