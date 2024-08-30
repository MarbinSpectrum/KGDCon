using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendItem : GameItem
{
    [SerializeField] private int score = 500;

    public override void GetItem()
    {
        if (hit)
            return;
        hit = true;

        DestroyObj();

        Sfx.Instance.Play(ESfx.GetItem);
        GameSystem.Instance.AddScore(score);
    }
}
