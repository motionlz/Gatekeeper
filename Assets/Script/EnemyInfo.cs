using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyInfo : MonoBehaviour
{
    public GameObject[] enemyInfo;
    // Start is called before the first frame update
    public void ShowData(int num)
    {
        enemyInfo[num].SetActive(true);
        Time.timeScale = 0;
    }
    public void CloseUI(int num)
    {
        enemyInfo[num].SetActive(false);
        Time.timeScale = 1;
    }
}
