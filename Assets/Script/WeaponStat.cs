using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponStat : MonoBehaviour
{
    public static WeaponStat Instance;
    /* 
    0 knife
    1 pistol
    2 shotgun
    3 flamethrower
    4 granade launcher
    */
    public static int currentWeapon = 0;
    [SerializeField]
    GameObject[] Weapon;
    public GameObject[] WeaponIcon;
    void Start()
    {
        Instance = this;
    }
    void Update()
    {

    }

    public void switchWeapon(int n)
    {
        unselect();
        Weapon[n].SetActive(true);
        WeaponIcon[n].SetActive(true);
        Weapon[n].transform.position = new Vector3(0, 0, 0);
        currentWeapon = n;
    }

    void unselect()
    {
        for (int i = 0; i < Weapon.Length; i++)
        {
            Weapon[i].SetActive(false);
            WeaponIcon[i].SetActive(false);
        }
    }
}
