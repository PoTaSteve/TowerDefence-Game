using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Turret : MonoBehaviour
{
    public Transform target;
    public Enemy EnemyTarget;
    public LayerMask enemyLayer;
    public LayerMask obstacleLayer;
    public float turnSpeed = 15f;
    [Space]
    public Transform PartToRotate;
    public Transform ShootPoint;
    [Space]
    float currentMaxDist = 0;
    float currentMinDist = Mathf.Infinity;
    int currentMaxStrength = 0;

    // Shoot Info
    [Space]
    public float shootTimer;
    public bool canShoot;

    //Turret infos
    [Space]
    public TargetingType targetingType;
    public float range;
    public float fireRate;

    public int projectileDamage;
    public int projectilePerforation;
    public float projectileSpeed;

    public GameObject projectile;

    public bool affectedByVillage;

    //Default values
    public readonly float constRange = 15f;
    public readonly float constFireRate = 0.5f;


    private void Start()
    {
        canShoot = false;
        shootTimer = 0f;
        affectedByVillage = false;
    }

    public void Update()
    {
        if (!canShoot)
        {
            shootTimer += Time.deltaTime;
        }

        if (shootTimer >= fireRate)
        {
            canShoot = true;
            shootTimer = 0f;
        }

        // Get Enemies
        List<Collider> enemiesInRadius = Physics.OverlapSphere(transform.position, range, enemyLayer).ToList();
        List<Collider> enemies = new List<Collider>();

        foreach (Collider collider in enemiesInRadius)
        {
            Vector3 dirToTarget = (collider.transform.position - transform.position).normalized;
            float dstToTarget = Vector3.Distance(collider.transform.position, transform.position);

            if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleLayer))
            {
                enemies.Add(collider);
            }
        }

        if (target != null && !enemies.Contains(target.GetComponent<Collider>()))
        {
            currentMaxDist = 0;
            currentMinDist = Mathf.Infinity;
            currentMaxStrength = 0;

            target = null;
        }

        Enemy currTarget = null;

        if (enemies.Count > 0)
        {
            if (targetingType == TargetingType.First)
            {
                foreach (Collider enemy in enemies)
                {
                    if (enemy.GetComponent<Enemy>().distanceTravelled > currentMaxDist)
                    {
                        currentMaxDist = enemy.GetComponent<Enemy>().distanceTravelled;
                        currTarget = enemy.GetComponent<Enemy>();
                    }
                }
            }
            else if (targetingType == TargetingType.Last)
            {
                foreach (Collider enemy in enemies)
                {
                    if (enemy.GetComponent<Enemy>().distanceTravelled < currentMinDist)
                    {
                        currentMinDist = enemy.GetComponent<Enemy>().distanceTravelled;
                        currTarget = enemy.GetComponent<Enemy>();
                    }
                }
            }
            else // Strong 
            {
                foreach (Collider enemy in enemies)
                {
                    if (enemy.GetComponent<Enemy>().strength < currentMaxStrength)
                    {
                        currentMaxStrength = enemy.GetComponent<Enemy>().strength;
                        currTarget = enemy.GetComponent<Enemy>();
                    }
                }
            }
        }

        if (currTarget != null)
        {
            target = currTarget.gameObject.transform;

            EnemyTarget = currTarget;
        }

        if (target != null)
        {
            Vector3 dir = target.position - transform.position;
            Quaternion lookRot = Quaternion.LookRotation(dir);
            Vector3 rot = Quaternion.Lerp(PartToRotate.rotation, lookRot, Time.deltaTime * turnSpeed).eulerAngles;
            PartToRotate.rotation = Quaternion.Euler(0f, rot.y, 0f);

            if (targetingType == TargetingType.First)
            {
                currentMaxDist = EnemyTarget.distanceTravelled;
            }
            else if (targetingType == TargetingType.Last)
            {
                currentMinDist = EnemyTarget.distanceTravelled;
            }
            else
            {
                currentMaxStrength = EnemyTarget.strength;
            }

            if (canShoot)
            {
                Shoot(lookRot);

                canShoot = false;
            }
        }
        else
        {
            currentMaxDist = 0;
            currentMinDist = Mathf.Infinity;
            currentMaxStrength = 0;
        }
    }

    public string TargetingTypeEnumToString(TargetingType type)
    {
        if (type == TargetingType.First)
        {
            return "First";
        }
        else if (type == TargetingType.Last)
        {
            return "Last";
        }
        else
        {
            return "Strong";
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    
    public void Shoot(Quaternion rot)
    {
        GameObject proj = Instantiate(projectile, ShootPoint.position, rot);

        Projectile projComp = proj.GetComponent<Projectile>();
        projComp.speed = projectileSpeed;
        projComp.damage = projectileDamage;
        projComp.perforation = projectilePerforation;
    }
}
