using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    GameObject Melee;
    [SerializeField]
    Collider2D col;
    TrailRenderer Tr;
    public int Atk;
    // Start is called before the first frame update
    void Start()
    {
        Melee = this.gameObject;
        col = Melee.GetComponent<Collider2D>();
        Tr = Melee.GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);
            Melee.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(t.position).x, Camera.main.ScreenToWorldPoint(t.position).y, 0);
            col.enabled = true;
            Tr.enabled = true;
        }
        else
        {
            col.enabled = false;
            Tr.enabled = false;
        }

    }
}
