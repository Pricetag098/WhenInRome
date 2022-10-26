using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShootInArc", menuName = "Ai/Attack/ShootInArc")]
public class ActShootInArc : TreeNode
{
    public TreeNode passThrough;

    [Header("Gun Settings")]
    [SerializeField] float fireRate;
    float fireTimer;
    

    [Header("Projectile Settings")]
    [SerializeField] int bulletsFired;
    [SerializeField] float bulletVel;
    [SerializeField] float damage;
    [SerializeField] float arc;

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


        }
        
        if (passThrough != null)
            passThrough.Run();
    }
    public override void Innit(Ai owner)
    {
        arc = Mathf.Clamp(arc, 0, 360);
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
    }

    void Fire(Vector3 dir)
    {
        Vector3 shootDir = dir;
        float angle = (float)arc /  (float)bulletsFired;

        int halfBullets = bulletsFired / 2;
        for (int i = -halfBullets; i < halfBullets; i++)
        {
            Vector3 tempDir = Quaternion.Euler(0, angle*i, 0) * dir;
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
    }

    void ShootBullet(Vector3 dir)
    {
        GameObject b = pooler.SpawnObj();
        b.transform.position = ai.transform.position; //+ dir * .75f;
        b.GetComponent<Bullet>().Init(dir, damage);
    }
}
