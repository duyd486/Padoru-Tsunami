using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;


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
