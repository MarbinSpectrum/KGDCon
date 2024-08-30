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

        UIPlayerBoard uIPlayerBoard = Canvas.Instance.Get<UIPlayerBoard>();
        if (uIPlayerBoard == null)
            return;
        uIPlayerBoard.DecreaseLife();
        PlayerUnit.Instance.HitEvent();
        Debug.Log("¿Œ∞£");
        Sky.Instance.UpdateSky();
        if (uIPlayerBoard.IsGameover)
        {
            Canvas.Instance.Get<UIGameoverPopup>().Bind(uIPlayerBoard.Score);
        }
    }
}
