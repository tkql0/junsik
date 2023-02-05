using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Enemy : MonoBehaviour
{
    public bool isDie = false;
    bool isDeSpawn = false;

    Rigidbody2D rigid;
    SpriteRenderer sprite;

    int nextMove;

    [SerializeField]
    float max_health;
    float cur_health;
    [SerializeField]
    float max_spawntime;

    public Slider health_Slider;
    public Slider spawn_time_Slider;


    public GameObject bait_prefab;
    public GameObject enemy_search;
    public GameObject Enemy_exp;

    float Search_CoolTime = 0f;
    float Search_resetTime = 2f;

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

        Search_CoolTime += Time.deltaTime;

        if (Search_resetTime < Search_CoolTime)
        {
            sense = false;
        }

        Hit_Tracking();
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

        if (diffX > 70.0f)
        {
            gameObject.SetActive(false);
        }
    }

    void Enemy_Move()
    {
        nextMove = Random.Range(-1, 2);
        if (nextMove != 0)
            sprite.flipX = nextMove < 0;

        if(sprite.flipX == true)
            enemy_search.transform.position = new Vector3(transform.position.x - 4, enemy_search.transform.position.y, enemy_search.transform.position.z);
        else
            enemy_search.transform.position = new Vector3(transform.position.x + 4, enemy_search.transform.position.y, enemy_search.transform.position.z);

        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        float next_MoveTime = Random.Range(2f, 5f);
        Invoke("Enemy_Move", next_MoveTime);
    }

    void Hit_Tracking()
    {
        if(isDamage == true)
        {
            Vector3 playerPos = GameManager.Instance.Player.transform.position;
            Vector3 myPos = transform.position;

            float DirX = playerPos.x - myPos.x;

            if (DirX != 0)
                sprite.flipX = DirX < 0;

            if (sprite.flipX == true)
                enemy_search.transform.position = new Vector3(transform.position.x - 4, enemy_search.transform.position.y, enemy_search.transform.position.z);
            else
                enemy_search.transform.position = new Vector3(transform.position.x + 4, enemy_search.transform.position.y, enemy_search.transform.position.z);
        }
    }

    public bool sense = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isDie == false && isDeSpawn == false)
        {
            if (!isDamage)
            {
                cur_health = cur_health - 5;
                // 닿으면 흔들리는 애니메이션
                StartCoroutine(OnDamage());
            }
        }

        if (collision.gameObject.CompareTag("Player_attack") && isDie == false && isDeSpawn == false)
        {
            if (!isDamage)
            {
                cur_health = cur_health - 5;
                StartCoroutine(OnDamage());
                Destroy(collision.gameObject);
            }
        }
    }

    bool isDamage = false;

    IEnumerator OnDamage()
    {
        isDamage = true;
        sprite.color = Color.red;
        sense = true;

        yield return new WaitForSeconds(0.5f);

        isDamage = false;
        sprite.color = Color.white;
    }

    void Enemy_Die()
    {
        if (spawn_time_Slider.value <= 0)
        {
            isDeSpawn = true;
            gameObject.SetActive(false);
            return;
        }
        else
            isDeSpawn = false;

        if (health_Slider.value <= 0)
        {
            isDie = true;

            for (int i = 0; i <= 3; i++)
            {
                Instantiate(Enemy_exp, transform.position, transform.rotation);
            }

            gameObject.SetActive(false);
            return;
        }
        else
            isDie = false;
    }
}
