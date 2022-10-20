using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShootInCircle", menuName = "Ai/Attack/ShootInCircle")]
public class ActShootInCircle : TreeNode
{
    public TreeNode passThrough;

    [Header("Gun Settings")]
    [SerializeField] float fireRate;
    float fireTimer;
    

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
        fireTimer += Time.deltaTime;
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
        if (passThrough != null)
        {
            passThrough = Instantiate(passThrough);
            passThrough.Innit(owner);
        }
    }

    void Fire(Vector3 dir)
    {
        Vector3 shootDir = dir;
        float angle = (float)360 /  (float)bulletsFired;

        for (int i = 0; i < bulletsFired; i++)
        {
            Vector3 tempDir = Quaternion.Euler(0, angle*i, 0) * Vector3.forward;
            //dir.Normalize();

            ShootBullet(tempDir * bulletVel);
        }
    }

    void ShootBullet(Vector3 dir)
    {
        GameObject b = pooler.SpawnObj();
        b.transform.position = ai.transform.position; //+ dir * .75f;
        b.GetComponent<Bullet>().Init(dir, damage);
    }
}
