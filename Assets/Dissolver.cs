using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolver : MonoBehaviour
{
    Material mat;
    float dissolve = 0;
    public float dissolveRate = 1;
    bool doDissolve = false;
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<MeshRenderer>() != null) { mat = GetComponent<MeshRenderer>().material; }
        else if (GetComponent<SkinnedMeshRenderer>() != null) { mat = GetComponent<SkinnedMeshRenderer>().material; }
    }

    // Update is called once per frame
    void Update()
    {
        if (doDissolve)
        {
            dissolve += Time.deltaTime * dissolveRate;
        }
        mat.SetFloat("_Dissolve", dissolve);
    }
    public void Dissolve()
    {
        doDissolve = true;
    }
}
