using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event EventHandler OnGameOver;


    [SerializeField] private bool isPlaying = false;
    [SerializeField] private float difficultyTimer;
    [SerializeField] private float difficultyTimerMax = 10f;
    [SerializeField] private float gameSpeed = 5f;
    [SerializeField] private float defaultGameSpeed = 5f;

    private enum GameState
    {
        MainMenu,
        IsPlaying,
        IsGameOver
    }
    private GameState state;

    private void Awake()
    {
        Instance = this;
    }


    void Start()
    {
        Time.timeScale = 2;
        ZumbaiManager.Instance.OnZumbaiRemoved += ZumbaiManager_OnZumbaiRemoved;
    }
    private void Update()
    {
        UpLevel();
    }

    private void ZumbaiManager_OnZumbaiRemoved(object sender, System.EventArgs e)
    {
        if(ZumbaiManager.Instance.GetZumbaiCount() == 0)
        {
            isPlaying = false;
            OnGameOver?.Invoke(this, EventArgs.Empty);
            ItemsManager.Instance.ResetItems();
        }
    }

    public void NewGame()
    {
        ZumbaiManager.Instance.NewGame();
        gameSpeed = defaultGameSpeed;
        isPlaying = true;
    }

    private void UpLevel()
    {
        difficultyTimer += Time.deltaTime;
        if (difficultyTimer > difficultyTimerMax)
        {
            difficultyTimer = 0;
            gameSpeed += 0.5f;
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
