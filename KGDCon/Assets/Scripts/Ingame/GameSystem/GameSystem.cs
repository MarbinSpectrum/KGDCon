using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public class GameSystem : SingletonBehaviour<GameSystem>
{
    private Dictionary<EItem, float> spawnCheckTime = new Dictionary<EItem, float>();
    private HashSet<int> hasSlot = new HashSet<int>();
    [SerializeField] private LoopGround loopGround;

    [SerializeField] private float trashTime    = 0;
    [SerializeField] private float hollTime1    = 0;
    [SerializeField] private float hollTime2    = 0;
    [SerializeField] private float friendTime   = 0;
    [SerializeField] private int humanPoint = 0;
    [SerializeField] private int hearthPoint = 0;
    private int humanPointFlag = 0;
    private int hearthPointFlag = 0;
    public bool run;
    private bool groundFlag = false;

    private void Update()
    {
        if (run == false)
            return;

        ItemMng itemMng = ItemMng.Instance;

        hasSlot.Clear();
        for (EItem item = EItem.Holl_1; item < EItem.MAX; item++)
        {
            if (CheckSpawnCondi(item) == false)
                continue;

            //오브젝트 생성
            GameItem gameItem = null;
            int getPos = -1;
            switch (item)
            {
                case EItem.Holl_2:
                    {
                        getPos = GetCreatePos(2);
                        if (getPos == -1)
                            continue;
                        hasSlot.Add(getPos);
                        hasSlot.Add(getPos + 1);
                    }
                    break;
                default:
                    {
                        getPos = GetCreatePos(1);
                        if (getPos == -1)
                            continue;
                        hasSlot.Add(getPos);
                    }
                    break;
            }
            gameItem = itemMng.CreateItem(item);
            gameItem.CreateObj(getPos);
            gameItem.transform.position = new Vector3(-4.5f + getPos, 0.01f, 15);
        }
    }

    private bool CheckSpawnCondi(EItem item)
    {
        //오브젝트마다 생성 조건
        int lineCnt = 0;
        for(int i = 0; i < loopGround.isBreak.Count; i++)
        {
            if (loopGround.isBreak[i])
                continue;
            lineCnt++;
        }
        if (spawnCheckTime.ContainsKey(item) == false)
            spawnCheckTime[item] = 0;
        spawnCheckTime[item] += Time.deltaTime;

        switch (item)
        {
            case EItem.Trash:
                if (spawnCheckTime[item] > trashTime)
                {
                    spawnCheckTime[item] = 0;
                    return true;
                }
                break;
            case EItem.Holl_1:
                if (spawnCheckTime[item] > hollTime1 && lineCnt <= 2)
                {
                    spawnCheckTime[item] = 0;
                    return true;
                }
                break;
            case EItem.Holl_2:
                if (spawnCheckTime[item] > hollTime2 && lineCnt >= 3)
                {
                    spawnCheckTime[item] = 0;
                    return true;
                }
                break;
            case EItem.Human:
                if (humanPointFlag >= humanPoint)
                {
                    humanPointFlag = 0;
                    return true;
                }
                break;
            case EItem.friend:
                if (spawnCheckTime[item] > friendTime)
                {
                    spawnCheckTime[item] = 0;
                    return true;
                }
                break;
            case EItem.Hearth:
                if (hearthPointFlag >= hearthPoint && UIPlayerBoard.Instance.IsFullLife == false)
                {
                    hearthPointFlag = 0;
                    return true;
                }
                break;
        }
        return false;
    }

    private int GetCreatePos(int width)
    {
        //생성위치 반환
        List<int> posList = new List<int>();
        for(int i = 0; i < loopGround.isBreak.Count; i++)
        {
            if (CanSet(i, width) == false)
                continue;
            if (hasSlot.Contains(i))
                continue;
            posList.Add(i);
        }

        if (posList.Count == 0)
            return -1;

        int rIdx = Random.Range(0, posList.Count);
        return posList[rIdx];
    }

    private bool CanSet(int idx, int width)
    {
        //해당 위치에 공간을 할당할 수 잇는지 고려
        for(int i = idx; i < idx + width; i++)
        {
            if (loopGround.isBreak.Count <= i)
                return false;
            if (loopGround.isBreak[i])
                return false;
        }
        return true;
    }

    public void RemoveGround()
    {
        if (groundFlag)
            loopGround.BreakRight();
        else
            loopGround.BreakLeft();
        groundFlag = !groundFlag;
    }

    public void AddScore(int score)
    {
        UIPlayerBoard uIPlayerBoard = UIPlayerBoard.Instance;
        if (uIPlayerBoard == null)
            return;
        uIPlayerBoard.Score += score;
        humanPointFlag += score;
        hearthPointFlag += score;
    }
}
