using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AmmoUpdate : MonoBehaviour
{
    public Text AmmoTxt;
    // Start is called before the first frame update
    public void upAmmo(int a)
    {
        AmmoTxt.text = a.ToString();
    }

    public void hideAmmo()
    {
        AmmoTxt.text = "";
    }
}
