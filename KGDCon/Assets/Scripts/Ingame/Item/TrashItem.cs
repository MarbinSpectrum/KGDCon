using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class TrashItem : GameItem
{
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected List<Sprite> imgs = new List<Sprite>();

    protected override void CreateObj(int idx)
    {
        pos = idx;

        int rIdx = Random.Range(0, imgs.Count);
        Sprite sprite = imgs[rIdx];
        spriteRenderer.sprite = sprite;
    }

    protected override void GetItem()
    {
        DestroyObj();
    }
}
