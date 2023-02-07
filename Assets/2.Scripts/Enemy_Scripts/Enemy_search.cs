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
    // ���� ��Ÿ��

    void Auto_Attack()
    { // �ڵ� ����
        if (inTarget.Length == 0)
            // Ž���� ������Ʈ�� ���°�?
            return;
        // Ž���� ������Ʈ�� �ִٸ�
        Vector3 targetPos = GameManager.Instance.Player.transform.position;
        Vector3 dir = targetPos - Enemy.transform.position;
        dir = dir.normalized;
        // ��ü�� �÷��̾��� ������ ��� �� ����ȭ

        GameObject lance = Instantiate(Lance, Enemy.position, Enemy.rotation);
        lance.GetComponent<Rigidbody2D>().velocity = dir * 20;
        // ������ ������ 
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
    // �� �������� �� �ʿ� �ֳ�?

    //Transform GetNearest()
    //{ // ���� ����� ������Ʈ
    //    Transform result = null;
    //    float diff = 7;
    //    // Ž�� ���� ������ 7 ������Ʈ �ʱ�ȭ

    //    foreach (RaycastHit2D target in inTarget)
    //    { // Ž���� ��� ������Ʈ ��ŭ �ݺ�
    //        Vector3 myPos = transform.position;
    //        Vector3 targetPos = target.transform.position;
    //        float curdiff = Vector3.Distance(myPos,targetPos);
    //        // Ž���� ������Ʈ�� �ڽŰ��� �Ÿ��� ���

    //        if(curdiff < diff)
    //        { // ���� �Ÿ��� Ž�� �������� ������?
    //            diff = curdiff;
    //            result = target.transform;
    //            // Ž�� ������ ���� ������ ��ü ��
    //            // ������Ʈ ����
    //        }
    //    }

    //    return result;
    //    // ����� ������Ʈ�� ��ȯ
    //}
}
