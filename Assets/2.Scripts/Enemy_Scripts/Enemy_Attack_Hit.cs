using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack_Hit : MonoBehaviour
{
    GameObject Deep_Sea;

    private void Start()
    {
        Deep_Sea = GameManager.Instance.SeaLevel;
    }

    private void Update()
    {
        PosDisY();
    }

    void PosDisY()
    {
        Vector3 Deep_SeaPos = Deep_Sea.transform.position;
        Vector3 myPos = transform.position;

        float DirY = myPos.y - Deep_SeaPos.y;
        float diffY = Mathf.Abs(DirY);
        // 오브젝트와 공격 오브젝트의 거리를 절대값으로 계산

        if (diffY > 40)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            gameObject.SetActive(false);
    }
}
