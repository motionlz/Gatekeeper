using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wall : MonoBehaviour
{
    // Start is called before the first frame update
    public static Wall Instance;
    SpriteRenderer sprite;
    public Color normal;
    Coroutine Hit;
    public int MaxHP;
    public int CurrentHP;
    public Slider HPBar;
    public GameObject OverUI;
    public GameObject Inventory;
    void Start()
    {
        Instance = this;
        sprite = this.gameObject.GetComponent<SpriteRenderer>();
    }
    public void TakeDamage(int dmg)
    {
        sprite.color = Color.red;
        CurrentHP -= dmg;
        UpdateUI();
        if (CurrentHP <= 0)
        {
            Gameover();
        }
        if (Hit != null)
            StopCoroutine(Hit);
        Hit = StartCoroutine(ColorDelay());
    }
    IEnumerator ColorDelay()
    {
        yield return new WaitForSeconds(0.3f);
        sprite.color = normal;
    }

    public void UpdateUI()
    {
        HPBar.maxValue = MaxHP;
        HPBar.value = CurrentHP;
    }

    void Gameover()
    {
        OverUI.SetActive(true);
        WaveManager.Instance.gameObject.GetComponent<Spawner>().CancelInvoke();
        CurrentHP = 1;
        WaveManager.Instance.SaveData();
        Inventory.SetActive(false);
    }
}
