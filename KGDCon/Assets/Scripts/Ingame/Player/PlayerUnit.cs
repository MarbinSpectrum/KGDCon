using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PlayerUnit : SerializedMonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private LoopGround loopGround;
    private bool isDie = false;

    private void Update()
    {
        if (isDie)
            return;
        MoveUpdate();
        DieCheckUpdate();
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
            gameObject.SetActive(false);
        }
    }
}
