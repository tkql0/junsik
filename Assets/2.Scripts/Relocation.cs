using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relocation : MonoBehaviour
{
    private void Update()
    {
        if (!Input.anyKey)
            // ���� ���콺�� Ű���带 �������ʰ� �ִ°�?
            return;
        PosDisX();
    }

    void PosDisX()
    {
        Vector3 playerPos = GameManager.Instance.Player.transform.position;
        Vector3 myPos = transform.position;

        float DirX = playerPos.x - myPos.x;
        float diffX = Mathf.Abs(DirX);
        // ������Ʈ�� �÷��̾��� �Ÿ��� ���밪���� ���

        DirX = DirX > 0 ? 1 : -1;

        if (diffX > 60.0f)
        { // ���� �÷��̾���� �Ÿ��� 60 �̻��̶��
            transform.Translate(Vector3.right * DirX * 120);
            return;
            // �÷��̾��� �ݴ� �������� �̵�
        }
    }
}
