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

        DestroyObj();

        UIPlayerBoard uIPlayerBoard = UIPlayerBoard.Instance;
        if (uIPlayerBoard == null)
            return;
        uIPlayerBoard.DecreaseLife();
        if(uIPlayerBoard.IsGameover)
        {
            UIGameoverPopup.Instance.Bind(uIPlayerBoard.Score);
        }
    }
}
