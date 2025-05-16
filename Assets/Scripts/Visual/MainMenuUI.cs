using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public static MainMenuUI Instance { get; private set; }

    [SerializeField] private Button playButton;

    private void Awake()
    {
        Instance = this;
        playButton.onClick.AddListener(() =>
        {
            GameManager.Instance.SetUpNewGame();
            Hide();
        });
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
