using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 0f;
    public int damage;
    public int perforation;
    public LayerMask enemyLayer;
    Vector3 prevPos;
    Vector3 tmpPos;
    List<GameObject> hitEnemy = new List<GameObject>();
    GameManagerTD gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManagerTD>();
    }

    // Update is called once per frame
    void Update()
    {
        tmpPos = transform.position;

        gameObject.transform.Translate(Vector3.forward * Time.deltaTime * speed);

        prevPos = tmpPos;
        Vector3 dir = transform.position - prevPos;

        if (Physics.Raycast(prevPos, dir.normalized, dir.magnitude, enemyLayer))
        {
            RaycastHit[] hits = Physics.RaycastAll(prevPos, dir.normalized, dir.magnitude, enemyLayer);

            foreach (RaycastHit hit in hits)
            {
                GameObject en = hit.collider.gameObject;
                if (!hitEnemy.Contains(en))
                {
                    hitEnemy.Add(en);
                    Damage(en);
                }                
            }            
        }
    }

    public void Damage(GameObject en)
    {
        Enemy enemy = en.GetComponent<Enemy>();

        enemy.health -= damage;
        perforation--;

        if (enemy.health > 0)
        {
            enemy.UpdateColor();
            enemy.UpdateSpeed();

            gm.money += damage;
            gm.UpdateMoney();
        }
        else
        {
            gm.money += enemy.health + damage;
            gm.UpdateMoney();

            Destroy(enemy.gameObject);
        }


        if (perforation <= 0)
        {
            Destroy(gameObject);
        }
    }
}
