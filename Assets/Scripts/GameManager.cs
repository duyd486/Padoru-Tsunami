using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event EventHandler OnGameOver;
    public event EventHandler OnGameStart;

    [SerializeField] private bool isPlaying = false;
    [SerializeField] private float difficultyTimer;
    [SerializeField] private float difficultyTimerMax = 10f;
    [SerializeField] private float gameSpeed = 5f;
    [SerializeField] private float defaultGameSpeed = 5f;

    [SerializeField] private int scaleTimeCount = 0;
    [SerializeField] private float timeScaleUp = 0.5f;

    private void Awake()
    {
        Instance = this;
    }


    void Start()
    {
        Application.targetFrameRate = 120;
        QualitySettings.vSyncCount = 0;

        Time.timeScale = 2;
        ZumbaiManager.Instance.OnZumbaiChanged += ZumbaiManager_OnZumbaiChanged;
    }
    private void Update()
    {
        UpLevel();
    }

    private void ZumbaiManager_OnZumbaiChanged(object sender, EventArgs e)
    {
        if(ZumbaiManager.Instance.GetZumbaiCount() == 0 && isPlaying == true)
        {
            isPlaying = false;
            OnGameOver?.Invoke(this, EventArgs.Empty);
            TrapManager.Instance.ResetItems();
        }
    }

    public void SetUpNewGame()
    {
        gameSpeed = defaultGameSpeed;
        isPlaying = true;
        scaleTimeCount = 0;
        Time.timeScale = 2;
        OnGameStart?.Invoke(this, EventArgs.Empty);
    }

    private void UpLevel()
    {
        difficultyTimer += Time.deltaTime;
        if (difficultyTimer > difficultyTimerMax)
        {
            difficultyTimer = 0;
            gameSpeed += 0.25f;
            scaleTimeCount++;
            if(scaleTimeCount == 10)
            {
                Time.timeScale += 0.1f;
                scaleTimeCount = 0;
            }
        }
    }
    public bool GetIsPlaying()
    {
        return isPlaying;
    }
    public float GetGameSpeed()
    {
        return gameSpeed;
    }
}
