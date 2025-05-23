using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    private const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";


    private AudioSource audioSource;
    private float volume = .2f;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        //volume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, 1f);

        audioSource.volume = volume;
    }

    private void Start()
    {
        GameManager.Instance.OnGameStart += GameManager_OnGameStart;
    }

    private void GameManager_OnGameStart(object sender, System.EventArgs e)
    {
        audioSource.time = 0f;
        audioSource.Play();
        Debug.Log(audioSource);
    }

    public void ChangeVolume()
    {
        volume += .1f;
        if (volume >= 1f)
        {
            volume = 0f;
        }
        audioSource.volume = volume;

        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, volume);
        PlayerPrefs.Save();
    }
    public float GetVolume()
    {
        return volume;
    }
}
