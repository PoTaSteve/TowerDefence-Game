using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mortar : MonoBehaviour
{
    public GameObject Aim;
    public LayerMask enemyLayer;
    public LayerMask obstacleLayer;
    //public float turnSpeed = 15f;
    [Space]
    //public Transform PartToRotate;
    public Transform ShootPoint;

    // Shoot Info
    [Space]
    public float shootTimer;

    //Turret infos
    [Space]
    //public TargetingType targetingType;
    //public float range;
    public float fireRate;

    public int projectileDamage;
    public int projectilePerforation;
    public float projectileSpeed;

    public GameObject projectile;

    public LayerMask groundMask;
    public Vector3 clickPos;
    public bool isSettingAim;

    private void Start()
    {
        shootTimer = 0f;
    }

    public void Update()
    {
        shootTimer += Time.deltaTime;

        if (shootTimer >= fireRate)
        {
            shootTimer = 0f;

            Shoot();
        }
    }

    public void Shoot()
    {
        GameObject proj = Instantiate(projectile, ShootPoint.position, Quaternion.identity);

        Projectile projComp = proj.GetComponent<Projectile>();
        projComp.speed = projectileSpeed;
        projComp.damage = projectileDamage;
        projComp.perforation = projectilePerforation;
    }

    public void SetAim()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, groundMask))
        {
            clickPos = hit.point;
        }

        if (isSettingAim)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Vector3 pos = new Vector3(clickPos.x, 0.1f, clickPos.z);

                Aim.transform.position = pos;

                isSettingAim = false;
            }
        }
    }


}
