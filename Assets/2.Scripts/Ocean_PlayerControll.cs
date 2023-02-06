using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ocean_PlayerControll : MonoBehaviour
{
    public GameObject Deep_Sea;

    private void Update()
    {
        PosDisY();
    }

    void PosDisY()
    {
        Vector3 playerPos = GameManager.Instance.Player.transform.position;
        Vector3 myPos = Deep_Sea.transform.position;

        float DirY = playerPos.y - myPos.y;

        if (DirY < 3)
        {
            GameManager.Instance.Player.isDie = true;

            GameManager.Instance.Player.Danger.SetActive(false);
        }
        else if (DirY < 5)
            GameManager.Instance.Player.Danger.SetActive(true);
        else
            GameManager.Instance.Player.Danger.SetActive(false);
    }
}