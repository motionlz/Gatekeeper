using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    int Wave;
    int EnemyLeft;
    float statMultiply;
    WaveManager waveManager;
    public int RandomEnemyPool;
    [Header("Enemy GameObject")]
    public GameObject[] EnemyType;
    public GameObject CursedEnemy;
    public GameObject[] SpawnPoint;
    public GameObject[] OnMapSpawnPoint;

    [Header("Spawnrate")]
    public float initialstart;
    public float continueSpawn;
    void Start()
    {
        waveManager = GetComponent<WaveManager>();
    }
    void spawn()
    {
        EnemyLeft -= 1;
        int A = Random.Range(0, 5);
        GameObject temp = Instantiate(EnemyType[RandomEnemy()], SpawnPoint[A].transform.position, Quaternion.identity);
        temp.GetComponent<SpriteRenderer>().sortingOrder = A;
        temp.GetComponent<EnemyStatus>().statmultiply(statMultiply);

        if (EnemyLeft <= 0)
        {
            CancelInvoke();
            StartCoroutine(NextWave());
        }
    }
    void CursedSpawn1()
    {
        int T = Random.Range(0, 5);
        GameObject temp = Instantiate(CursedEnemy, OnMapSpawnPoint[T].transform.position, Quaternion.identity);
        temp.GetComponent<SpriteRenderer>().sortingOrder = T;
    }
    void CursedSpawn2()
    {
        int T = Random.Range(0, 5);
        GameObject temp = Instantiate(CursedEnemy, OnMapSpawnPoint[5 + T].transform.position, Quaternion.identity);
        temp.GetComponent<SpriteRenderer>().sortingOrder = T;
    }

    int RandomEnemy()
    {
        return Random.Range(0, RandomEnemyPool);
    }

    public void SetWave(float rate, int e, float m, int w)
    {
        continueSpawn = rate;
        EnemyLeft = e;
        statMultiply = m;
        Wave = w;
        StartWave(w);
    }
    void StartWave(int w)
    {
        InvokeRepeating("spawn", initialstart, continueSpawn);
        if (w >= 11)
        {
            InvokeRepeating("CursedSpawn1", initialstart + 2, 13);
        }
        if (w >= 21)
        {
            InvokeRepeating("CursedSpawn2", initialstart + 2, 13);
        }
    }

    IEnumerator NextWave()
    {
        yield return new WaitForSeconds(3);
        waveManager.nextWave();
    }
}
