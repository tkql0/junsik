using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start_UI : MonoBehaviour
{
    [SerializeField]
    GameObject GameStart_Panel;

    public void OnClick()
    {
        if (GameStart_Panel == true)
        {
            GameStart_Panel.SetActive(false);
        }
    }
}
