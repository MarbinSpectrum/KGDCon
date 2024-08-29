using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanItem : GameItem
{
    public override void GetItem()
    {
        DestroyObj();
    }
}
