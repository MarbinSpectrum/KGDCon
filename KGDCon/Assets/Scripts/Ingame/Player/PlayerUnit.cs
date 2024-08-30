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

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        GameSystem gameSystem = GameSystem.Instance;
        if (gameSystem == null || gameSystem.run == false)
            return;
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

        Vector3 movePos = transform.position + new Vector3(1, 0, 0) * axisValue * speed * GameSystem.Instance.objScale * Time.deltaTime;
        if (LoopGround.Instance.IsCanMove(movePos))
            transform.position = movePos;
    }

    private void DieCheckUpdate()
    {
        if (LoopGround.Instance.IsDie(transform.position))
        {
            isDie = true;
            UIGameoverPopup uIGameoverPopup = Canvas.Instance.Get<UIGameoverPopup>();
            UIPlayerBoard uIPlayerBoard = Canvas.Instance.Get<UIPlayerBoard>();

            uIGameoverPopup.Bind(uIPlayerBoard.Score);

            GameSystem.Instance.GameOver();
        }
    }

    public void HitEvent()
    {
        if (Canvas.Instance.Get<UIPlayerBoard>().LifeCount == 0)
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

    public void OnTriggerEnter(Collider other)
    {
        GameItem gameItem = other.GetComponent<GameItem>();
        if (gameItem == null)
            return;
        if (gameItem.isDie)
            return;
        if (gameItem.hit)
            return;

        gameItem.GetItem();
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

    public void Init()
    {
        transform.position = startPos;
        simpleColor.Set_Shader(0);
        iceBreak.gameObject.SetActive(false);
        isDie = false;

        gameObject.SetActive(true);
    }
}

