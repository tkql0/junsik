using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack_Hit : MonoBehaviour
{
    public int damage = 5;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ocean")|| collision.gameObject.CompareTag("Enemy"))
            gameObject.SetActive(false);
    }
}
