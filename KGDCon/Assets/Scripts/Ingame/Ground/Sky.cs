using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sky : SingletonBehaviour<Sky>
{
    [SerializeField] private Sprite sky0;
    [SerializeField] private Sprite sky1;
    [SerializeField] private Sprite sky2;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public void UpdateSky()
    {
        if (UIPlayerBoard.Instance.LifeCount <= 1)
        {
            spriteRenderer.sprite = sky2;
        }
        else if (UIPlayerBoard.Instance.LifeCount <= 2)
        {
            spriteRenderer.sprite = sky1;
        }
        else
        {
            spriteRenderer.sprite = sky0;
        }
    }
}
