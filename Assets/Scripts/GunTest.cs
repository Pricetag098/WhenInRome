using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTest : MonoBehaviour
{
    [SerializeField] PlayerAim aim;
    ObjectPooler pooler;
    [SerializeField] GameObject bullet;
    [SerializeField] float bulletVel;
    // Start is called before the first frame update
    void Start()
    {
        pooler = GetComponent<ObjectPooler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject b = pooler.SpawnObj();
            b.transform.position = transform.position + aim.aimDir * .75f;
            b.GetComponent<Rigidbody>().velocity = aim.aimDir * bulletVel;
        }
    }
}
