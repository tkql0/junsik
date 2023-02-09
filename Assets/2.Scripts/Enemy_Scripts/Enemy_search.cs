using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_search : MonoBehaviour
{
    [SerializeField]
    float Player_Seatch_ange;

    float Rate_Of_Fire = 1.5f;
    float CoolDown_Time = 0f;
    // 공격 쿨타임

    public GameObject Danger;

    [SerializeField]
    GameObject Lance;
    Transform Enemy;

    public LayerMask targetMask;

    public RaycastHit2D[] inTarget;

    private void Start()
    {
        Enemy = transform.parent.transform;
    }

    private void Update()
    {
        if (Rate_Of_Fire < CoolDown_Time)
        {
            CoolDown_Time = 0f;
            Auto_Attack();
        }
    }

    private void FixedUpdate()
    {
        Search();
    }

    void Search()
    { // 플레이어 탐색
        inTarget = Physics2D.CircleCastAll(transform.position + new Vector3(0, 1, 0), Player_Seatch_ange, Vector2.zero, 0, targetMask);
        // 해당 장소에서 주어진 반지름의 거리에 있는 레이어 오브젝트를 받아오기
        if (inTarget.Length != 0)
        { // 탐색된 오브젝트가 한라도 있는가?
            Danger.SetActive(true);
            CoolDown_Time += Time.deltaTime;
            // 경고 오브젝트 활성화
            // 공격 쿨타임 증가
        }
        else
            Danger.SetActive(false);
        // 아니라면 경고 오브젝트 비활성화
    }

    void Auto_Attack()
    { // 자동 공격
        if (inTarget.Length == 0)
            // 탐색된 오브젝트가 없는가?
            return;
        // 탐색된 오브젝트가 있다면
        Vector3 targetPos = GameManager.Instance.Player.transform.position;
        Vector3 dir = targetPos - Enemy.position;
        dir = dir.normalized;
        // 본체와 플레이어의 방향을 계산 후 정렬화

        GameObject lance = GameManager.Instance.object_manager.MakeObj(Obj.enemy_attack_);
        lance.transform.position = Enemy.position;
        lance.transform.rotation = Enemy.rotation;
        lance.GetComponent<Rigidbody2D>().velocity = dir * 20;
        // 몬스터의 공격을 활성화 후 플레이어를 공격
    }
}
