using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smear : MonoBehaviour
{
    Material mat;
    
    public string Value = "_SmearDirection";
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

        Vector4 vect = new Vector4(dashDir.x, dashDir.y, dashDir.z, 0);
        mat.SetVector(Value, vect * intencity * smearPerc);
        smearTime = Mathf.Clamp01(smearTime - Time.deltaTime);
        
        if (smearTime < 0)
        {
            smearTime = 0;
        }

    }
    [ContextMenu("Flash")]
    public void TestSmear()
    {
        SmearModel(transform.forward);
    }


    public void SmearModel(Vector3 dir)
    {

        smearTime = smearDuation;
        dashDir = dir * intencity;
    }

    
}
