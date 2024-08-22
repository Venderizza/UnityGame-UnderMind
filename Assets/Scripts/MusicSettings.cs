using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MusicSettings : MonoBehaviour
{
    //_________________________SLIDER оепелеммше: MUSIC__________________________//
    [SerializeField] private Slider MusicSlider;
    [SerializeField] private AudioSource Music;
    private float MusicLevel;

    //_________________________SLIDER оепелеммше: SOUNDS__________________________//
    [SerializeField] private Slider SoundSlider;
    [SerializeField] private AudioSource[] Sounds;
    private float SoundLevel;

    [SerializeField] private TMP_Text score;
    private int BestScore;

    private void Start()
    {
        if (PlayerPrefs.HasKey("MusicLevel"))
        {
            MusicLevel = PlayerPrefs.GetFloat("MusicLevel");
            MusicSlider.value = MusicLevel;

        }

        if (PlayerPrefs.HasKey("SoundLevel"))
        {
            SoundLevel = PlayerPrefs.GetFloat("SoundLevel");
            SoundSlider.value = SoundLevel;

        }
        if (PlayerPrefs.HasKey("BestScore"))
        {
            BestScore = PlayerPrefs.GetInt("BestScore");
            score.text = BestScore.ToString();
        }

    }

    private void Update()
    {
        UpdateMusicSettings();
    }

    public void SaveMusicSettings()
    {
        PlayerPrefs.SetFloat("MusicLevel", MusicLevel);
        PlayerPrefs.SetFloat("SoundLevel", SoundLevel);
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            SaveMusicSettings();
        }
    }

    public void UpdateMusicSettings()
    {
        Music.volume = MusicSlider.value;
        MusicLevel = MusicSlider.value;


        for (int i = 0; i < Sounds.Length; i++)
        {
            Sounds[i].volume = SoundSlider.value;
            SoundLevel = SoundSlider.value;
        }

        SaveMusicSettings();

    }
}
