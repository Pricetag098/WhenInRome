using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    Material mat;
    public string emmision = "_EmissionColor";
    public Color colour = Color.white;
    [HideInInspector]
    public float frequncy=1;
    [HideInInspector]
    public Color pulseColour = Color.grey;
    public float flashDuation =1;
    
    List<FlashData> flashes = new List<FlashData>();
    // Start is called before the first frame update
    void Start()
    {
        if(GetComponent<MeshRenderer>() != null) { mat = GetComponent<MeshRenderer>().material; }
        else if(GetComponent<SkinnedMeshRenderer>() != null) { mat = GetComponent<SkinnedMeshRenderer>().material; }

    }

    // Update is called once per frame
    void Update()
    {
        Color col = Color.clear;
        List<FlashData> temp = new List<FlashData>();
        foreach(FlashData data in flashes)
        {
            temp.Add(data);
        }
        
        foreach(FlashData data in flashes)
        {
            //Debug.Log(data.color);
            data.time += Time.deltaTime;
            col += (data.color * (1 - (data.time / data.duration)));
            if(data.time > flashDuation)
            {
                temp.Remove(data);
            }
        }
        flashes.Clear();
        foreach(FlashData data in temp)
        {
            flashes.Add(data);
        }
        //Debug.Log(col);


        mat.SetColor(emmision, col + pulseColour * (1-(Mathf.Cos(Time.time * frequncy)/2 + .5f)));
        
        
        
    }
    [ContextMenu("Flash")]
    public void DoFlash()
    {
        FlashData data = new FlashData();
        data.color = colour;
        data.time = 0;
        data.duration = flashDuation;
        flashes.Add(data);
    }

    public void FlashColour(Color col,float time)
    {
        FlashData data = new FlashData();
        data.color = col;
        data.time = 0;
        data.duration = time;
        flashes.Add(data);
    }
    class FlashData 
    {
        public Color color;
        public float duration;
        public float time;

    }

}
