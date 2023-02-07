using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_search : MonoBehaviour
{
    [SerializeField]
    float Player_Seatch_ange;

    public GameObject Danger;

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

    [SerializeField]
    GameObject Lance;
    Transform Enemy;

    float Rate_Of_Fire = 1.5f;
    float CoolDown_Time = 0f;
    // 공격 쿨타임

    void Auto_Attack()
    { // 자동 공격
        if (inTarget.Length == 0)
            // 탐색된 오브젝트가 없는가?
            return;
        // 탐색된 오브젝트가 있다면
        Vector3 targetPos = GameManager.Instance.Player.transform.position;
        Vector3 dir = targetPos - Enemy.transform.position;
        dir = dir.normalized;
        // 본체와 플레이어의 방향을 계산 후 정렬화

        GameObject lance = Instantiate(Lance, Enemy.position, Enemy.rotation);
        lance.GetComponent<Rigidbody2D>().velocity = dir * 20;
        // 몬스터의 공격을 
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
    // 이 다음까지 갈 필요 있나?

    //Transform GetNearest()
    //{ // 가장 가까운 오브젝트
    //    Transform result = null;
    //    float diff = 7;
    //    // 탐색 범위 반지름 7 오브젝트 초기화

    //    foreach (RaycastHit2D target in inTarget)
    //    { // 탐색된 모든 오브젝트 만큼 반복
    //        Vector3 myPos = transform.position;
    //        Vector3 targetPos = target.transform.position;
    //        float curdiff = Vector3.Distance(myPos,targetPos);
    //        // 탐색된 오브젝트와 자신과의 거리를 계산

    //        if(curdiff < diff)
    //        { // 계산된 거리가 탐색 범위보다 작은가?
    //            diff = curdiff;
    //            result = target.transform;
    //            // 탐색 범위를 계산된 범위로 대체 후
    //            // 오브젝트 저장
    //        }
    //    }

    //    return result;
    //    // 저장된 오브젝트를 반환
    //}
}
