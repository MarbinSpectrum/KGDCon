using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public class GroundObj : SerializedMonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> groundObj = new List<SpriteRenderer>();
    [SerializeField] private Sprite bodySpr;
    [SerializeField] private Sprite edgeSpr;

    private void Start()
    {
        for (int i = 0; i < groundObj.Count; i++)
        {
            float xValue = LoopGround.Instance.headX + GameSystem.Instance.objScale * i;
            groundObj[i].gameObject.transform.localPosition = new Vector3(xValue, groundObj[i].gameObject.transform.localPosition.y, groundObj[i].gameObject.transform.localPosition.z);
            groundObj[i].gameObject.transform.localScale = Vector3.one * GameSystem.Instance.objScale;
        }
    }

    public void Refresh(List<bool> isBreak)
    {
        for (int i = 0; i < groundObj.Count; i++)
        {
            groundObj[i].gameObject.SetActive(!isBreak[i]);
            if (i - 1 < 0 || (isBreak[i - 1] && isBreak[i] == false))
            {
                groundObj[i].sprite = edgeSpr;
                groundObj[i].flipX = false;
            }
            else if (i + 1 >= groundObj.Count || (isBreak[i + 1] && isBreak[i] == false))
            {
                groundObj[i].sprite = edgeSpr;
                groundObj[i].flipX = true;
            }
            else
            {
                groundObj[i].sprite = bodySpr;
                groundObj[i].flipX = false;
            }
        }
    }
}
