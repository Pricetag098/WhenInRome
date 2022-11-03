using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectPooler))]
public class Gun : MonoBehaviour
{
    [Header("Gun Settings")]
    [SerializeField] float equipTime;
    [SerializeField] float fireRate;
    float fireTimer;
    public int maxAmmo;
    public int ammo;
    [SerializeField] float spread;
    [SerializeField] bool auto;
    [SerializeField] float aimAssistAngle = 0;

    [SerializeField] float reloadDuration;
    float reloadTime;
    public float reloadProgress;
    bool isReloading;

    [Header("Projectile Settings")]
    [SerializeField] int bulletsFired;
    [SerializeField] float bulletVel;
    [SerializeField] float damage;

    [Header("Sounds")]
    [SerializeField] SoundPlayer shoot;
    [SerializeField] SoundPlayer reload;
    [SerializeField] SoundPlayer equip;
    [SerializeField] SoundPlayer empty;


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
    
    public void Equip()
    {
        equip.Play();
        isReloading = false;
        reloadTime = reloadDuration;
        fireTimer = equipTime;
    }

    // Update is called once per frame
    void Update()
    {


        bool fire = (auto && Input.GetMouseButton(0)) || (!auto && Input.GetMouseButtonDown(0));

        if (fire && fireTimer < 0 && !isReloading)
        {
            if(ammo <= 0)
            {
                reloadTime = 0;
                isReloading = true;
                reload.Play();
            }
            else
            {
                Fire();
                ammo--;
                fireTimer = fireRate;
            }
            
        }
        fireTimer -= Time.deltaTime;


        if (Input.GetKeyDown(KeyCode.R) && !isReloading && ammo != maxAmmo)
        {
            reloadTime = 0;
            isReloading = true;
            reload.Play();
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
        shoot.Play();
        if(ammo == 1)
        {
            empty.Play();
        }
        Vector3 shootDir = aim.aimDir;
        if(aimAssistAngle > 0)
        {
            shootDir = aim.GetAssistedDir(aimAssistAngle);
        }

        for(int i = 0; i< bulletsFired; i++)
        {
            Vector3 dir = Quaternion.Euler(0, (Random.value - .5f) * spread, 0) * shootDir;
            //dir.Normalize();

            ShootBullet(dir * bulletVel);
        }
    }

    void ShootBullet(Vector3 dir)
    {
        GameObject b = pooler.SpawnObj();
        b.transform.position = transform.position + aim.aimDir * .75f;
        b.GetComponent<Bullet>().Init(dir,damage);
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
