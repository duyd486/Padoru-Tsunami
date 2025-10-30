using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public event EventHandler OnCandyChanged;

    [SerializeField] private int currentZumbai = 0;
    [SerializeField] private int totalZumbai = 0;
    [SerializeField] private int currentCandy = 0;
    [SerializeField] private float totalTime = 0;
    [SerializeField] private float startTime;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ZumbaiManager.Instance.OnZumbaiChanged += ZumbaiManager_OnZumbaiChanged;
        GameManager.Instance.OnGameStart += GameManager_OnGameStart;
    }

    private void GameManager_OnGameStart(object sender, System.EventArgs e)
    {
        currentZumbai = 1;
        totalZumbai = 1;
        currentCandy = 0;
        totalTime = 0;
        startTime = Time.realtimeSinceStartup;
        OnCandyChanged?.Invoke(this, EventArgs.Empty);
    }

    private void Update()
    {
        totalTime = Time.realtimeSinceStartup - startTime;
    }

    private void ZumbaiManager_OnZumbaiChanged(object sender, System.EventArgs e)
    {
        if(currentZumbai <= ZumbaiManager.Instance.GetZumbaiCount())
        {
            totalZumbai++;
        }
        currentZumbai = ZumbaiManager.Instance.GetZumbaiCount();
    }

    public void AddCandy()
    {
        currentCandy++;
        OnCandyChanged?.Invoke(this, EventArgs.Empty);
    }
    public int GetCurrentZumbai()
    {
        return currentZumbai;
    }
    public int GetCurrentCandy() { 
        return currentCandy; 
    }
    public int GetTotalZumbai() { 
        return totalZumbai; 
    }
    public float GetTotalTime()
    {
        return totalTime;
    }
}
