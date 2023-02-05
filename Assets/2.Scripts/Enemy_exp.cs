using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_exp : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer sprite;

    int nextMove;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        Invoke("Enemy_Move", 0.1f);
    }

    private void Update()
    {
        PosDisX();
        Destroy(gameObject, 10.0f);
    }

    void PosDisX()
    {
        Vector3 playerPos = GameManager.Instance.Player.transform.position;
        Vector3 myPos = transform.position;

        float DirX = playerPos.x - myPos.x;

        float diffX = Mathf.Abs(DirX);

        if (diffX > 80.0f)
        {
            gameObject.SetActive(false);
        }
    }

    void Enemy_Move()
    {
        nextMove = Random.Range(-1, 2);
        if (nextMove != 0)
            sprite.flipX = nextMove < 0;

        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);
    }
}
