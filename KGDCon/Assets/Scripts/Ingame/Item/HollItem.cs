using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HollItem : GameItem
{
    public override void GetItem()
    {
        if (hit)
            return;
        hit = true;

        Sfx.Instance.Play(ESfx.OnFallIntoHole);
        UIPlayerBoard uIPlayerBoard = Canvas.Instance.Get<UIPlayerBoard>();
        if (uIPlayerBoard == null)
            return;
        uIPlayerBoard.DecreaseHalfLife();
        PlayerUnit.Instance.HitEvent();
        Sky.Instance.UpdateSky();
        if (uIPlayerBoard.IsGameover)
        {
            Canvas.Instance.Get<UIGameoverPopup>().Bind(uIPlayerBoard.Score);
        }
    }
}
