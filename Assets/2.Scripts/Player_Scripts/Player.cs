using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class Player : MonoBehaviour
{
    public Vector2 inputVec;

    Rigidbody2D rigid;
    SpriteRenderer sprite;

    public float move_Maxspeed;
    // 속도를 공격 받을 시 감소 하게

    bool isDie;
    bool isPlayer_Jump;

    public int melee_damage = 5;
    public int ranged_damage = 5;

    public GameObject Danger;
    [SerializeField]
    GameObject GameOver_Panel;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        curHealth = maxHealth;
        curBreath = maxBreath;

        healthSlider.value = maxHealth;
        breathSlider.value = maxBreath;
        expSlider.value = 0;
    }

    private void Update()
    {
        Value_Update();
        Stats_Update();

        if (expSlider.value == 100 && isLv_up == false)
        {
            isLv_up = true;
            Lv_Up();
        }

        if (healthSlider.value <= 0)
            isDie = true;

        if(isDie == true)
        {
            GameOver_Panel.SetActive(true);
            Time.timeScale = 0;
        }

        Player_Move();

        if (hit_damage != null)
            Destroy(hit_damage, 0.5f);

        HpTxt.text = (int)curHealth + " / " + maxHealth;
        BpTxt.text = (int)curBreath + " / " + maxBreath;
    }

    private void FixedUpdate()
    {
        if (isDie == false && GameManager.Instance.isSwimming == true)
        {
            rigid.gravityScale = 0.2f;
            rigid.AddForce(inputVec.normalized, ForceMode2D.Impulse);
            // 중력을 1만큼 주고 키보드를 누른 방향으로 힘을 주어 이동

            if (rigid.velocity.x > move_Maxspeed)
                rigid.velocity = new Vector2(move_Maxspeed, rigid.velocity.y);
            else if (rigid.velocity.x < move_Maxspeed * (-1))
                rigid.velocity = new Vector2(move_Maxspeed * (-1), rigid.velocity.y);

            if (rigid.velocity.y > move_Maxspeed)
                rigid.velocity = new Vector2(rigid.velocity.x, move_Maxspeed);
            else if (rigid.velocity.y < move_Maxspeed * (-1))
                rigid.velocity = new Vector2(rigid.velocity.x, move_Maxspeed * (-1));
            // 주어진 속도은 최고 속도를 넘기지 않게 제한
        }
    }

    void LateUpdate()
    {
        if (inputVec.x != 0)
            sprite.flipX = inputVec.x > 0;
    }

    private void OnEnable()
    {
        hit_damage = null;

        move_Maxspeed = 20;
        isDie = false;
        isPlayer_Jump = false;
        isLv_up = false;
        isDamage = false;
    }

    void Value_Update()
    {
        healthSlider.maxValue = maxHealth;
        breathSlider.maxValue = maxBreath;
        expSlider.maxValue = maxExperience;

        healthSlider.value = curHealth;
        breathSlider.value = curBreath;
        expSlider.value = curExperience;
    }

    public void ReStrat()
    {
        SceneManager.LoadScene(0);
    }

    void Player_Move()
    {
        if (isDie == false)
        { // 플레이어가 죽지 않았다면
            inputVec.x = Input.GetAxisRaw("Horizontal");
            inputVec.y = Input.GetAxisRaw("Vertical");
            // 키보드 입력 받기

            if (GameManager.Instance.isSwimming == true)
            { // 수영중인 상태라면
                isPlayer_Jump = false;

                if (curBreath > 0.0f)
                    curBreath -= Time.deltaTime;
                // 호흡 게이지가 0 이상이라면 호흡 게이지 감소
                else
                    curHealth -= Time.deltaTime;
                // 호흡 게이지가 0 이하라면 체력 게이지 감소
            }
            else if (GameManager.Instance.isSwimming == false)
            { // 수영중인 상태가 아니라면
                rigid.gravityScale = 5f;
                curBreath = maxBreath;
                // 중력을 5만큼 주고 호흡 게이지 회복
                if (curHealth < maxHealth)
                    curHealth += Time.deltaTime;
                // 최대 체력보다 적다면 체력 게이지 회복

                if (isPlayer_Jump == false)
                { // 점프 중이 아니라면
                    rigid.AddForce(Vector2.up * rigid.velocity.y, ForceMode2D.Impulse);
                    isPlayer_Jump = true;
                    // 현재 속도만큼 점프
                }
            }
        }
    }
}
// 공격력이 늘어나면 크기가 커지는게 좋은가