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

    void Start()
    {
        reticle.gameObject.SetActive(true);
        chargedReticle.gameObject.SetActive(false);
    }

    void Update()
    {
        if (cm.meter >= cm.maxMeter)
        {
            reticle.gameObject.SetActive(false);
            chargedReticle.gameObject.SetActive(true);
        }

        else
        {
            reticle.gameObject.SetActive(true);
            chargedReticle.gameObject.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        if (cm.meter >= cm.maxMeter)
        {
            Vector3 angles = chargedReticle.transform.eulerAngles;
            angles.z = angles.z - rotationSpeed * Time.deltaTime;
            chargedReticle.transform.eulerAngles = angles;
        }
    }
}
