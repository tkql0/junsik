using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_search : MonoBehaviour
{
    [SerializeField]
    float Player_Seatch_ange;

    float Rate_Of_Fire = 1.5f;
    float CoolDown_Time = 0f;
    // ���� ��Ÿ��

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
    { // �÷��̾� Ž��
        inTarget = Physics2D.CircleCastAll(transform.position + new Vector3(0, 1, 0), Player_Seatch_ange, Vector2.zero, 0, targetMask);
        // �ش� ��ҿ��� �־��� �������� �Ÿ��� �ִ� ���̾� ������Ʈ�� �޾ƿ���
        if (inTarget.Length != 0)
        { // Ž���� ������Ʈ�� �Ѷ� �ִ°�?
            Danger.SetActive(true);
            CoolDown_Time += Time.deltaTime;
            // ��� ������Ʈ Ȱ��ȭ
            // ���� ��Ÿ�� ����
        }
        else
            Danger.SetActive(false);
        // �ƴ϶�� ��� ������Ʈ ��Ȱ��ȭ
    }

    void Auto_Attack()
    { // �ڵ� ����
        if (inTarget.Length == 0)
            // Ž���� ������Ʈ�� ���°�?
            return;
        // Ž���� ������Ʈ�� �ִٸ�
        Vector3 targetPos = GameManager.Instance.Player.transform.position;
        Vector3 dir = targetPos - Enemy.position;
        dir = dir.normalized;
        // ��ü�� �÷��̾��� ������ ��� �� ����ȭ

        GameObject lance = GameManager.Instance.object_manager.MakeObj(Obj.enemy_attack_);
        lance.transform.position = Enemy.position;
        lance.transform.rotation = Enemy.rotation;
        lance.GetComponent<Rigidbody2D>().velocity = dir * 20;
        // ������ ������ Ȱ��ȭ �� �÷��̾ ����
    }
}
