using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform player;
    [SerializeField] Vector3 offset;
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + offset;
    }
}
