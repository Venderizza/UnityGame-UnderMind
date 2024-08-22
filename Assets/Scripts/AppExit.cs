using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppExit : MonoBehaviour
{
    public void ExitGame()
    {
        Application.Quit();
        PlayerPrefs.Save();
    }
}
