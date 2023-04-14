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
                // ������ ��鸮�� �ִϸ��̼� �ְ�ʹ�
                StartCoroutine(OnDamage());
            }
        }

        if (collision.gameObject.CompareTag("Player_attack") && isDie == false && isDeSpawn == false)
        { // ���Ͱ� ���� �ʾ��� �� ������ �޾Ҵ°�?

            hit_damage = Instantiate(Hit_Obj1, new Vector3(collision.transform.position.x,
                collision.transform.position.y, collision.transform.position.z), collision.transform.rotation);
            if (!isDamage)
            { // �����ð��� �ƴ϶��
                cur_health = cur_health - GameManager.Instance.Player.ranged_damage;
                // ������ ������ ��ŭ ü�� ����
                StartCoroutine(OnDamage());
                // �����ð� �ο�
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
        { // ���� ���� �ð��� 0���� �۴ٸ�
            isDeSpawn = true;
            gameObject.SetActive(false);
            // ������Ʈ ��Ȱ��ȭ
            return;
        }
        else
            isDeSpawn = false;

        if (health_Slider.value <= 0)
        { // ü���� 0���� �۴ٸ�
            isDie = true;

            for (int i = 0; i <= 3; i++)
            {
                GameObject enemy_exp = GameManager.Instance.object_manager.MakeObj(Obj.enemy_exp_);
                enemy_exp.transform.position = new Vector3(gameObject.transform.position.x,
                    gameObject.transform.position.y, gameObject.transform.position.z);
                // ����ġ ���͸� 3�� Ȱ��ȭ
            }

            gameObject.SetActive(false);
            // Ȱ��ȭ�� ������ ������Ʈ ��Ȱ��ȭ
            return;
        }
        else
            isDie = false;
    }
}
