using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    float time;
    public int Atk;
    public bool RandomRotaion = false;
    void Start()
    {
        if (RandomRotaion)
        {
            var euler = transform.eulerAngles;
            euler.z = Random.Range(0.0f, 360.0f);
            transform.eulerAngles = euler;
        }
        StartCoroutine(Destroy(time));
    }
    IEnumerator Destroy(float t)
    {
        yield return new WaitForSeconds(t);
        Destroy(this.gameObject);
    }
}
