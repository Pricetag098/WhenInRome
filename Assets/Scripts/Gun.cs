using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectPooler))]
public class Gun : MonoBehaviour
{
    [Header("Gun Settings")]
    [SerializeField] float fireRate;
    float fireTimer;
    public int maxAmmo;
    public int ammo;
    [SerializeField] float spread;
    [SerializeField] bool auto;

    [SerializeField] float reloadDuration;
    float reloadTime;
    public float reloadProgress;
    bool isReloading;

    [Header("Projectile Settings")]
    [SerializeField] int bulletsFired;
    [SerializeField] float bulletVel;
    [SerializeField] float damage;


    Holster holster;
    PlayerAim aim;
    ObjectPooler pooler;
    

    
    // Start is called before the first frame update
    void Start()
    {
        if(bulletsFired < 1)
        {
            bulletsFired = 1;
        }
            
        ammo = maxAmmo;

        holster = GetComponentInParent<Holster>();
        aim = holster.playerAim;
        pooler = GetComponent<ObjectPooler>();

    }
    

    // Update is called once per frame
    void Update()
    {


        bool fire = (auto && Input.GetMouseButton(0)) || (!auto && Input.GetMouseButtonDown(0));

        if (fire && ammo > 0 && fireTimer > fireRate && !isReloading)
        {
            Fire();
            ammo--;
            fireTimer = 0;
        }
        fireTimer += Time.deltaTime;


        if (Input.GetKeyDown(KeyCode.R) && !isReloading && ammo != maxAmmo)
        {
            reloadTime = 0;
            isReloading = true;
        }

        if (isReloading)
        {
            
            reloadTime += Time.deltaTime;
            reloadProgress = Mathf.Clamp01(reloadTime / reloadDuration);
            if(reloadProgress == 1)
            {
                ammo = maxAmmo;
                isReloading = false;

            }
        }
        
    }

    void Fire()
    {
        for(int i = 0; i< bulletsFired; i++)
        {
            Vector3 dir = aim.aimDir * 10 + RandomVector() * spread;
            dir.Normalize();

            ShootBullet(dir * bulletVel);
        }
    }

    void ShootBullet(Vector3 dir)
    {
        GameObject b = pooler.SpawnObj();
        b.transform.position = transform.position + aim.aimDir * .75f;
        b.GetComponent<Bullet>().Init(dir);
    }

    Vector3 RandomVector()
    {
        return new Vector3(
            Random.value - .5f,
            Random.value - .5f,
            Random.value - .5f
            ).normalized;
    }
}
