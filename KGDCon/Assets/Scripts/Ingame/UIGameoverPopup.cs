using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameoverPopup : UI
{
    [SerializeField] private Text _bestScoreText;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _pressAnyKeyText;

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            Sfx.Instance.PlayButtonClick();
            Canvas.Instance.Get<UIStartPopup>().Bind();
            Time.timeScale = 1f;
            Hide();

            GameSystem.Instance.StartGame();
        }
    }

    public void Bind(int score)
    {
        Show();
        Bgm.Instance.Stop();
        Sfx.Instance.Play(ESfx.Gameover);
        PlayerPrefs.SetInt("BestScore", score);
        _bestScoreText.text = $"최고 점수: {PlayerPrefs.GetInt("BestScore", 0).WithComma()}";
        _scoreText.text = $"현재 점수: {score.WithComma()}";
    }
}
