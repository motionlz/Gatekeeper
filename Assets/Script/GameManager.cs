using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Wall wallScript;
    public Text CoinTxt;
    [SerializeField]
    bool ResetSave;
    [Header("PlayerData")]
    public int Coin;
    public int Wave;
    public int GateHP;
    public int GateMaxHP;
    [Header("Weapon Level")]
    [Range(0, 5)]
    public int[] WeaponLv;
    [Header("Gun Ammo")]
    public int[] Ammo;
    public int[] MaxAmmo;
    [Header("Weapon GameObject")]
    public GameObject[] Weapon;
    public Button[] WeaponBtn;
    public int[] WeaponBaseStat;
    [Header("PlayerPrefs Name")]
    public string[] saveName;
    public string[] WeaponName;
    public string[] AmmoName;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        if (ResetSave)
        {
            PlayerPrefs.DeleteAll();
        }
        if (PlayerPrefs.HasKey(saveName[0]))
        {
            LoadData();
        }
        else
        {
            SaveData();
        }
    }

    int CalculateStat(int t)
    {
        int Dmg = ((WeaponLv[t] + 1) * WeaponBaseStat[t]);
        return Dmg;
    }
    int calculateGate(int l)
    {
        int hp = 1;
        switch (l)
        {
            case 1: hp = 300; break;
            case 2: hp = 500; break;
            case 3: hp = 700; break;
            case 4: hp = 1000; break;
            default: break;
        }
        return hp;
    }

    void SetWeaponStat(int w)
    {
        if (w == 0)
        {
            Weapon[w].GetComponent<MeleeWeapon>().Atk = CalculateStat(w);
        }
        else
        {
            Weapon[w].GetComponent<GunFire>().AttackDamage = CalculateStat(w);
            Weapon[w].GetComponent<GunFire>().Ammo = Ammo[w];
            WeaponBtn[w].interactable = WeaponLv[w] == 0 ? false : true;
        }
    }
    public void UpdateGateHP()
    {
        GateMaxHP = calculateGate(WeaponLv[5]);
    }

    public void UpdateStat()
    {
        UpdateGateHP();
        wallScript.MaxHP = GateMaxHP;
        wallScript.CurrentHP = GateHP;
        Wall.Instance.UpdateUI();
        CoinUpdate(0);
        for (int i = 0; i < 5; i++)
        {
            SetWeaponStat(i);
        }
    }

    public void CoinUpdate(int c)
    {
        Coin += c;
        CoinTxt.text = Coin.ToString();
    }

    public void LoadData()
    {
        Coin = PlayerPrefs.GetInt(saveName[0]);
        Wave = PlayerPrefs.GetInt(saveName[1]);
        GateHP = PlayerPrefs.GetInt(saveName[2]);
        for (int i = 0; i < 6; i++)
        {
            WeaponLv[i] = PlayerPrefs.GetInt(WeaponName[i]);
        }
        for (int i = 1; i < 5; i++)
        {
            Ammo[i] = PlayerPrefs.GetInt(AmmoName[i]);
        }
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt(saveName[0], Coin);
        PlayerPrefs.SetInt(saveName[1], Wave);
        PlayerPrefs.SetInt(saveName[2], GateHP);
        for (int i = 0; i < 6; i++)
        {
            PlayerPrefs.SetInt(WeaponName[i], WeaponLv[i]);
        }
        for (int i = 1; i < 5; i++)
        {
            PlayerPrefs.SetInt(AmmoName[i], Ammo[i]);
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
