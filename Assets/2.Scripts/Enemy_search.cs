using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_search : MonoBehaviour
{
    [SerializeField]
    float Player_Seatch_ange;

    public GameObject Danger;

    public LayerMask targetMask;

    public RaycastHit2D[] inTarget;

    public Transform nearTarget;

    SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        Enemy = transform.parent.transform;
    }

    private void Update()
    {
        if (Rate_Of_Fire < CoolTime)
        {
            CoolTime = 0f;
            Auto_Attack();
        }
    }

    private void FixedUpdate()
    {
        Search();
        nearTarget = GetNearest();
    }

    [SerializeField]
    GameObject Lance;
    Transform Enemy;

    float Rate_Of_Fire = 1.5f;
    public float CoolTime = 0f;

    void Auto_Attack()
    {
        if (!nearTarget)
            return;

        Vector3 targetPos = GameManager.Instance.Player.transform.position;
        Vector3 dir = targetPos - Enemy.transform.position;
        dir = dir.normalized;

        GameObject lance = Instantiate(Lance, Enemy.position, Enemy.rotation);
        lance.GetComponent<Rigidbody2D>().velocity = dir * 20;
    }

    void Search()
    {
        inTarget = Physics2D.CircleCastAll(transform.position + new Vector3(0, 1, 0), Player_Seatch_ange, Vector2.zero, 0, targetMask);

        if (inTarget.Length != 0)
        {
            sprite.color = Color.red;
            CoolTime += Time.deltaTime;
        }

        else
            sprite.color = Color.white;
    }

    Transform GetNearest()
    {
        Transform result = null;
        float diff = 7;

        foreach (RaycastHit2D target in inTarget)
        {
            Vector3 myPos = transform.position;
            Vector3 targetPos = target.transform.position;
            float curdiff = Vector3.Distance(myPos,targetPos);

            if(curdiff < diff)
            {
                diff = curdiff;
                result = target.transform;
            }
        }

        return result;
    }
}
