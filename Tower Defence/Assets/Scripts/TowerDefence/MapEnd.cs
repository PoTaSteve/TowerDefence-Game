using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEnd : MonoBehaviour
{
    public string enemyTag = "Enemy(TD)";
    GameManagerTD gm;

    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManagerTD>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(enemyTag))
        {
            // Decrease player health
            gm.health -= other.gameObject.GetComponent<Enemy>().health;
            gm.UpdateHealth();

            Destroy(other.gameObject);
        }
    }
}
