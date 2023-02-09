using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relocation : MonoBehaviour
{
    private void Update()
    {
        if (!Input.anyKey)
            // 현재 마우스나 키보드를 누르지않고 있는가?
            return;
        PosDisX();
    }

    void PosDisX()
    {
        Vector3 playerPos = GameManager.Instance.Player.transform.position;
        Vector3 myPos = transform.position;

        float DirX = playerPos.x - myPos.x;
        float diffX = Mathf.Abs(DirX);
        // 오브젝트와 플레이어의 거리를 절대값으로 계산

        DirX = DirX > 0 ? 1 : -1;

        if (diffX > 60.0f)
        { // 만약 플레이어와의 거리가 60 이상이라면
            transform.Translate(Vector3.right * DirX * 120);
            return;
            // 플레이어의 반대 방향으로 이동
        }
    }
}
