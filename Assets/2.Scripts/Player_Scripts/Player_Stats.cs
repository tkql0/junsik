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

        Health_Txt.text = "ü�� : " + (int)curHealth + " / " + maxHealth;
        Dive_Time_Txt.text = "��� �ð� : " + (int)curBreath + " / " + +maxBreath;
        Melee_Attack_Text.text = "�ٰŸ� ������ : " + melee_damage;
        Ranged_Attack_Text.text = "���Ÿ� ������ : " + ranged_damage;
        Player_Lv_Text.text = "Lv. " + PlayerLv;
        Player_Lv_point_Text.text = player_name + " ( " + Lv_point + " )";
    }

    public void Player_skill(Player_skill_setting skill_setting)
    {
        //GameObject click_skill = EventSystem.current.currentSelectedGameObject;
        // EventSystem.current.currentSelectedGameObject �� ������ ������ null �޸�

        if (skill_setting.curLv >= skill_setting.maxLv)
            return;

        if (Lv_point != 0)
        {
            switch (skill_setting.type)
            {
                case Type.Giant:
                    transform.localScale = new Vector3(transform.localScale.x + 0.5f,
                        transform.localScale.y + 0.5f);
                    maxHealth += 2;
                    melee_damage += 1;
                    skill_setting.curLv++;
                    break;
                case Type.Skin_Breathing:
                    maxBreath += 2;
                    skill_setting.curLv++;
                    break;
            }
            Lv_point--;
        }
    }
}
