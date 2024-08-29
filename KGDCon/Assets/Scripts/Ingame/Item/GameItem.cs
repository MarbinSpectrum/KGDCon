using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public class GameItem : SerializedMonoBehaviour
{
    [SerializeField] protected EItem eItem;
    protected int pos = -1;

    protected bool isDie = false;

    protected virtual void Update()
    {
        if (isDie)
            return;
        MoveDown();
    }

    protected virtual void CreateObj(int idx)
    {
        pos = idx;
    }

    protected virtual void GetItem()
    {
        DestroyObj();
    }

    protected virtual void MoveDown()
    {
        transform.position += new Vector3(0, 0, -1) * ItemSpeed() * Time.deltaTime;
        if(transform.position.z < -20)
            DestroyObj();
    }

    protected virtual float ItemSpeed()
    {
        return 1f;
    }

    protected virtual void DestroyObj()
    {
        isDie = true;
        ItemMng itemMng = ItemMng.Instance;
        itemMng.RemoveItem(eItem, this);
    }
}

