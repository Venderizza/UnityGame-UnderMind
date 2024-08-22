using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class gameInterface : MonoBehaviour
{
    [SerializeField] private TMP_Text score;
    [SerializeField] private TMP_Text HP;
    [SerializeField] private TMP_Text bullets;
    [SerializeField] private TMP_Text armor;
    [SerializeField] private TMP_Text time;

    [SerializeField] private GameObject FirstHP;
    [SerializeField] private GameObject FirstBullet;
    [SerializeField] private GameObject FirstArmor;

    [SerializeField] private GameObject Enemy0Panel;
    [SerializeField] private GameObject Enemy1Panel;
    [SerializeField] private GameObject Enemy2Panel;

    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private GameObject OnLoadPanel;

    private int Score;
    private int gameTime_sec;
    private int gameTime_min;
    [SerializeField] private int delta;

    private void OnEnable()
    {
        EventManager.onChangeNumbOfBullets += ChangeBullets;
        EventManager.onChangePlayerHealth += ChangeHealth;
        EventManager.onKilledEnemy += ChangeScore;
        EventManager.onSetArmor += ChangeArmor;

        EventManager.onFirstPickable += ShowPanel;

        EventManager.onFirstEnemy0 += ShowEnemy0Panel;
        EventManager.onFirstEnemy1 += ShowEnemy1Panel;
        EventManager.onFirstEnemy2 += ShowEnemy2Panel;

        EventManager.onPlayerDeath += ShowGameOverPanel;

    }
    private void OnDisable()
    {
        EventManager.onChangeNumbOfBullets -= ChangeBullets;
        EventManager.onChangePlayerHealth -= ChangeHealth;
        EventManager.onKilledEnemy -= ChangeScore;
        EventManager.onSetArmor -= ChangeArmor;

        EventManager.onPlayerDeath -= ShowGameOverPanel;
    }

    private void Start()
    {
        StartCoroutine(Timer());
        Destroy(OnLoadPanel, 6);
    }

    void ChangeScore(int value)
    {
        Score += value;
        score.text = $"{Score}";
    }
    void ChangeHealth(int value)
    {
        HP.text = $"{value}";
    }
    void ChangeBullets(int value)
    {
        bullets.text = $"{value}";
    }
    void ChangeArmor(int value)
    {
        armor.text = $"{value}";
    }

    //____________________________________//

    void ShowPanel(string tag)
    {
        if (tag == "pickHP" && FirstHP != null)
        {
            FirstHP.gameObject.SetActive(true);
            Destroy(FirstHP.gameObject, 7);
        }
        else if (tag == "pickArmor" && FirstArmor != null)
        {
            FirstArmor.gameObject.SetActive(true);
            Destroy(FirstArmor.gameObject, 7);
        }
        else if (tag == "pickBullet" && FirstBullet != null)
        {
            FirstBullet.gameObject.SetActive(true);
            Destroy(FirstBullet.gameObject, 7);
        }
        else if (FirstHP == null && FirstArmor == null && FirstBullet == null) EventManager.onFirstPickable -= ShowPanel;
    }

    //____________________________________//

    void ShowEnemy0Panel()
    {
        if (Enemy0Panel != null)
        {
            Enemy0Panel.gameObject.SetActive(true);

            EventManager.onFirstEnemy0 -= ShowEnemy0Panel;
            Destroy(Enemy0Panel, 7);
        }
    }
    void ShowEnemy1Panel()
    {
        if (Enemy1Panel != null)
        {
            Enemy1Panel.gameObject.SetActive(true);

            EventManager.onFirstEnemy1 -= ShowEnemy1Panel;
            Destroy(Enemy1Panel, 7);
        }
    }
    void ShowEnemy2Panel()
    {
        if (Enemy2Panel != null)
        {
            Enemy2Panel.gameObject.SetActive(true);

            EventManager.onFirstEnemy2 -= ShowEnemy2Panel;
            Destroy(Enemy2Panel, 7);
        }
    }

    //_____________________________________//

    void ShowGameOverPanel()
    {
        if (PlayerPrefs.HasKey("BestScore") && PlayerPrefs.GetInt("BestScore") < Score)
        {
            PlayerPrefs.SetInt("BestScore", Score);
        }
        else if (!PlayerPrefs.HasKey("BestScore")) PlayerPrefs.SetInt("BestScore", Score);
        PlayerPrefs.Save();

        GameOverPanel.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    IEnumerator Timer()
    {
        while (true)
        {
            if (gameTime_sec == 59)
            {
                gameTime_min++;
                gameTime_sec = -1;
            }
            gameTime_sec += delta;
            time.text = $"{gameTime_min.ToString("D2")} : {gameTime_sec.ToString("D2")}";
            yield return new WaitForSeconds(1);
        }
    }
}
