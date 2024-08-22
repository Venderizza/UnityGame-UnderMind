using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void SceneChange(int SceneNumber)
    {
        SceneManager.LoadScene(SceneNumber);
        Time.timeScale = 1f;
    }
}
