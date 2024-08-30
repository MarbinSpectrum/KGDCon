using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearthItem : GameItem
{
    public override void GetItem()
    {
        if (hit)
            return;
        hit = true;

        DestroyObj();
        Sfx.Instance.Play(ESfx.GetItem);

        UIPlayerBoard uIPlayerBoard = UIPlayerBoard.Instance;
        if (uIPlayerBoard == null)
            return;
        uIPlayerBoard.IncreaseHalfLife();
        Sky.Instance.UpdateSky();
    }
}
