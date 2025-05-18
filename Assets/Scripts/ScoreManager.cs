using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] private int currentZumbai = 0;
    [SerializeField] private int totalZumbai = 0;
    [SerializeField] private int currentCandy = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ZumbaiManager.Instance.OnZumbaiAdded += ZumbaiManager_OnZumbaiAdded;
        ZumbaiManager.Instance.OnZumbaiRemoved += ZumbaiManager_OnZumbaiRemoved;
    }

    private void ZumbaiManager_OnZumbaiRemoved(object sender, System.EventArgs e)
    {
        currentZumbai--;
    }

    private void ZumbaiManager_OnZumbaiAdded(object sender, System.EventArgs e)
    {
        currentZumbai++;
        totalZumbai++;
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
}
