using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    float speed;
    int atk;
    EnemyStatus Stat;
    bool slow = false;
    [Header("Attack Setting")]
    public bool oneTimeDamage;
    [SerializeField]
    float startAtkDelay;
    [SerializeField]
    float continueAtkDelay;
    public GameObject RangeAttackPrefab;
    public GameObject RangeSpawnPoint;
    [Header("Move Setting")]
    [SerializeField]
    bool stopmove = false;
    public bool DelayMove;
    public bool RangeAttacker;
    public float startmoveTime;
    public float DestroyTime;
    // Start is called before the first frame update
    void Start()
    {
        Stat = this.gameObject.GetComponent<EnemyStatus>();
        speed = Stat.Speed;
        atk = Stat.Damage;
        if (DelayMove)
        {
            StartCoroutine(startmove(startmoveTime));
        }
        if (DestroyTime > 0)
        {
            StartCoroutine(SelfDelete(DestroyTime));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopmove)
        {
            float s = slow ? speed / 2 : speed;
            transform.Translate(Vector2.left * s * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Wall")
        {
            stopmove = true;
            if (oneTimeDamage)
            {
                AttackDoor();
                Destroy(this.gameObject);
            }
            else
                InvokeRepeating("AttackDoor", continueAtkDelay, startAtkDelay);
        }
        if (RangeAttacker && other.tag == "RangeStop")
        {
            stopmove = true;
            InvokeRepeating("RangeAttack", continueAtkDelay, startAtkDelay);
        }
    }

    void AttackDoor()
    {
        Wall.Instance.TakeDamage(atk);
    }
    public void Slowset(bool b)
    {
        slow = b;
    }
    void RangeAttack()
    {
        GameObject Ra = Instantiate(RangeAttackPrefab, RangeSpawnPoint.transform.position, Quaternion.identity);
        Ra.GetComponent<EnemyStatus>().SetStat(Stat.Health / 10, atk);
    }

    IEnumerator startmove(float t)
    {
        yield return new WaitForSeconds(t);
        stopmove = false;
        this.gameObject.GetComponent<EnemyHit>().canHit = true;
    }

    IEnumerator SelfDelete(float t)
    {
        yield return new WaitForSeconds(t);
        Destroy(this.gameObject);
    }
}
