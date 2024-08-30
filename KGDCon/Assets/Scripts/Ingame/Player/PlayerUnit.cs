using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PlayerUnit : SingletonBehaviour<PlayerUnit>
{
    /** 유닛 이동 속도 **/
    [SerializeField] private float      speed;
    /** 판정 범위 값 **/
    [SerializeField] private float      checkDis;
    [SerializeField] private SimpleColorShader simpleColor;
    [SerializeField] private float blinkDelay = 0.1f;
    [SerializeField] private GameObject iceBreak;
    private bool isDie = false;

    private void Update()
    {
        GameSystem gameSystem = GameSystem.Instance;
        if (gameSystem == null || gameSystem.run == false)
            return;
        if (isDie)
            return;
        MoveUpdate();
        DieCheckUpdate();
        ItemCheckUpdate();
    }

    private void MoveUpdate()
    {
        float axisValue = Input.GetAxis("Horizontal");
        if (axisValue == 0)
            return;

        Vector3 movePos = transform.position + new Vector3(1, 0, 0) * axisValue * speed * GameSystem.Instance.objScale * Time.deltaTime;
        if (LoopGround.Instance.IsCanMove(movePos))
            transform.position = movePos;
    }

    private void DieCheckUpdate()
    {
        if (LoopGround.Instance.IsDie(transform.position))
        {
            isDie = true;
            UIGameoverPopup uIGameoverPopup = UIGameoverPopup.Instance;
            UIPlayerBoard uIPlayerBoard = UIPlayerBoard.Instance;

            uIGameoverPopup.Bind(uIPlayerBoard.Score);
            UIGameoverPopup.Instance.Show();
        }
    }

    public void ItemCheckUpdate()
    {
        ItemMng itemMng = ItemMng.Instance;
        for(int i = 0; i < itemMng.gameItem.Count; i++)
        {
            GameItem gameItem = itemMng.gameItem[i];
            if (gameItem.isDie)
                continue;
            if (gameItem.hit)
                continue;
            if (gameItem.eItem == EItem.Holl_2)
            {
                if (Vector3.Distance(transform.position, gameItem.transform.position) < checkDis || 
                    Vector3.Distance(transform.position, gameItem.transform.position + new Vector3(1, 0, 0)) < checkDis)
                {
                    gameItem.GetItem();
                }
            }
            else if (Vector3.Distance(transform.position, gameItem.transform.position) < checkDis)
            {
                gameItem.GetItem();
            }
        }
    }

    public void HitEvent()
    {
        if (UIPlayerBoard.Instance.LifeCount == 0)
            isDie = true;
        else
        {
            IEnumerator run()
            {
                for (int i = 0; i < 3; i++)
                {
                    simpleColor.Set_Shader(1);
                    yield return new WaitForSeconds(blinkDelay);
                    simpleColor.Set_Shader(0);
                    yield return new WaitForSeconds(blinkDelay);
                }
            }
            StartCoroutine(run());
        }
    }

    public void IceBreakEvent()
    {
        IEnumerator run()
        {
            iceBreak.gameObject.SetActive(true);
            yield return new WaitForSeconds(3);
            iceBreak.gameObject.SetActive(false);
        }
        StartCoroutine(run());
    }
}
