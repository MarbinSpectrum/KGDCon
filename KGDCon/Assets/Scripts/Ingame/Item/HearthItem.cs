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

        UIPlayerBoard uIPlayerBoard = UIPlayerBoard.Instance;
        if (uIPlayerBoard == null)
            return;
        uIPlayerBoard.IncreaseHalfLife();
    }
}
