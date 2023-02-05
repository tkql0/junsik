using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    float maxHealth;
    float curHealth;

    [SerializeField]
    float maxBreath;

    [SerializeField]
    float maxExperience;
    float curExperience = 0;

    public Slider healthSlider;
    public Slider breathSlider;
    public Slider expSlider;

    public Vector2 inputVec;

    int PlayerLv = 1;

    public Text ExpTxt;

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

    private void Start()
    {
        curHealth = maxHealth;

        healthSlider.maxValue = maxHealth;
        breathSlider.maxValue = maxBreath;
        expSlider.maxValue = maxExperience;

        healthSlider.value = maxHealth;
        breathSlider.value = maxBreath;
        expSlider.value = 0;
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
        {
            isDie = true;
            GameOver_Panel.SetActive(true);

            Time.timeScale = 0;
            return;
        }

        Player_Move();
        //if (Input.GetButtonUp("Horizontal"))
        //    rigid.velocity = new Vector2(rigid.velocity.normalized.x, rigid.velocity.y);
        //if (Input.GetButtonUp("Vertical"))
        //    rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.normalized.y);
        // 급정지 기능 급하게 방향조절하면 산소게이지 떨어지게 할수도 있겠다
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

    void Lv_Up()
    {
        expSlider.value = 0;
        curExperience = 0;
        PlayerLv++;
        ExpTxt.text = "Lv. " + PlayerLv;
        isLv_up = false;
        curHealth = curHealth + 2;
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
