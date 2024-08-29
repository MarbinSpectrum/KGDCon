using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearthItem : GameItem
{
    protected override void GetItem()
    {
        DestroyObj();
    }
}
