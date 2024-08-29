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
        base.Awake();

        _startButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            UIPlayerBoard.Instance.Initialize();
            // ���� ����
        });
        _exitButton.onClick.AddListener(() => Application.Quit());
    }
}
