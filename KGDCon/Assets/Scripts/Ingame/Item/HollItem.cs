using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HollItem : GameItem
{
    protected override void GetItem()
    {
        DestroyObj();
    }
}
