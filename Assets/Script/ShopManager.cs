using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public GameObject[] WeaponPic;
    [Header("Button Prefab")]
    public GameObject AmmoBuyBtn;
    public GameObject RepairBtn;
    public GameObject UpgradeBtn;
    [Space(20)]
    public GameManager gameManager;
    public GameObject WeaponInventory;
    int SelectedWeapon;
    public int[] AmmoAmount;
    public int[] AmmoPrice;
    public int RepairAmount;
    public int RepairPrice;
    [Header("Text")]
    public Text CoinTxt;
    public Text AmmoLeftTxt;
    public Text AmmoMaxTxt;
    public Text TypeTxt;
    public Text UpgradeCostTxt;
    public Text BuyAmmoCostTxt;
    [Space(20)]
    public GameObject[] Star;
    [Header("Weapon upgrade cost")]
    public int[] KnifeLvCost;
    public int[] PistolLvCost;
    public int[] ShotgunLvCost;
    public int[] FlameLvCost;
    public int[] GLLvCost;
    public int[] GateLvCost;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        UpdateUI();
    }

    public void UpgradeWeapon()
    {
        gameManager.Coin -= costCal();
        gameManager.WeaponLv[SelectedWeapon] += 1;
        gameManager.UpdateStat();
        UpgradeBtn.GetComponent<Button>().interactable = false;
        UpdateUI();
    }

    public void BuyAmmo()
    {
        gameManager.Coin -= AmmoPrice[SelectedWeapon];
        gameManager.Ammo[SelectedWeapon] = Mathf.Clamp(gameManager.Ammo[SelectedWeapon] + AmmoAmount[SelectedWeapon], 0, gameManager.MaxAmmo[SelectedWeapon]);
        AmmoBuyBtn.GetComponent<Button>().interactable = false;
        UpdateUI();
    }

    public void Repair()
    {
        gameManager.Coin -= RepairPrice;
        gameManager.GateHP = Mathf.Clamp(gameManager.GateHP + RepairAmount, 0, gameManager.GateMaxHP);
        gameManager.UpdateStat();
        RepairBtn.GetComponent<Button>().interactable = false;
        UpdateUI();
    }

    void hideAll()
    {
        for (int i = 0; i < WeaponPic.Length; i++)
        {
            WeaponPic[i].SetActive(false);
        }
    }

    public void SelectWeapon(int i)
    {
        hideAll();
        WeaponPic[i].SetActive(true);
        SelectedWeapon = i;
        UpdateUI();
    }
    void UpdateUI()
    {
        gameManager.SaveData();
        int tempCoin = gameManager.Coin;
        CoinTxt.text = tempCoin.ToString();
        InfoShow(SelectedWeapon);

        if (SelectedWeapon < 5 && SelectedWeapon > 0)
            buyAmmoBtnUpdate(tempCoin);
        else
            RepairBtnUpdate(tempCoin);
        UpgradeBtnUpdate(tempCoin);

    }
    void UpgradeBtnUpdate(int tempCoin)
    {
        int cost = 0;
        int lv = gameManager.WeaponLv[SelectedWeapon];
        if (lv < 5)
        {
            cost = costCal();
            UpgradeCostTxt.text = cost.ToString();
            if (tempCoin < cost)
            {
                UpgradeBtn.GetComponent<Button>().interactable = false;
            }
            else
            {
                UpgradeBtn.GetComponent<Button>().interactable = true;
            }
        }
        else
        {
            UpgradeCostTxt.text = "Max";
            UpgradeBtn.GetComponent<Button>().interactable = false;
        }
    }
    int costCal()
    {
        int cost = 0;
        int lv = gameManager.WeaponLv[SelectedWeapon];
        switch (SelectedWeapon)
        {
            case 0:
                cost = KnifeLvCost[lv]; break;
            case 1:
                cost = PistolLvCost[lv]; break;
            case 2:
                cost = ShotgunLvCost[lv]; break;
            case 3:
                cost = FlameLvCost[lv]; break;
            case 4:
                cost = GLLvCost[lv]; break;
            case 5:
                cost = GateLvCost[lv]; break;
            default: break;
        }
        return cost;
    }

    void buyAmmoBtnUpdate(int tempCoin)
    {
        int cost = AmmoPrice[SelectedWeapon];
        BuyAmmoCostTxt.text = cost.ToString();
        if (tempCoin < cost || gameManager.Ammo[SelectedWeapon] == gameManager.MaxAmmo[SelectedWeapon] || gameManager.WeaponLv[SelectedWeapon] < 1)
        {
            AmmoBuyBtn.GetComponent<Button>().interactable = false;
        }
        else
        {
            AmmoBuyBtn.GetComponent<Button>().interactable = true;
        }
    }
    void RepairBtnUpdate(int tempCoin)
    {
        if (tempCoin < RepairPrice || gameManager.GateHP == gameManager.GateMaxHP)
        {
            RepairBtn.GetComponent<Button>().interactable = false;
        }
        else
        {
            RepairBtn.GetComponent<Button>().interactable = true;
        }
    }

    public void InfoShow(int i)
    {
        upgradeStarUI(gameManager.WeaponLv[SelectedWeapon]);
        hideAllinfo();
        if (i == 0)
        {
            TypeTxt.text = "Ammo";
            AmmoLeftTxt.text = "-- ";
            AmmoMaxTxt.text = "/ --";
        }
        else if (i == 5)
        {
            RepairBtn.SetActive(true);
            TypeTxt.text = "HP";
            AmmoLeftTxt.text = gameManager.GateHP.ToString() + " ";
            AmmoMaxTxt.text = "/ " + gameManager.GateMaxHP.ToString();
        }
        else
        {
            AmmoBuyBtn.SetActive(true);
            TypeTxt.text = "Ammo";
            AmmoLeftTxt.text = gameManager.Ammo[SelectedWeapon].ToString() + " ";
            AmmoMaxTxt.text = "/ " + gameManager.MaxAmmo[SelectedWeapon].ToString();
        }
    }
    void hideAllinfo()
    {
        AmmoBuyBtn.SetActive(false);
        RepairBtn.SetActive(false);
    }

    void upgradeStarUI(int s)
    {
        for (int i = 0; i < 5; i++)
        {
            Star[i].SetActive(false);
        }
        for (int i = 0; i < s; i++)
        {
            Star[i].SetActive(true);
        }
    }

    public void CloseUI()
    {
        this.gameObject.SetActive(false);
        WeaponInventory.SetActive(true);
        gameManager.UpdateStat();
    }

    public void updateCall()
    {
        UpdateUI();
    }
}
