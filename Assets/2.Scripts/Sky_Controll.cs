using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sky_Controll : MonoBehaviour
{
    public GameObject enemy_obj;
    
    private void Start()
    {
        StartCoroutine(spawn());
    }

    void spawn_enemy()
    {
        int ranEnemy = Random.Range(-20, 20);
        GameObject enemy = GameManager.Instance.object_manager.MakeObj(Obj.enemy_);
        enemy.transform.position = new Vector3(transform.position.x + ranEnemy, 0.3f, -2);
    }

    IEnumerator spawn()
    {
        spawn_enemy();
        yield return new WaitForSeconds(10.0f);
        StartCoroutine(spawn());
    }
}
