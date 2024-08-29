using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanItem : GameItem
{
    protected override void GetItem()
    {
        DestroyObj();
    }
}
