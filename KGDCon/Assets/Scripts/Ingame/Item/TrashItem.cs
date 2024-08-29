using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class TrashItem : GameItem
{
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected List<Sprite> imgs = new List<Sprite>();

    public override void CreateObj(int idx)
    {
        pos = idx;
        isDie = false;
        hit = false;
        int rIdx = Random.Range(0, imgs.Count);
        Sprite sprite = imgs[rIdx];
        spriteRenderer.sprite = sprite;
    }

    public override void GetItem()
    {
        if (hit)
            return;
        hit = true;

        DestroyObj();

        GameSystem gameSystem = GameSystem.Instance;
        if (gameSystem == null)
            return;
        gameSystem.RemoveGround();
    }
}
