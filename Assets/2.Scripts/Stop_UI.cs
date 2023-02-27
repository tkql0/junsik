using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stop_UI : MonoBehaviour
{
    [SerializeField]
    GameObject Stop_Button;

    bool Click_count = false;

    public void OnStop()
    {
        if (Click_count == false)
        {
            enter();
            Click_count = true;
            Time.timeScale = 0;
        }
        else
        {
            exit();
            Click_count = false;
            Time.timeScale = 1;
        }
    }

    public RectTransform stats_ui;

    void enter()
    {
        stats_ui.anchoredPosition = Vector2.zero;
    }

    void exit()
    {
        stats_ui.anchoredPosition = Vector2.up * 2000;
    }
}
