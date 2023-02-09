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
        // 플레이어와의 거리를 절대값으로 계산

        if (diffX > 70.0f)
        {
            --GameManager.Instance.re_fish;
            gameObject.SetActive(false);
            // 플레이어와의 거리가 70 이상이라면 비활성화
        }
    }

    void Enemy_Move()
    {
        nextMove = Random.Range(-1, 2);
        if (nextMove != 0)
            sprite.flipX = nextMove < 0;
        // 랜덤한 방향을 바라보기

        float next_MoveTime = Random.Range(2f, 5f);
        Invoke("Enemy_Move", next_MoveTime);
        // 랜덤한 시간 뒤에 반복 실행

        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);
        // 바라보는 방향으로 이동
    }
}
// 플레이어가 가까워진다면 멀어지게 바꿔야지
