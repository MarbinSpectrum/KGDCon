using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public class LoopGround : SingletonBehaviour<LoopGround>
{
    /** ¹Ù´Ú »ý¼º ÁöÁ¡ **/
    [SerializeField] private Transform  createPos;
    /** ¹Ù´Ú °´Ã¼ **/
    [SerializeField] private GroundObj  groundObj;
    /** ¶¥ °£°Ý **/
    [SerializeField] private float      groundSpeed;
    /** ¶¥ °´Ã¼ ±×·ì **/
    [SerializeField] private Animator   groundAni;
    /** °æ°è¼± Å©±â **/
    [SerializeField] private float      edgeWidth = 2f;

    /** ¶¥ °´Ã¼ °ü¸®¿ë ¸®½ºÆ® **/
    private List<GroundObj>             groundList  = new List<GroundObj>();
    public List<bool>                   isBreak { get; private set; } = new List<bool>();

    /** »ý¼ºµÇ´Â ¶¥ °¹¼ö **/
    private const int GROUND_CNT    = 50;
    private const int GROUND_WIDTH = 10;

    private float leftOutline;
    private float rightOutline;

    public float headX { get; private set; }
    public float tailX { get; private set; }

    public void Init()
    {
        headX = -(GameSystem.Instance.objScale * (GROUND_WIDTH - 1))/2f;
        tailX = -headX;
        leftOutline = headX;
        rightOutline = tailX;

        isBreak.Clear();
        for (int i = 0; i < GROUND_WIDTH; i++)
            isBreak.Add(false);

        while (groundList.Count < GROUND_CNT)
        {
            //¶¥ »ý¼º
            GroundObj newGround = Instantiate(groundObj, createPos.transform.position + new Vector3(0, 0, GameSystem.Instance.objScale) * groundList.Count, Quaternion.identity, groundAni.transform);
            newGround.gameObject.SetActive(true);
            newGround.Refresh(isBreak);
            groundList.Add(newGround);
        }

        for(int i = 0; i < groundList.Count; i++)
        {
            groundList[i].Refresh(isBreak);
        }

        PlayLoop();
    }



    public void BreakLeft()
    {
        for (int i = 0; i < isBreak.Count; i++)
        {
            if (isBreak[i] == true)
                continue;
            isBreak[i] = true;
            ItemMng.Instance.RemoveAllItem(i);
            leftOutline += GameSystem.Instance.objScale;
            break;
        }
        for (int i = 0; i < groundList.Count; i++)
        {
            groundList[i].Refresh(isBreak);
        }
    }

    public void BreakRight()
    {
        for (int i = isBreak.Count - 1; i >= 0; i--)
        {
            if (isBreak[i] == true)
                continue;
            isBreak[i] = true;
            ItemMng.Instance.RemoveAllItem(i);

            rightOutline -= GameSystem.Instance.objScale;
            break;
        }
        for (int i = 0; i < groundList.Count; i++)
        {
            groundList[i].Refresh(isBreak);
        }
    }

    public bool IsCanMove(Vector3 pos)
    {
        if (pos.x <= leftOutline + edgeWidth)
            return false;
        if (pos.x >= rightOutline - edgeWidth)
            return false;
        return true;
    }

    public bool IsDie(Vector3 pos)
    {
        if (pos.x <= leftOutline + edgeWidth)
            return true;
        if (pos.x >= rightOutline - edgeWidth)
            return true;
        return false;
    }

    public void StopLoop()
    {
        groundAni.speed = 0;
    }

    public void PlayLoop()
    {
        groundAni.speed = groundSpeed;
    }


}
