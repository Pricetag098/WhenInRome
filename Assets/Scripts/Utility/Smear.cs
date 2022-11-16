using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smear : MonoBehaviour
{
    Material mat;
    
    public string Value = "SmearDirection";
    //public Color colour = Color.white;

    public float intencity;
    public float smearDuation = 1;
    public float smearTime;
    Vector3 dashDir;
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<MeshRenderer>() != null) { mat = GetComponent<MeshRenderer>().material; }
        else if (GetComponent<SkinnedMeshRenderer>() != null) { mat = GetComponent<SkinnedMeshRenderer>().material; }

    }

    // Update is called once per frame
    void Update()
    {
        
        float smearPerc = smearTime / smearDuation;

        Vector4 vect = Vector4.one; //new Vector4(dashDir.x, dashDir.y, dashDir.z, 0);
        mat.SetVector(Value, vect * smearPerc * intencity);
        smearTime = Mathf.Clamp01(smearTime + Time.deltaTime);
        
        if (smearTime < 0)
        {
            smearTime = 0;
        }

    }
    [ContextMenu("Flash")]
    public void SmearModel(Vector3 dir)
    {

        smearTime = 0;
        dashDir = dir * intencity;
    }
}
