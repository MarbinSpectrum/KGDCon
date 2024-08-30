using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStartPopup : SingletonBehaviour<UIStartPopup>
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _exitButton;
    private CanvasGroup _canvasGroup;

    protected override void Awake()
    {
        Time.timeScale = 0f;

        base.Awake();

        _canvasGroup = GetComponent<CanvasGroup>();

        _startButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            Time.timeScale = 1f;
            GameSystem.Instance.run = true;
        });
        _exitButton.onClick.AddListener(() => Application.Quit());
    }

    private void Start()
    {
        Bind();
    }

    public void Bind()
    {
        Time.timeScale = 0f;
        UIPlayerBoard.Instance.Initialize();
    }

    public void Show() => _canvasGroup.alpha = 1.0f;
    public void Hide() => _canvasGroup.alpha = 0f;
}
