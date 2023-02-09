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

    [SerializeField]
    float maxExperience;
    float curExperience;

    int PlayerLv = 1;
    [SerializeField]
    Text ExpTxt;
    [SerializeField]
    Slider healthSlider;
    [SerializeField]
    Slider breathSlider;
    [SerializeField]
    Slider expSlider;

    bool isLv_up;
    [SerializeField]
    GameObject Hit_Obj;

    GameObject hit_damage;

    void Lv_Up()
    {
        expSlider.value = 0;
        curExperience = 0;
        PlayerLv++;
        ExpTxt.text = "Lv. " + PlayerLv;
        isLv_up = false;
        curHealth = curHealth + 2;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fish") && isDie == false)
        {
            --GameManager.Instance.re_fish;
            collision.gameObject.SetActive(false);
            curExperience = curExperience + 5;
        }

        if (collision.gameObject.CompareTag("Exp") && isDie == false)
        {
            collision.gameObject.SetActive(false);
            curExperience = curExperience + 10;
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

    bool isDamage = false;

    IEnumerator OnDamage()
    {
        isDamage = true;
        sprite.color = Color.red;

        yield return new WaitForSeconds(0.5f);

        isDamage = false;
        sprite.color = Color.white;
    }
}
