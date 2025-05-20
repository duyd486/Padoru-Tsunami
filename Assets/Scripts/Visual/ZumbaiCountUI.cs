using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ZumbaiCountUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentZumbaiCountText;
    [SerializeField] private TextMeshProUGUI totalZumbaiCountText;
    [SerializeField] private TextMeshProUGUI candyCountText;



    private void Start()
    {
        currentZumbaiCountText.text = "X 0";
        ZumbaiManager.Instance.OnZumbaiChanged += ZumbaiManager_OnZumbaiChanged;
        GameManager.Instance.OnGameStart += GameManager_OnGameStart;
        GameManager.Instance.OnGameOver += GameManager_OnGameOver;
        Hide();
    }

    private void GameManager_OnGameOver(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void GameManager_OnGameStart(object sender, System.EventArgs e)
    {
        Show();
        UpdateVisual();
    }

    private void ZumbaiManager_OnZumbaiChanged(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        currentZumbaiCountText.text = "X " + ScoreManager.Instance.GetCurrentZumbai().ToString();
        totalZumbaiCountText.text = ScoreManager.Instance.GetTotalZumbai().ToString();
        candyCountText.text = ScoreManager.Instance.GetCurrentCandy().ToString();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
