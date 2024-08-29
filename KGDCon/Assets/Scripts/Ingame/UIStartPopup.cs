using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStartPopup : SingletonBehaviour<UIStartPopup>
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _exitButton;

    protected override void Awake()
    {
        Time.timeScale = 0f;

        base.Awake();

        _startButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            UIPlayerBoard.Instance.Initialize();
            Time.timeScale = 1f;
        });
        _exitButton.onClick.AddListener(() => Application.Quit());
    }

    public void Bind()
    {
        Time.timeScale = 0f;
    }
}
