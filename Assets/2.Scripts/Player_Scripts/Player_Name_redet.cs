using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Name_redet : MonoBehaviour
{
    public void OnReSet()
    {
        PlayerPrefs.DeleteKey("CurPlayerName");
    }
    // �޴����� ���߿� ���� ����â �ʱ�ȭ �����߰ڴ�.
    // �����ϱ�?
}
