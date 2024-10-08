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

        UIPlayerBoard uIPlayerBoard = Canvas.Instance.Get<UIPlayerBoard>();
        if (uIPlayerBoard == null)
            return;
        uIPlayerBoard.IncreaseHalfLife();
        Sky.Instance.UpdateSky();

        EffScripts eff = EffMng.Instance.CreateItem(EEffect.Hearth);
        eff.transform.position = PlayerUnit.Instance.transform.position + new Vector3(0, 0.6f, 0);
    }
}
