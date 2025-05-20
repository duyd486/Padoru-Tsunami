using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private TextMeshProUGUI hatText;
    [SerializeField] private TextMeshProUGUI candyText;
    [SerializeField] private TextMeshProUGUI timeText;



    private void Awake()
    {
        restartButton.onClick.AddListener(() =>
        {
            GameManager.Instance.SetUpNewGame();
            Hide();
        });
        mainMenuButton.onClick.AddListener(() =>
        {
            Hide();
            MainMenuUI.Instance.Show();
        });
    }

    private void Start()
    {
        Hide();
        GameManager.Instance.OnGameOver += GameManager_OnGameOver;
    }

    private void GameManager_OnGameOver(object sender, System.EventArgs e)
    {
        hatText.text = ScoreManager.Instance.GetTotalZumbai().ToString();
        candyText.text = ScoreManager.Instance.GetCurrentCandy().ToString();
        timeText.text = ScoreManager.Instance.GetTotalTime().ToString();
        Show();
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
