using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanItem : GameItem
{
    public override void GetItem()
    {
        if (hit)
            return;
        hit = true;

        Sfx.Instance.Play(ESfx.HitHuman);
        DestroyObj();

        UIPlayerBoard uIPlayerBoard = UIPlayerBoard.Instance;
        if (uIPlayerBoard == null)
            return;
        uIPlayerBoard.DecreaseLife();
        PlayerUnit.Instance.HitEvent();
        Sky.Instance.UpdateSky();
        if (uIPlayerBoard.IsGameover)
        {
            UIGameoverPopup.Instance.Bind(uIPlayerBoard.Score);
        }
    }
}
