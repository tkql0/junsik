using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Type
{
    Null,
    Giant,
    Skin_Breathing
}

public class Player_skill_setting : MonoBehaviour
{
    public Type type;

    public int maxLv;
    public int curLv = 0;

    [SerializeField]
    Text Skill_Lv_Text;

    private void Update()
    {
        if (curLv < maxLv)
            Skill_Lv_Text.text = "Lv." + curLv;
        else
            Skill_Lv_Text.text = "MaxLv";
    }
}
