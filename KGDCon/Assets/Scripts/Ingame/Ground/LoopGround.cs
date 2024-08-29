using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public class LoopGround : SerializedMonoBehaviour
{
    /** �ٴ� ���� ���� **/
    [SerializeField] private Transform  createPos;
    /** �ٴ� ��ü **/
    [SerializeField] private GroundObj  groundObj;
    /** �� ���� **/
    [SerializeField] private float      groundDis;
    /** �� ���� **/
    [SerializeField] private float      groundSpeed;
    /** �� ��ü �׷� **/
    [SerializeField] private Animator   groundAni;
    /** ��輱 ũ�� **/
    [SerializeField] private float      edgeWidth = 2f;

    /** �� ��ü ������ ����Ʈ **/
    private List<GroundObj>             groundList  = new List<GroundObj>();
    public List<bool>                   isBreak { get; private set; } = new List<bool>();

    /** �����Ǵ� �� ���� **/
    private const int GROUND_CNT    = 50;
    private const int GROUND_WIDTH = 10;

    private float headX = -5.5f;
    private float tailX = 5.5f;

    private void Awake()
    {
        Init();
    }


    private void Init()
    {
        for(int i = 0; i < GROUND_CNT; i++)
        {
            //�� ����
            GroundObj newGround = Instantiate(groundObj, createPos.transform.position + new Vector3(0, 0, groundDis) * i, Quaternion.identity, groundAni.transform);
            newGround.gameObject.SetActive(true);
            groundList.Add(newGround);
        }

        for (int i = 0; i < GROUND_WIDTH; i++)
            isBreak.Add(false);
        groundAni.speed = groundSpeed;
    }

    public void BreakLeft()
    {
        for (int i = 0; i < isBreak.Count; i++)
        {
            if (isBreak[i] == true)
                continue;
            isBreak[i] = true;
            headX++;
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
            tailX--;
            break;
        }
        for (int i = 0; i < groundList.Count; i++)
        {
            groundList[i].Refresh(isBreak);
        }
    }

    public bool IsCanMove(Vector3 pos)
    {
        if (pos.x <= headX + edgeWidth)
            return false;
        if (pos.x >= tailX - edgeWidth)
            return false;
        return true;
    }

    public bool IsDie(Vector3 pos)
    {
        if (pos.x <= headX + edgeWidth)
            return true;
        if (pos.x >= tailX - edgeWidth)
            return true;
        return false;
    }

}
