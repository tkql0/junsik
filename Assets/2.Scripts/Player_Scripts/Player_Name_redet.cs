using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Name_redet : MonoBehaviour
{
    public void OnReSet()
    {
        PlayerPrefs.DeleteKey("CurPlayerName");
    }
    // 메뉴판을 나중에 만들어서 스탯창 초기화 나눠야겠다.
    // 저장하기?
}
