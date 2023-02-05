using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relocation : MonoBehaviour
{
    private void Update()
    {
        if (!Input.anyKey)
            return;
        PosDisX();
    }

    void PosDisX()
    {
        Vector3 playerPos = GameManager.Instance.Player.transform.position;
        Vector3 myPos = transform.position;

        float DirX = playerPos.x - myPos.x;

        float diffX = Mathf.Abs(DirX);

        DirX = DirX > 0 ? 1 : -1;

        if (diffX > 60.0f)
        {
            transform.Translate(Vector3.right * DirX * 120);
            return;
        }
    }
}
