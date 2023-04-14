using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Enemy
{
    [SerializeField]
    float max_health;
    float cur_health;
    [SerializeField]
    float max_spawntime;

    [SerializeField]
    Slider health_Slider;
    [SerializeField]
    Slider spawn_time_Slider;

    [SerializeField]
    GameObject enemy_search;
    [SerializeField]
    GameObject Enemy_exp;

    bool isDamage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isDie == false && isDeSpawn == false)
        {
            hit_damage = Instantiate(Hit_Obj2, new Vector3(collision.transform.position.x,
                   collision.transform.position.y, collision.transform.position.z), collision.transform.rotation);
            if (!isDamage)
            {
                cur_health = cur_health - GameManager.Instance.Player.melee_damage;
                // 닿으면 흔들리는 애니메이션 넣고싶다
                StartCoroutine(OnDamage());
            }
        }

        if (collision.gameObject.CompareTag("Player_attack") && isDie == false && isDeSpawn == false)
        { // 몬스터가 죽지 않았을 때 공격을 받았는가?

            hit_damage = Instantiate(Hit_Obj1, new Vector3(collision.transform.position.x,
                collision.transform.position.y, collision.transform.position.z), collision.transform.rotation);
            if (!isDamage)
            { // 무적시간이 아니라면
                cur_health = cur_health - GameManager.Instance.Player.ranged_damage;
                // 공격의 데미지 만큼 체력 감소
                StartCoroutine(OnDamage());
                // 무적시간 부여
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

    void Enemy_Die()
    {
        if (spawn_time_Slider.value <= 0)
        { // 만약 스폰 시간이 0보다 작다면
            isDeSpawn = true;
            gameObject.SetActive(false);
            // 오브젝트 비활성화
            return;
        }
        else
            isDeSpawn = false;

        if (health_Slider.value <= 0)
        { // 체력이 0보다 작다면
            isDie = true;

            for (int i = 0; i <= 3; i++)
            {
                GameObject enemy_exp = GameManager.Instance.object_manager.MakeObj(Obj.enemy_exp_);
                enemy_exp.transform.position = new Vector3(gameObject.transform.position.x,
                    gameObject.transform.position.y, gameObject.transform.position.z);
                // 경험치 몬스터를 3번 활성화
            }

            gameObject.SetActive(false);
            // 활성화가 끝나면 오브젝트 비활성화
            return;
        }
        else
            isDie = false;
    }
}
