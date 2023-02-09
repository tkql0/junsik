using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_exp : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer sprite;

    int nextMove;

    float spawn_Time;
    float despawn_Time;

    public int player_exp;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        Invoke("Enemy_Move", 0.1f);
    }

    private void Update()
    {
        PosDisX();
        spawn_Time += Time.deltaTime;

        if (despawn_Time < spawn_Time)
            gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        spawn_Time = 0f;
        despawn_Time = 10f;
        player_exp = 10;
    }

    void PosDisX()
    {
        Vector3 playerPos = GameManager.Instance.Player.transform.position;
        Vector3 myPos = transform.position;

        float DirX = playerPos.x - myPos.x;
        float diffX = Mathf.Abs(DirX);
        // �÷��̾���� �Ÿ��� ���밪���� ���

        if (diffX > 80.0f)
            gameObject.SetActive(false);
        // �÷��̾���� �Ÿ��� 80 �̻��̶�� ��Ȱ��ȭ
    }

    void Enemy_Move()
    {
        nextMove = Random.Range(-1, 2);
        if (nextMove != 0)
            sprite.flipX = nextMove < 0;
        // ������ ������ �ٶ󺸱�

        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);
        // �ٶ󺸴� �������� �̵�
    }
}
