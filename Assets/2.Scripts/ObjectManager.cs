using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [SerializeField]
    GameObject enemy_prefab;
    [SerializeField]
    GameObject enemy_attack_prefab;
    [SerializeField]
    GameObject bait_prefab;
    [SerializeField]
    GameObject fish_prefab;
    [SerializeField]
    GameObject player_attack_prefab;

    [SerializeField]
    Transform enemy_group;
    [SerializeField]
    Transform enemy_attack_group;
    [SerializeField]
    Transform bait_group;
    [SerializeField]
    Transform fish_group;
    [SerializeField]
    Transform player_attack_group;

    GameObject[] enemy;
    GameObject[] enemy_attack;
    GameObject[] bait;
    public GameObject[] fish;
    GameObject[] player_attack;

    GameObject[] targetPool;

    private void Awake()
    {
        enemy = new GameObject[30];
        enemy_attack = new GameObject[30];
        bait = new GameObject[10];
        fish = new GameObject[50];
        player_attack = new GameObject[30];

        Generate();
    }

    void Generate()
    {
        for (int i = 0; i < enemy.Length; i++)
        {
            enemy[i] = Instantiate(enemy_prefab, enemy_group);
            enemy[i].SetActive(false);
        }
        for (int i = 0; i < enemy_attack.Length; i++)
        {
            enemy_attack[i] = Instantiate(enemy_attack_prefab, enemy_attack_group);
            enemy_attack[i].SetActive(false);
        }
        for (int i = 0; i < bait.Length; i++)
        {
            bait[i] = Instantiate(bait_prefab, bait_group);
            bait[i].SetActive(false);
        }
        for (int i = 0; i < fish.Length; i++)
        {
            fish[i] = Instantiate(fish_prefab, fish_group);
            fish[i].SetActive(false);
        }
        for (int i = 0; i < player_attack.Length; i++)
        {
            player_attack[i] = Instantiate(player_attack_prefab, player_attack_group);
            player_attack[i].SetActive(false);
        }
    }

    public GameObject MakeObj(Obj type)
    {
        switch (type)
        {
            case Obj.enemy_:
                targetPool = enemy;
                break;
            case Obj.enemy_attack_:
                targetPool = enemy_attack;
                break;
            case Obj.bait_:
                targetPool = bait;
                break;
            case Obj.fish_:
                targetPool = fish;
                break;
            case Obj.player_attack_:
                targetPool = player_attack;
                break;
        }
        for (int i = 0; i < targetPool.Length; i++)
        {
            if (!targetPool[i].activeSelf)
            {
                targetPool[i].SetActive(true);
                return targetPool[i];
            }
        }
        return null;
    }
}
