using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameTimeManage : MonoBehaviour
{
    public static bool IsPaused = false;
    public GameObject PanelPause;
    public GameObject PanelLevelUp;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

    }

    public void Resume()
    {
        PanelPause.SetActive(false);
        PanelLevelUp.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }

    public void Pause()
    {
        PanelPause.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
    }
}
