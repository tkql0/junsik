using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Enemy : MonoBehaviour
{
    bool isDie;
    bool isDeSpawn;

    Rigidbody2D rigid;
    SpriteRenderer sprite;

    int nextMove;

    [SerializeField]
    GameObject Hit_Obj1;
    [SerializeField]
    GameObject Hit_Obj2;

    GameObject hit_damage;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        Invoke("Enemy_Move", 1.0f);
    }

    void Update()
    {
        health_Slider.value = cur_health;

        if (spawn_time_Slider.value > 0.0f)
            spawn_time_Slider.value -= Time.deltaTime;

        Enemy_Die();

        PosDisX();

        Hit_Tracking();

        if(hit_damage != null)
            Destroy(hit_damage, 0.5f);
    }

    void OnEnable()
    {
        isDie = false;
        isDamage = false;
        sprite.color = Color.white;
        cur_health = max_health;
        health_Slider.maxValue = max_health;
        spawn_time_Slider.maxValue = max_spawntime;

        health_Slider.value = max_health;
        spawn_time_Slider.value = max_spawntime;

        hit_damage = null;

        isDamage = false;
    }

    private void OnDisable()
    {
        isDeSpawn = true;
    }

    void PosDisX()
    {
        Vector3 playerPos = GameManager.Instance.Player.transform.position;
        Vector3 myPos = transform.position;

        float DirX = playerPos.x - myPos.x;
        float diffX = Mathf.Abs(DirX);
        // �÷��̾���� �Ÿ��� ���밪���� ���

        if (diffX > 70.0f)
            gameObject.SetActive(false);
        // �÷��̾���� �Ÿ��� 70 �̻��̶�� ��Ȱ��ȭ
    }

    void Enemy_Move()
    {
        nextMove = Random.Range(-1, 2);
        if (nextMove != 0)
            sprite.flipX = nextMove < 0;
        // ������ ������ �ٶ󺸱�

        if(sprite.flipX == true)
            enemy_search.transform.position = new Vector3(transform.position.x - 4,
                enemy_search.transform.position.y, enemy_search.transform.position.z);
        else
            enemy_search.transform.position = new Vector3(transform.position.x + 4,
                enemy_search.transform.position.y, enemy_search.transform.position.z);
        // �ٶ󺸴� �������� Ž�� ������ �̵�


        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);
        // �ٶ󺸴� �������� �̵�

        float next_MoveTime = Random.Range(2f, 5f);
        Invoke("Enemy_Move", next_MoveTime);
        // ������ �ð� �ڿ� �ݺ� ����
    }

    void Hit_Tracking()
    {
        if(isDamage == true)
        { // �������� �Ծ��ٸ�
            Vector3 playerPos = GameManager.Instance.Player.transform.position;
            Vector3 myPos = transform.position;

            float DirX = playerPos.x - myPos.x;
            // �÷��̾���� �Ÿ��� ���

            if (DirX != 0)
                sprite.flipX = DirX < 0;
            // �÷��̾� �������� �̵�

            if (sprite.flipX == true)
                enemy_search.transform.position = new Vector3(transform.position.x - 4,
                    enemy_search.transform.position.y, enemy_search.transform.position.z);
            else
                enemy_search.transform.position = new Vector3(transform.position.x + 4,
                    enemy_search.transform.position.y, enemy_search.transform.position.z);
            // �ٶ󺸴� �������� Ž�� ������ �̵�
        }
    }
}
