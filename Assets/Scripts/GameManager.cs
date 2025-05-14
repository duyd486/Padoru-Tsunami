using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private bool isPlaying = false;
    [SerializeField] private float difficultyTimer;
    [SerializeField] private float difficultyTimerMax = 10f;
    [SerializeField] private float gameSpeed = 5f;

    public enum GameState
    {
        MainMenu,
        IsPlaying,
        IsPause,
        IsOver
    }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Time.timeScale = 2;
    }

    private void Update()
    {
        difficultyTimer += Time.deltaTime;
        if(difficultyTimer > difficultyTimerMax)
        {
            difficultyTimer = 0;
            UpLevel();
        }
    }

    private void UpLevel()
    {
        gameSpeed += 0.5f;
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
