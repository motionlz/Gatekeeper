using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GunFire : MonoBehaviour
{
    GameObject Gun;
    [Header("GameObject")]
    public GameObject BulletPrefab;

    public GameObject Sight;
    [Header("Gun Setting")]
    public int AttackDamage;
    [SerializeField]
    float DelayFire;
    float cooldown;
    public int Ammo;
    [SerializeField]
    bool AutoFire;
    bool reload;
    public AmmoUpdate ammoUpdate;

    // Start is called before the first frame update
    void Start()
    {
        Gun = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch aimPoint = Input.GetTouch(0);
            Gun.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(aimPoint.position).x, Camera.main.ScreenToWorldPoint(aimPoint.position).y + 0.05f, 0);
            if (Input.touchCount > 1 && cooldown <= 0 && !reload)
            {
                if (Ammo > 0)
                {
                    Fire();
                }
            }
        }
        if (Input.touchCount != 2)
            reload = false;

        if (cooldown > 0)
            cooldown -= Time.deltaTime;


    }

    void FireBullet()
    {
        Vector2 temp = new Vector2(Sight.transform.position.x + (Random.Range(-0.05f, 0.05f)), Sight.transform.position.y + (Random.Range(-0.05f, 0.05f)));
        GameObject b = Instantiate(BulletPrefab, temp, Quaternion.identity);
        b.GetComponent<DestroyBullet>().Atk = AttackDamage;
    }

    public void Fire()
    {
        Ammo -= 1;
        cooldown = DelayFire;
        FireBullet();
        UpdateBullet();
        if (!AutoFire)
            reload = true;

    }
    public void UpdateBullet()
    {
        ammoUpdate.upAmmo(Ammo);
    }
}
