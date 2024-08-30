using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStartPopup : UI
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _exitButton;

    private void Awake()
    {
        Time.timeScale = 0f;

        _startButton.onClick.AddListener(() =>
        {
            Sfx.Instance.PlayButtonClick();
            Hide();
            Time.timeScale = 1f;
            GameSystem.Instance.run = true;
        });

        _exitButton.onClick.AddListener(() =>
        {
            Sfx.Instance.PlayButtonClick();
            Application.Quit();
        });
    }

    private void Start()
    {
        Bind();
        Bgm.Instance.Play(EBgm.MainBgm);
    }

    public void Bind()
    {
        Time.timeScale = 0f;
        Canvas.Instance.Get<UIPlayerBoard>().Initialize();
    }
}
