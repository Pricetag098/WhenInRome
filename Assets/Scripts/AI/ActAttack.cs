using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShootAtPlayer", menuName = "Ai/Attack/ShootAtPlayer")]
public class ActAttack : TreeNode
{
    public TreeNode passThrough,onShoot;

    [Header("Gun Settings")]
    [SerializeField] float fireRate;
    float fireTimer;
    [SerializeField] float spread;

    [Header("Projectile Settings")]
    [SerializeField] int bulletsFired;
    [SerializeField] float bulletVel;
    [SerializeField] float damage;
    
    ObjectPooler pooler;
    public override void Run()
    {
        
        if (fireTimer > fireRate)
        {
            Vector3 dir = ai.player.transform.position - ai.transform.position;
            dir.y = 0;
            dir.Normalize();
            Fire(dir);
                
            fireTimer = 0;
            if (onShoot != null)
                onShoot.Run();

        }

        if(passThrough != null)
            passThrough.Run();
        
    }
    public override void Innit(Ai owner)
    {
        ai = owner;
        pooler = ai.GetComponent<ObjectPooler>();
        if (bulletsFired < 1)
        {
            bulletsFired = 1;
        }
        if (passThrough != null)
        {
            passThrough = Instantiate(passThrough);
            passThrough.Innit(owner);
        }
        if (onShoot != null)
        {
            onShoot = Instantiate(onShoot);
            onShoot.Innit(owner);
        }
    }

    void Fire(Vector3 dir)
    {
        Vector3 shootDir = dir;
        

        for (int i = 0; i < bulletsFired; i++)
        {
            Vector3 tempDir = Quaternion.Euler(0, (Random.value - .5f) * spread, 0) * shootDir;
            //dir.Normalize();

            ShootBullet(tempDir * bulletVel);
        }
    }
    public override void Tick()
    {
        fireTimer += Time.deltaTime;
        if (passThrough != null)
        {
            passThrough.Tick();
        }
        if (onShoot != null)
        {
            onShoot.Tick();
        }
    }

    void ShootBullet(Vector3 dir)
    {
        GameObject b = pooler.SpawnObj();
        b.transform.position = ai.transform.position; //+ dir * .75f;
        b.GetComponent<Bullet>().Init(dir, damage,0);
    }
}
