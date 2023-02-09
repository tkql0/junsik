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
        // 플레이어와의 거리를 절대값으로 계산

        if (diffX > 80.0f)
            gameObject.SetActive(false);
        // 플레이어와의 거리가 80 이상이라면 비활성화
    }

    void Enemy_Move()
    {
        nextMove = Random.Range(-1, 2);
        if (nextMove != 0)
            sprite.flipX = nextMove < 0;
        // 랜덤한 방향을 바라보기

        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);
        // 바라보는 방향으로 이동
    }
}
