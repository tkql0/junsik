using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.EventSystems;

public class Player_Fire : MonoBehaviour
{
    [SerializeField]
    GameObject Lance;
    [SerializeField]
    Transform Player;

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

    void LookAtMouse()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = new Vector2(mousePos.x - Player.position.x, mousePos.y - Player.position.y);

        Player.right = dir;
        // 움직이는 걸 손으로 바꾸고 누르고 있다면 충전 떄면 공격으로
    }

    public float time = 10f;
    bool mouse_click = false;

    float Rate_Of_Fire = 0.3f;
    float CoolTime = 0f;

    void Click_Time()
    {
        if (Input.GetMouseButtonDown(0) && mouse_click == false)
        {
            mouse_click = true;
            time = 10f;
        }

        else if (Input.GetMouseButton(0))
        {
            time += Time.deltaTime;
        }

        else if (Input.GetMouseButtonUp(0) && GameManager.Instance.isSwimming == false && mouse_click == true)
        {
            mouse_click = false;
            GameObject lance = Instantiate(Lance, Player.position, Player.rotation);
            lance.GetComponent<Rigidbody2D>().velocity = lance.transform.right * time;
        }
    }

    void Auto_Attack()
    {
        if (mouse_click == false && GameManager.Instance.isSwimming == false)
        {
            if (!nearTarget)
                return;

            Vector3 targetPos = nearTarget.position;
            Vector3 dir = targetPos - Player.transform.position;
            dir = dir.normalized;

            GameObject lance = Instantiate(Lance, Player.position, Player.rotation);
            lance.GetComponent<Rigidbody2D>().velocity = dir * 10;
        }
    }

    [SerializeField]
    float Player_Seatch_ange;
    public LayerMask targetMask;

    public RaycastHit2D[] inTarget;

    public Transform nearTarget;

    private void FixedUpdate()
    {
        Search();
        nearTarget = GetNearest();
    }

    void Search()
    {
        inTarget = Physics2D.CircleCastAll(Player.transform.position, Player_Seatch_ange, Vector2.zero, 0, targetMask);
    }

    Transform GetNearest()
    {
        Transform result = null;
        float diff = 7;

        foreach (RaycastHit2D target in inTarget)
        {
            Vector3 myPos = Player.transform.position;
            Vector3 targetPos = target.transform.position;
            float curdiff = Vector3.Distance(myPos, targetPos);

            if (curdiff < diff)
            {
                diff = curdiff;
                result = target.transform;
            }
        }

        return result;
    }
}
