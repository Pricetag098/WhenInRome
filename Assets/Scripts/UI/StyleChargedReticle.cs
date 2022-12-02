using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StyleChargedReticle : MonoBehaviour
{
    public Image reticle;
    public Image chargedReticle;

    public CombatMeter cm;

    public float rotationSpeed;

    void Update()
    {
        if(cm == null)
        {
            return;
        }
       
        if (cm.meter >= cm.maxMeter)
        {
            reticle.gameObject.SetActive(false);
            chargedReticle.gameObject.SetActive(true);
            Vector3 angles = chargedReticle.transform.eulerAngles;
            angles.z = angles.z - rotationSpeed * Time.deltaTime;
            chargedReticle.transform.eulerAngles = angles;
        }

        else
        {
            reticle.gameObject.SetActive(true);
            chargedReticle.gameObject.SetActive(false);
        }
    }

   
}
