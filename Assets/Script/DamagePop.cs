using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePop : MonoBehaviour
{
    public TextMesh textMesh;
    string Resisttext = "Resist\n";
    public float dissappearTime;
    Rigidbody2D Rb;
    // Start is called before the first frame update
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Rb.AddForce(Vector2.up * 100);
        Rb.AddForce(Vector2.left * Random.Range(-50, 50));
        StartCoroutine(Destroy(dissappearTime));
    }

    public void Setup(int dmg, bool Resist)
    {
        textMesh.text = (Resist ? Resisttext : "") + dmg.ToString();
    }

    IEnumerator Destroy(float t)
    {
        yield return new WaitForSeconds(t);
        Destroy(this.gameObject);
    }
}
