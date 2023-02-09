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
    { // ���� Ž��
        inTarget = Physics2D.CircleCastAll(Player.transform.position, Player_Seatch_ange, Vector2.zero, 0, targetMask);
        // �ش� ��ҿ��� �־��� �������� �Ÿ��� �ִ� ���̾� ������Ʈ�� �޾ƿ���
    }

    Transform GetNearest()
    { // ���� ����� ������Ʈ
        Transform result = null;
        float diff = 7;
        // Ž�� ���� ������ 7 ������Ʈ �ʱ�ȭ
        foreach (RaycastHit2D target in inTarget)
        { // Ž���� ��� ������Ʈ ��ŭ �ݺ�
            Vector3 myPos = Player.transform.position;
            Vector3 targetPos = target.transform.position;
            float curdiff = Vector3.Distance(myPos, targetPos);
            // Ž���� ������Ʈ�� �ڽŰ��� �Ÿ��� ���
            if (curdiff < diff)
            { // ���� �Ÿ��� Ž�� �������� ������?
                diff = curdiff;
                result = target.transform;
                // Ž�� ������ ���� ������ ��ü �� ������Ʈ ����
            }
        }
        return result;
        // ����� ������Ʈ�� ��ȯ
    }

    void Auto_Attack()
    { // �ڵ� ����
        if (mouse_click == false && GameManager.Instance.isSwimming == false)
        { // �÷��̾ Ŭ���� ���°� �ƴϰ� �������� �ƴ϶��
            if (!nearTarget)
                // ����� �Ÿ��� Ž���� ������Ʈ�� �ִ°�?
                return;
            // Ž���� ������Ʈ�� �ִٸ�
            Vector3 targetPos = nearTarget.position;
            Vector3 dir = targetPos - Player.transform.position;
            dir = dir.normalized;
            // ��ü�� �÷��̾��� ������ ��� �� ����ȭ

            GameObject lance_Auto = GameManager.Instance.object_manager.MakeObj(Obj.player_attack_);
            lance_Auto.transform.position = Player.transform.position;
            lance_Auto.transform.rotation = Player.transform.rotation;
            lance_Auto.GetComponent<Rigidbody2D>().velocity = dir * 10;
            // �÷��̾��� ������ Ȱ��ȭ �� ���͸� ����
        }
    }

    void Click_Time()
    {
        if (Input.GetMouseButtonDown(0) && mouse_click == false)
        { // ���콺�� �����°�?
            mouse_click = true;
            time = 10f;
            // ���콺�� ���� �ð� �ʱ�ȭ
        }

        else if (Input.GetMouseButton(0))
        { // ���콺�� ������ �ִ°�?
            time += Time.deltaTime;
            // ���콺�� ���� �ð� ����
        }

        else if (Input.GetMouseButtonUp(0) && GameManager.Instance.isSwimming == false && mouse_click == true)
        { // �������� �ƴ� ���¿��� ���콺�� ���°�?
            mouse_click = false;
            GameObject lance = GameManager.Instance.object_manager.MakeObj(Obj.player_attack_);
            lance.transform.position = Player.transform.position;
            lance.transform.rotation = Player.transform.rotation;
            lance.GetComponent<Rigidbody2D>().velocity = lance.transform.right * time;
            // �÷��̾��� ������ Ȱ��ȭ �� ���콺�� ���� �ð� ��ŭ�� ������ ���͸� ����
        }
    }

    void LookAtMouse()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = new Vector2(mousePos.x - Player.position.x, mousePos.y - Player.position.y);

        Player.right = dir.normalized;
    }
}
