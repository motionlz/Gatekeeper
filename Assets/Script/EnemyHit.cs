using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    GameObject Self;
    SpriteRenderer SR;
    Coroutine hit;
    EnemyAI ai;
    EnemyStatus stat;
    int CurrentHP, MaxHP;
    [Header("Temp")]
    public Color NormalColor;
    [Header("Resistance Setting")]
    public bool IgnoreDamage;
    public bool KnifeResistance;
    public bool PistolResistance;
    public bool ShotgunResistance;
    public bool FlameThrowerResistance;
    public bool GrenadeLauncherResistance;

    [SerializeField]
    int damageReduction;

    [Header("Skill Setting")]
    public bool ReflectSkill;
    public int ReflectDamage;
    public bool OnDeathSkill;
    public GameObject MiniSlimePrefab;
    [Header("Additional Setting")]
    public bool canHit = true;
    public GameObject dmgPop;
    public GameObject popPoint;
    void Start()
    {
        Self = this.gameObject;
        SR = Self.GetComponent<SpriteRenderer>();
        ai = Self.GetComponent<EnemyAI>();
        stat = Self.GetComponent<EnemyStatus>();
        MaxHP = stat.Health;
        CurrentHP = MaxHP;

        NormalColor = SR.color;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (canHit)
        {
            if (other.tag == "Weapon")
            {
                Hit(other.gameObject.GetComponent<MeleeWeapon>().Atk);
            }
            if (other.tag == "Bullet")
            {
                Hit(other.gameObject.GetComponent<DestroyBullet>().Atk);
            }
        }
    }

    void Hit(int dmg)
    {
        if (!IgnoreDamage)
        {
            int tempDmg = ResistanceCalculate(dmg);
            CurrentHP -= tempDmg;
            GameObject dp = Instantiate(dmgPop, popPoint.transform.position, Quaternion.identity);
            dp.GetComponent<DamagePop>().Setup(tempDmg, ResistBool());
            HpCheck();
        }
        if (ReflectSkill)
        {
            Wall.Instance.TakeDamage(ReflectDamage);
        }

        SR.color = Color.red;
        ai.Slowset(true);
        if (hit != null)
            StopCoroutine(hit);
        hit = StartCoroutine(DelayHit());
    }

    IEnumerator DelayHit()
    {
        yield return new WaitForSeconds(0.2f);
        SR.color = NormalColor;
        ai.Slowset(false);
    }

    void HpCheck()
    {
        if (CurrentHP <= 0)
        {
            if (OnDeathSkill)
            {
                OnDeath();
            }
            GameManager.Instance.CoinUpdate(5 + (WaveManager.Instance.Wave / 2));
            Destroy(this.gameObject);
        }
    }

    void OnDeath()
    {
        GameObject temp1 = Instantiate(MiniSlimePrefab, new Vector2(transform.position.x, transform.position.y + 0.1f), Quaternion.identity);
        temp1.GetComponent<EnemyStatus>().SetStat(MaxHP / 2, stat.Damage / 2);
        GameObject temp2 = Instantiate(MiniSlimePrefab, new Vector2(transform.position.x, transform.position.y - 0.1f), Quaternion.identity);
        temp2.GetComponent<EnemyStatus>().SetStat(MaxHP / 2, stat.Damage / 2);
    }

    int ResistanceCalculate(int dmg)
    {
        int weapon = WeaponStat.currentWeapon;
        switch (weapon)
        {
            case 0: if (KnifeResistance) dmg /= damageReduction; break;
            case 1: if (PistolResistance) dmg /= damageReduction; break;
            case 2: if (ShotgunResistance) dmg /= damageReduction; break;
            case 3: if (FlameThrowerResistance) dmg /= damageReduction; break;
            case 4: if (GrenadeLauncherResistance) dmg /= damageReduction; break;
            default: break;
        }
        return dmg;
    }
    bool ResistBool()
    {
        int weapon = WeaponStat.currentWeapon;
        bool result = false;
        switch (weapon)
        {
            case 0: if (KnifeResistance) result = true; break;
            case 1: if (PistolResistance) result = true; break;
            case 2: if (ShotgunResistance) result = true; break;
            case 3: if (FlameThrowerResistance) result = true; break;
            case 4: if (GrenadeLauncherResistance) result = true; break;
            default: break;
        }
        return result;
    }
}
