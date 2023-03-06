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
    // �ӵ��� ���� ���� �� ���� �ϰ�

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
            // �߷��� 1��ŭ �ְ� Ű���带 ���� �������� ���� �־� �̵�

            if (rigid.velocity.x > move_Maxspeed)
                rigid.velocity = new Vector2(move_Maxspeed, rigid.velocity.y);
            else if (rigid.velocity.x < move_Maxspeed * (-1))
                rigid.velocity = new Vector2(move_Maxspeed * (-1), rigid.velocity.y);

            if (rigid.velocity.y > move_Maxspeed)
                rigid.velocity = new Vector2(rigid.velocity.x, move_Maxspeed);
            else if (rigid.velocity.y < move_Maxspeed * (-1))
                rigid.velocity = new Vector2(rigid.velocity.x, move_Maxspeed * (-1));
            // �־��� �ӵ��� �ְ� �ӵ��� �ѱ��� �ʰ� ����
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
        { // �÷��̾ ���� �ʾҴٸ�
            inputVec.x = Input.GetAxisRaw("Horizontal");
            inputVec.y = Input.GetAxisRaw("Vertical");
            // Ű���� �Է� �ޱ�

            if (GameManager.Instance.isSwimming == true)
            { // �������� ���¶��
                isPlayer_Jump = false;

                if (curBreath > 0.0f)
                    curBreath -= Time.deltaTime;
                // ȣ�� �������� 0 �̻��̶�� ȣ�� ������ ����
                else
                    curHealth -= Time.deltaTime;
                // ȣ�� �������� 0 ���϶�� ü�� ������ ����
            }
            else if (GameManager.Instance.isSwimming == false)
            { // �������� ���°� �ƴ϶��
                rigid.gravityScale = 5f;
                curBreath = maxBreath;
                // �߷��� 5��ŭ �ְ� ȣ�� ������ ȸ��
                if (curHealth < maxHealth)
                    curHealth += Time.deltaTime;
                // �ִ� ü�º��� ���ٸ� ü�� ������ ȸ��

                if (isPlayer_Jump == false)
                { // ���� ���� �ƴ϶��
                    rigid.AddForce(Vector2.up * rigid.velocity.y, ForceMode2D.Impulse);
                    isPlayer_Jump = true;
                    // ���� �ӵ���ŭ ����
                }
            }
        }
    }
}
// ���ݷ��� �þ�� ũ�Ⱑ Ŀ���°� ������