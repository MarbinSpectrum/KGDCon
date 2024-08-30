using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public class GameSystem : SingletonBehaviour<GameSystem>
{
    private Dictionary<EItem, float> spawnCheckTime = new Dictionary<EItem, float>();

    /** �����Ⱑ ������ �ֱ� **/
    [SerializeField] private float trashTime = 0;
    /** ������ ������ �ֱ� **/
    [SerializeField] private float hollTime1 = 0;
    /** 2ĭ ¥�� ������ ������ �ֱ� **/
    [SerializeField] private float hollTime2 = 0;
    /** ü���� ������ ���� **/
    [SerializeField] private float friendTime = 0;
    /** �ΰ��� ������ ���� **/
    [SerializeField] private int humanPoint = 0;
    /** ü���� ������ ���� **/
    [SerializeField] private int hearthPoint = 0;

    /** ������Ʈ �̵� �ӵ� **/
    [SerializeField] public float itemSpeed = 0;
    /** ������Ʈ �ִ� �̵� �ӵ� **/
    [SerializeField] public float itemmMaxSpeed = 0;
    /** ������Ʈ �ִ� �̵� �ӵ� **/
    [SerializeField] public int timeScore = 0;

    [SerializeField] public float objScale = 1f;


    private int humanPointFlag = 0;
    private int hearthPointFlag = 0;
    [System.NonSerialized] public bool run;

    private bool groundFlag = false;
    public float time { get; private set; } = 0;

    private float pointTime = 0;

    private void Update()
    {
        if (run == false)
            return;

        ItemMng itemMng = ItemMng.Instance;
        time += Time.deltaTime;

        pointTime += Time.deltaTime;
        if (pointTime >= 1)
        {
            pointTime = 0;
            AddScore(timeScore);
        }

        for (EItem item = EItem.Holl_1; item < EItem.MAX; item++)
        {
            if (CheckSpawnCondi(item) == false)
                continue;

            //������Ʈ ����
            GameItem gameItem = null;
            int getPos = -1;
            switch (item)
            {
                case EItem.Holl_2:
                    {
                        getPos = GetCreatePos(2);
                        if (getPos == -1)
                            continue;
                    }
                    break;
                default:
                    {
                        getPos = GetCreatePos(1);
                        if (getPos == -1)
                            continue;
                    }
                    break;
            }
            gameItem = itemMng.CreateItem(item);
            gameItem.CreateObj(getPos);
            gameItem.transform.position = new Vector3(LoopGround.Instance.headX + getPos * objScale, 0.01f, 15);
            gameItem.transform.localScale = Vector3.one * objScale;
        }
    }

    private bool CheckSpawnCondi(EItem item)
    {
        //������Ʈ���� ���� ����
        int lineCnt = 0;
        for(int i = 0; i < LoopGround.Instance.isBreak.Count; i++)
        {
            if (LoopGround.Instance.isBreak[i])
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
                if (hearthPointFlag >= hearthPoint && Canvas.Instance.Get<UIPlayerBoard>().IsFullLife == false)
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
        //������ġ ��ȯ
        List<int> posList = new List<int>();
        for(int i = 0; i < LoopGround.Instance.isBreak.Count; i++)
        {
            if (CanSet(i, width) == false)
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
        //�ش� ��ġ�� ������ �Ҵ��� �� �մ��� ���
        for(int i = idx; i < idx + width; i++)
        {
            if (LoopGround.Instance.isBreak.Count <= i)
                return false;
            if (LoopGround.Instance.isBreak[i])
                return false;
        }
        return true;
    }

    public void RemoveGround()
    {
        Sfx.Instance.Play(ESfx.OnRemoveGround);
        if (groundFlag)
            LoopGround.Instance.BreakRight();
        else
            LoopGround.Instance.BreakLeft();
        groundFlag = !groundFlag;
    }

    public void AddScore(int score)
    {
        UIPlayerBoard uIPlayerBoard = Canvas.Instance.Get<UIPlayerBoard>();
        if (uIPlayerBoard == null)
            return;
        uIPlayerBoard.Score += score;
        humanPointFlag += score;
        hearthPointFlag += score;
    }
}
