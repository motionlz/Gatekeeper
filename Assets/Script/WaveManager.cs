using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;
    public EnemyInfo enemyInfo;
    GameManager gameManager;
    Spawner spawner;
    [Header("Wave Setting")]
    public int Wave;
    public int EnemyPerWave;
    public int startWaveEnemy;
    int EnemyPool = 1;
    public Text WaveTxt;
    // Start is called before the first frame update

    void Start()
    {
        Instance = this;
        gameManager = GameManager.Instance;
        spawner = this.GetComponent<Spawner>();
        //WaveSetUp();
    }
    void WaveCheck(int W)
    {
        if (W >= 21)
            EnemyPool = 5;
        else if (W >= 16)
            EnemyPool = 4;
        else if (W >= 6)
            EnemyPool = 3;
        else if (W >= 2)
            EnemyPool = 2;
        else
            EnemyPool = 1;

        spawner.RandomEnemyPool = EnemyPool;
    }

    void enemyInfoCheck(int w)
    {
        switch (w)
        {
            case 1: enemyInfo.ShowData(0); break;
            case 2: enemyInfo.ShowData(1); break;
            case 6: enemyInfo.ShowData(2); break;
            case 11: enemyInfo.ShowData(3); break;
            case 16: enemyInfo.ShowData(4); break;
            case 21: enemyInfo.ShowData(5); break;
            default: break;
        }
    }
    public void WaveSetUp()
    {
        Wave = gameManager.Wave;
        WaveCheck(Wave);
        enemyInfoCheck(Wave);
        WaveTxt.text = "Wave " + Wave;
        float R = 2.5f - (float)((int)Wave / 10) / 10;
        int E = startWaveEnemy + ((Wave - 1) * EnemyPerWave);
        int M = (Wave - 1) / 2;
        float m = 1 + ((float)M / 10);
        spawner.SetWave(R, E, m, Wave);
    }

    public void nextWave()
    {
        Wave += 1;
        SaveData();
        WaveSetUp();
    }

    public void SaveData()
    {
        gameManager.Wave = Wave;
        gameManager.GateHP = Wall.Instance.CurrentHP;
        for (int i = 1; i < 5; i++)
        {
            gameManager.Ammo[i] = gameManager.Weapon[i].GetComponent<GunFire>().Ammo;
        }
        gameManager.SaveData();
    }
}
