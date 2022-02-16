using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public Transform target;
    public Enemy EnemyTarget;
    public string enemyTag = "Enemy(TD)";
    public float turnSpeed = 15f;

    public Transform PartToRotate;
    public Transform ShootPoint;

    public TargetingType targetingType;

    public List<Enemy> enemies = new List<Enemy>();

    public float currentMaxDist = 0;
    public float currentMinDist = Mathf.Infinity;
    public int currentMaxStrength = 0;

    private void Start()
    {

    }

    public void Update()
    {
        Enemy currTarget = null;

        if (enemies.Count > 0)
        {
            if (targetingType == TargetingType.First)
            {
                foreach (Enemy enemy in enemies)
                {
                    if (enemy.distanceTravelled > currentMaxDist)
                    {
                        currentMaxDist = enemy.distanceTravelled;
                        currTarget = enemy;
                    }
                }
            }
            else if (targetingType == TargetingType.Last)
            {
                foreach (Enemy enemy in enemies)
                {
                    if (enemy.distanceTravelled < currentMinDist)
                    {
                        currentMinDist = enemy.distanceTravelled;
                        currTarget = enemy;
                    }
                }
            }
            else // Strong 
            {
                foreach (Enemy enemy in enemies)
                {
                    if (enemy.strength < currentMaxStrength)
                    {
                        currentMaxStrength = enemy.strength;
                        currTarget = enemy;
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
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(enemyTag))
        {
            enemies.Add(other.gameObject.GetComponent<Enemy>());
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(enemyTag))
        {
            enemies.Remove(other.gameObject.GetComponent<Enemy>());

            if (other.gameObject.transform == target)
            {
                target = null;
                EnemyTarget = null;

                currentMaxDist = 0;
                currentMinDist = Mathf.Infinity;
                currentMaxStrength = 0;
            }
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
}
