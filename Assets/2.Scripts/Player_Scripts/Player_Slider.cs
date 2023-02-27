using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Player
{
    [SerializeField]
    float maxHealth;
    public float curHealth;

    [SerializeField]
    float maxBreath;
    float curBreath;

    [SerializeField]
    float maxExperience;
    float curExperience;

    int PlayerLv = 1;
    [SerializeField]
    Text ExpTxt;
    [SerializeField]
    Text HpTxt;
    [SerializeField]
    Text BpTxt;
    [SerializeField]
    Slider healthSlider;
    [SerializeField]
    Slider breathSlider;
    [SerializeField]
    Slider expSlider;

    bool isLv_up;
    bool isDamage;

    [SerializeField]
    GameObject Hit_Obj;
    GameObject hit_damage;

    void Lv_Up()
    { // 레벨 업
        curExperience = 0;
        PlayerLv++;
        ExpTxt.text = "Lv. " + PlayerLv;
        // 경험치를 초기화 하고 레벨 업
        isLv_up = false;
        maxHealth += 2;
        //curHealth += 2;
        // 레벨 업 보상으로 최대 체력 증가
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fish") && isDie == false)
        {
            --GameManager.Instance.re_fish;

            Fish_Enemy fish_exp = collision.GetComponent<Fish_Enemy>();
            collision.gameObject.SetActive(false);
            curExperience += fish_exp.player_exp;
        }

        if (collision.gameObject.CompareTag("Exp") && isDie == false)
        {
            Enemy_exp enemy_exp = collision.GetComponent<Enemy_exp>();
            collision.gameObject.SetActive(false);
            curExperience += enemy_exp.player_exp;
        }

        if (!isDamage)
        {
            if (collision.gameObject.CompareTag("Enemy_Attack") && isDie == false)
            {
                hit_damage = Instantiate(Hit_Obj, new Vector3(collision.transform.position.x,
                    collision.transform.position.y, collision.transform.position.z), collision.transform.rotation);
                collision.gameObject.SetActive(false);
                curHealth = curHealth - 5;
                StartCoroutine(OnDamage());
            }
        }
    }

    IEnumerator OnDamage()
    {
        isDamage = true;
        sprite.color = Color.red;

        yield return new WaitForSeconds(0.5f);

        isDamage = false;
        sprite.color = Color.white;
    }
}
