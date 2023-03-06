using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Player
{
    [SerializeField]
    Text Health_Txt;
    [SerializeField]
    Text Dive_Time_Txt;
    [SerializeField]
    Text Melee_Attack_Text;
    [SerializeField]
    Text Ranged_Attack_Text;
    [SerializeField]
    Text Player_Lv_Text;
    [SerializeField]
    Text Player_Lv_point_Text;

    public string player_name = null;

    public int Lv_point = 0;

    void Stats_Update()
    {
        if(PlayerPrefs.HasKey("CurPlayerName"))
            player_name = PlayerPrefs.GetString("CurPlayerName");

        Health_Txt.text = "체력 : " + (int)curHealth + " / " + maxHealth;
        Dive_Time_Txt.text = "잠수 시간 : " + (int)curBreath + " / " + +maxBreath;
        Melee_Attack_Text.text = "근거리 데미지 : " + melee_damage;
        Ranged_Attack_Text.text = "원거리 데미지 : " + ranged_damage;
        Player_Lv_Text.text = "Lv. " + PlayerLv;
        Player_Lv_point_Text.text = player_name + " ( " + Lv_point + " )";
    }

    public void Player_skill(Player_skill_setting skill_setting)
    {
        //GameObject click_skill = EventSystem.current.currentSelectedGameObject;
        // EventSystem.current.currentSelectedGameObject 빈 공간을 누르면 null 메모

        if (Lv_point != 0)
        {
            switch (skill_setting.type)
            {
                case Type.Dwarf:

                    break;
                case Type.Giant:
                    transform.localScale = new Vector3(transform.localScale.x + 1f,
                        transform.localScale.y + 1f);
                    maxHealth += 2;
                    melee_damage += 1;
                    break;
            }
            Lv_point--;
        }
    }
}
