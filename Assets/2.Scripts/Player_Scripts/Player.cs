using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public partial class Player : MonoBehaviour
{
    public Vector2 inputVec;

    Rigidbody2D rigid;
    SpriteRenderer sprite;

    public float move_Maxspeed = 0;
    public float jump_power = 30;

    public bool isDie = false;
    public bool isPlayer_Jump = false;
    bool isLv_up = false;

    public GameObject Danger;

    public GameObject GameOver_Panel;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        healthSlider.value = curHealth;
        expSlider.value = curExperience;

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
    }

    public void ReStrat()
    {
        SceneManager.LoadScene(0);
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.isSwimming == true && isDie == false)
        {
            rigid.gravityScale = 0;
            rigid.AddForce(inputVec.normalized, ForceMode2D.Impulse);

            if (rigid.velocity.x > move_Maxspeed)
                rigid.velocity = new Vector2(move_Maxspeed, rigid.velocity.y);
            else if (rigid.velocity.x < move_Maxspeed * (-1))
                rigid.velocity = new Vector2(move_Maxspeed * (-1), rigid.velocity.y);

            if (rigid.velocity.y > move_Maxspeed)
                rigid.velocity = new Vector2(rigid.velocity.x, move_Maxspeed);
            else if (rigid.velocity.y < move_Maxspeed * (-1))
                rigid.velocity = new Vector2(rigid.velocity.x, move_Maxspeed * (-1));
        }
    }

    void LateUpdate()
    {
        if (inputVec.x != 0)
            sprite.flipX = inputVec.x > 0;
    }

    void Player_Move()
    {
        if (isDie == false)
        {
            inputVec.x = Input.GetAxisRaw("Horizontal");
            inputVec.y = Input.GetAxisRaw("Vertical");

            if (GameManager.Instance.isSwimming == true)
            {
                rigid.gravityScale = 0;
                isPlayer_Jump = false;
                if (breathSlider.value > 0.0f)
                    breathSlider.value -= Time.deltaTime;
                else
                    curHealth -= Time.deltaTime;
            }
            else if (GameManager.Instance.isSwimming == false)
            {
                rigid.gravityScale = 5f;
                breathSlider.value = maxBreath;
                if (curHealth < healthSlider.maxValue)
                    curHealth += Time.deltaTime;

                if (isPlayer_Jump == false)
                {
                    rigid.AddForce(Vector2.up * rigid.velocity.y, ForceMode2D.Impulse);
                    isPlayer_Jump = true;
                }
            }
        }
    }
}
