using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class WaveSpawner : MonoBehaviour
{
    public GameObject BaseEnemyPrefab;
    public PathCreator pathCreator;
    public Transform MapStart;

    public int waveNumber;
    public int enemiesAlive;

    private void Start()
    {
        enemiesAlive = 0;
        //StartCoroutine(Round1());
    }

    public void SpawnEnemy(int health, int strength)
    {
        if (strength == 0)
        {
            GameObject enemy = Instantiate(BaseEnemyPrefab, MapStart);

            Enemy enemyComp = enemy.GetComponent<Enemy>();

            enemyComp.pathCreator = pathCreator;
            enemyComp.health = health;
            enemyComp.strength = strength;

            enemyComp.UpdateColor();
            enemyComp.UpdateSpeed();
        }
    }

    public IEnumerator Round1()
    {
        RoundStartAnim(1);

        yield return new WaitForSeconds(3f);

        for (int i = 0; i < 10; i++)
        {
            SpawnEnemy(1, 0);
            enemiesAlive++;

            yield return new WaitForSeconds(0.6f);
        }

        yield return new WaitForSeconds(1.2f);
        
        for (int i = 0; i < 10; i++)
        {
            SpawnEnemy(1, 0);
            enemiesAlive++;

            yield return new WaitForSeconds(0.4f);
        }

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < 8; i++)
        {
            SpawnEnemy(2, 0);
            enemiesAlive++;

            yield return new WaitForSeconds(0.7f);
        }
    }

    public void RoundStartAnim(int round)
    {
        Debug.Log("Starting Round " + round.ToString());
    }
}
