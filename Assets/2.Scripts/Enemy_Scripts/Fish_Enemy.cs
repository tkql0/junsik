using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish_Enemy : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer sprite;

    int nextMove;

    public int player_exp;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        Invoke("Enemy_Move", 1);
    }

    private void Update()
    {
        PosDisX();
    }

    private void OnEnable()
    {
        player_exp = 5;
    }

    void PosDisX()
    {
        Vector3 playerPos = GameManager.Instance.Player.transform.position;
        Vector3 myPos = transform.position;

        float DirX = playerPos.x - myPos.x;
        float diffX = Mathf.Abs(DirX);
        // �÷��̾���� �Ÿ��� ���밪���� ���

        if (diffX > 70.0f)
        {
            --GameManager.Instance.re_fish;
            gameObject.SetActive(false);
            // �÷��̾���� �Ÿ��� 70 �̻��̶�� ��Ȱ��ȭ
        }
    }

    void Enemy_Move()
    {
        nextMove = Random.Range(-1, 2);
        if (nextMove != 0)
            sprite.flipX = nextMove < 0;
        // ������ ������ �ٶ󺸱�

        float next_MoveTime = Random.Range(2f, 5f);
        Invoke("Enemy_Move", next_MoveTime);
        // ������ �ð� �ڿ� �ݺ� ����

        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);
        // �ٶ󺸴� �������� �̵�
    }
}
// �÷��̾ ��������ٸ� �־����� �ٲ����
