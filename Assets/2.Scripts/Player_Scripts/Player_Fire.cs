using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.EventSystems;

public class Player_Fire : MonoBehaviour
{
    Transform Player;
    Transform nearTarget;

    [SerializeField]
    float Player_Seatch_ange;
    public LayerMask targetMask;

    public RaycastHit2D[] inTarget;

    Camera cam;

    private void Start()
    {
        cam = Camera.main;
        Player = GameManager.Instance.Player.transform.GetChild(1);
    }

    private void Update()
    {
        LookAtMouse();
        Click_Time();

        CoolTime += Time.deltaTime;

        if(Rate_Of_Fire < CoolTime)
        {
            CoolTime = 0f;
            Auto_Attack();
        }
    }

    public float time = 10f;
    bool mouse_click = false;

    float Rate_Of_Fire = 0.3f;
    float CoolTime = 0f;

    private void FixedUpdate()
    {
        Search();
        nearTarget = GetNearest();
    }

    void Search()
    { // 몬스터 탐색
        inTarget = Physics2D.CircleCastAll(Player.transform.position, Player_Seatch_ange, Vector2.zero, 0, targetMask);
        // 해당 장소에서 주어진 반지름의 거리에 있는 레이어 오브젝트를 받아오기
    }

    Transform GetNearest()
    { // 가장 가까운 오브젝트
        Transform result = null;
        float diff = 7;
        // 탐색 범위 반지름 7 오브젝트 초기화
        foreach (RaycastHit2D target in inTarget)
        { // 탐색된 모든 오브젝트 만큼 반복
            Vector3 myPos = Player.transform.position;
            Vector3 targetPos = target.transform.position;
            float curdiff = Vector3.Distance(myPos, targetPos);
            // 탐색된 오브젝트와 자신과의 거리를 계산
            if (curdiff < diff)
            { // 계산된 거리가 탐색 범위보다 작은가?
                diff = curdiff;
                result = target.transform;
                // 탐색 범위를 계산된 범위로 대체 후 오브젝트 저장
            }
        }
        return result;
        // 저장된 오브젝트를 반환
    }

    void Auto_Attack()
    { // 자동 공격
        if (mouse_click == false && GameManager.Instance.isSwimming == false)
        { // 플레이어가 클릭한 상태가 아니고 수영중이 아니라면
            if (!nearTarget)
                // 가까운 거리에 탐색된 오브젝트가 있는가?
                return;
            // 탐색된 오브젝트가 있다면
            Vector3 targetPos = nearTarget.position;
            Vector3 dir = targetPos - Player.transform.position;
            dir = dir.normalized;
            // 본체와 플레이어의 방향을 계산 후 정렬화

            GameObject lance_Auto = GameManager.Instance.object_manager.MakeObj(Obj.player_attack_);
            lance_Auto.transform.position = Player.transform.position;
            lance_Auto.transform.rotation = Player.transform.rotation;
            lance_Auto.GetComponent<Rigidbody2D>().velocity = dir * 10;
            // 플레이어의 공격을 활성화 후 몬스터를 공격
        }
    }

    void Click_Time()
    {
        if (Input.GetMouseButtonDown(0) && mouse_click == false)
        { // 마우스를 눌렀는가?
            mouse_click = true;
            time = 10f;
            // 마우스를 누른 시간 초기화
        }

        else if (Input.GetMouseButton(0))
        { // 마우스를 누르고 있는가?
            time += Time.deltaTime;
            // 마우스를 누른 시간 증가
        }

        else if (Input.GetMouseButtonUp(0) && GameManager.Instance.isSwimming == false && mouse_click == true)
        { // 수영중이 아니 상태에서 마우스를 놨는가?
            mouse_click = false;
            GameObject lance = GameManager.Instance.object_manager.MakeObj(Obj.player_attack_);
            lance.transform.position = Player.transform.position;
            lance.transform.rotation = Player.transform.rotation;
            lance.GetComponent<Rigidbody2D>().velocity = lance.transform.right * time;
            // 플레이어의 공격을 활성화 후 마우스를 누른 시간 만큼의 힘으로 몬스터를 공격
        }
    }

    void LookAtMouse()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = new Vector2(mousePos.x - Player.position.x, mousePos.y - Player.position.y);

        Player.right = dir.normalized;
    }
}
