using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    Material mat;
    public string emmision = "_EmissionColor";
    public Color colour = Color.white;

    public float frequncy;
    public float flashDuation =1;
    public float flashTime;
    // Start is called before the first frame update
    void Start()
    {
        if(GetComponent<MeshRenderer>() != null) { mat = GetComponent<MeshRenderer>().material; }
        else if(GetComponent<SkinnedMeshRenderer>() != null) { mat = GetComponent<SkinnedMeshRenderer>().material; }

    }

    // Update is called once per frame
    void Update()
    {
        
        mat.SetColor(emmision, colour * flashTime / flashDuation);
        flashTime -= Time.deltaTime;
        if(flashTime < 0)
        {
            flashTime = 0;
        }
        
    }
    [ContextMenu("Flash")]
    public void FlashColour()
    {
        flashTime = flashDuation;
    }
}
