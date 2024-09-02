using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseUI;

    public void PauseGame()
    {
        PauseUI.SetActive(true);
        Time.timeScale = 0;
    }
    public void ContinueGame()
    {
        PauseUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
