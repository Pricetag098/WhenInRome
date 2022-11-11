using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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


    public Sprite icon;

    Holster holster;
    PlayerAim aim;
    ObjectPooler pooler;
    bool fire = false;

    PlayerInputs inputActions;
    InputAction fireInput;
    InputAction reloadInput;


    private void Awake()
    {
        inputActions = new PlayerInputs();
        fireInput = inputActions.Player.Fire;
        fireInput.performed += InputOn;
        fireInput.canceled += InputOff;

        reloadInput = inputActions.Player.Reload;
        reloadInput.performed += TryReload;
    }
    private void OnEnable()
    {
        
        fireInput.Enable();
        
        
        reloadInput.Enable();
        
    }
    private void OnDisable()
    {
        fireInput.Disable();
        reloadInput.Disable();
    }


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
        CancelReload();
        fireTimer = equipTime;
    }

    public void CancelReload()
    {
        isReloading = false;
        reloadTime = reloadDuration;
        reloadProgress = 1;
    }

    void TryReload(InputAction.CallbackContext context)
    {
        if (!isReloading && ammo != maxAmmo)
        {
            reloadTime = 0;
            isReloading = true;
            reload.Play();
        }
    }

    void InputOn(InputAction.CallbackContext context)
    {
        Debug.Log("Pressed");
        if (!auto)
        {
            TryFire();
        }
        else
        {
            fire = true;
        }
    }

    void InputOff(InputAction.CallbackContext context)
    {
        Debug.Log("Release");
        fire = false;
    }
    // Update is called once per frame
    void Update()
    {

        if (fire)
        {
            TryFire();
        }



        fireTimer -= Time.deltaTime;


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

    void TryFire()
    {
        if (fireTimer < 0 && !isReloading && Time.timeScale == 1)
        {



            Fire();
            ammo--;
            fireTimer = fireRate;
            if (ammo <= 0)
            {
                reloadTime = 0;
                isReloading = true;
                reload.Play();
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
        b.transform.position = holster.playerAim.transform.position + aim.aimDir * .75f + holster.VertOffset * Vector3.up;
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
