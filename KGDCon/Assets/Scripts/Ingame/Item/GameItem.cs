using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public class GameItem : SerializedMonoBehaviour
{
    [SerializeField] public EItem eItem;
    public int pos { protected set; get; } = -1;
    public bool hit { protected set; get; } = false;
    public bool isDie { protected set; get; } = false;

    protected virtual void Update()
    {
        if (isDie)
            return;
        MoveDown();
    }

    public virtual void CreateObj(int idx)
    {
        pos = idx;
        isDie = false;
        hit = false;

    }

    public virtual void GetItem()
    {
        if (hit)
            return;
        hit = true;
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
        float lerpValue = Mathf.Min(1, GameSystem.Instance.time / 100f);
        float addSpeed = Mathf.Lerp(0, 10, lerpValue);
        return 3f + addSpeed;
    }

    public virtual void DestroyObj()
    {
        isDie = true;
        ItemMng itemMng = ItemMng.Instance;
        itemMng.RemoveItem(eItem, this);
    }
}

