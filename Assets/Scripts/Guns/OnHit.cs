using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnHit : MonoBehaviour
{
    public UnityEvent onHit;

    public float timeSinceLastHit = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        timeSinceLastHit += Time.deltaTime;
    }
    public void Hit()
    {
        timeSinceLastHit = 0;
        onHit.Invoke();
    }
}
