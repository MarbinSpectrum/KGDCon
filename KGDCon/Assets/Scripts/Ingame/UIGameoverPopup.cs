using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameoverPopup : SingletonBehaviour<UIGameoverPopup>
{
    [SerializeField] private Text _bestScoreText;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _pressAnyKeyText;

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            UIStartPopup.Instance.Bind();
            gameObject.SetActive(false);
        }
    }

    public void Bind(int score)
    {
        gameObject.SetActive(true);
        PlayerPrefs.SetInt("BestScore", score);
        _bestScoreText.text = $"최고 점수: {PlayerPrefs.GetInt("BestScore", 0).WithComma()}";
        _scoreText.text = $"현재 점수: {score.WithComma()}";
    }
}
