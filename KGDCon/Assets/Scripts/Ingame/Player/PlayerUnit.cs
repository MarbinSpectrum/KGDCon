using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PlayerUnit : SerializedMonoBehaviour
{
    /** 유닛 이동 속도 **/
    [SerializeField] private float      speed;
    /** 판정 범위 값 **/
    [SerializeField] private float      checkDis;
    [SerializeField] private LoopGround loopGround;
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

        Vector3 movePos = transform.position + new Vector3(1, 0, 0) * axisValue * speed * Time.deltaTime;
        if (loopGround.IsCanMove(movePos))
            transform.position = movePos;
    }

    private void DieCheckUpdate()
    {
        if (loopGround.IsDie(transform.position))
        {
            isDie = true;
            UIGameoverPopup uIGameoverPopup = UIGameoverPopup.Instance;
            uIGameoverPopup.Bind(0);
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

            if (gameItem.eItem == EItem.Holl_2)
            {
                if (Vector3.Distance(transform.position, gameItem.transform.position) < checkDis || 
                    Vector3.Distance(transform.position + new Vector3(1, 0, 0), gameItem.transform.position) < checkDis)
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
}
