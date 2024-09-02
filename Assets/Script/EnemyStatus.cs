using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    [Header("Status Setting")]
    [Range(0.1f, 3f)]
    public float Speed;
    public int Health, Damage;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetStat(int h, int d)
    {
        Health = h;
        Damage = d;
    }
    public void statmultiply(float M)
    {
        Health = (int)(Health * M);
        Damage = (int)(Damage * M);
    }
}
