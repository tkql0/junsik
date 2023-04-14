using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    Rigidbody2D rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        transform.right = GetComponent<Rigidbody2D>().velocity;
        lance_gravity();
    }

    void lance_gravity()
    {
        Vector3 myPos = transform.position;
        Vector3 SeaLevelPos = GameManager.Instance.SeaLevel.transform.position;

        float DirY = myPos.y - SeaLevelPos.y;
        rigid.drag = DirY <= 0 ? 3 : 1;
        // 몬스터의 공격이 해수면보다 아래인가? 맞다면 가속 1로 변경
    }
}
