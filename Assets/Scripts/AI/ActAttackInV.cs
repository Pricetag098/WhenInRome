using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShootInV", menuName = "Ai/Attack/ShootInV")]
public class ActAttackInV : TreeNode
{
    public TreeNode passThrough;

    [Header("Gun Settings")]
    [SerializeField] float fireRate;
    float fireTimer;
    [SerializeField] float angle;
    [SerializeField] float offset;
    [SerializeField] float spacing;

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


        }

        if (passThrough != null)
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
        if(bulletsFired % 2 == 0)
        {
            bulletsFired++;
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
        Vector3 origin = dir * offset + ai.transform.position;
        Vector3 leftVect = Quaternion.Euler(0, 180 + angle, 0) * dir;
        Vector3 rightVect = Quaternion.Euler(0,- (180 + angle), 0) * dir;

        int bullets = bulletsFired - 1;
        bullets = bullets / 2;

        ShootBullet(origin,dir);

        if(bullets == 0) { return; }
        for(int i = 1; i < bullets+1; i++)
        {
            ShootBullet(i * spacing * leftVect + origin, dir);
        }
        for (int i = 1; i < bullets+1; i++)
        {
            ShootBullet(i * spacing * rightVect + origin, dir);
        }
        Debug.DrawRay(ai.transform.position,dir * offset);
        Debug.DrawRay(origin, leftVect * offset);
        Debug.DrawRay(origin, rightVect * offset);


    }
    public override void Tick()
    {
        fireTimer += Time.deltaTime;
        if (passThrough != null)
        {
            passThrough.Tick();
        }
    }

    void ShootBullet(Vector3 origin, Vector3 dir)
    {
        GameObject b = pooler.SpawnObj();
        b.transform.position = origin; //+ dir * .75f;
        b.GetComponent<Bullet>().Init(dir * bulletVel, damage);
    }
}
