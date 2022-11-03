using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Collider col;
    public SoundPlayer open,close;
    private void Start()
    {
        col = GetComponent<Collider>();
        col.enabled = false;
    }
    public void Open()
    {
        col.enabled = false;
    }
    public void Close()
    {
        col.enabled=true;
    }
}
