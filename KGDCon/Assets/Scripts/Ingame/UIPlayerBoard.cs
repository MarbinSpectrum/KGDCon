using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerBoard : UI
{
    public int Score
    {
        get => _score;
        set
        {
            _score = Mathf.Max(value, 0);
            UpdateScore(_score);
        }
    }
    public bool IsGameover => _lifeIndex >= 2 * _lives.Length;
    public bool IsFullLife => _lifeIndex == 0;
    public float LifeCount => (2f * _lives.Length - _lifeIndex) / 2;

    [SerializeField] private Text _scoreText;
    [SerializeField] private Image[] _lives;

    private int _score;
    private int _lifeIndex;

    private void Update()
    {
        var bgm = Bgm.Instance;

        if (IsGameover)
            bgm.Stop();
        else if (LifeCount <= 1)
            bgm.Play(EBgm.DangerBgm);
        else
            bgm.Play(EBgm.MainBgm);
    }

    public void Initialize()
    {
        Score = 0;
        _lifeIndex = 0;
        UpdateScore(0);
        for (int i = 0; i < _lives.Length; ++i)
            _lives[i].fillAmount = 1f;
    }

    private void UpdateScore(int newScore) =>
        _scoreText.text = $"Á¡¼ö: {newScore:#,##0}";

    public void IncreaseHalfLife()
    {
        if (_lifeIndex <= 0)
            return;
        _lives[--_lifeIndex / 2].fillAmount += 0.5f;
    }

    public void DecreaseLife()
    {
        DecreaseHalfLife();
        DecreaseHalfLife();
    }

    public void DecreaseHalfLife()
    {
        if (IsGameover)
            return;
        _lives[_lifeIndex++ / 2].fillAmount -= 0.5f;
    }
}
