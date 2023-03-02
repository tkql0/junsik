using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Name_Input : MonoBehaviour
{
    public InputField playerNameInput;
    private string playerName = null;

    private void Awake()
    {
        playerName = playerNameInput.GetComponent<InputField>().text;
    }

    private void Update()
    {
        if (playerName.Length > 0 && Input.GetKeyDown(KeyCode.Return))
            InputName();
    }

    public void InputName()
    {
        playerName = playerNameInput.text;
        PlayerPrefs.SetString("CurPlayerName", playerName);
        GameManager.Instance.Player.player_name = playerName;
    }
}
