using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject enemy_prefab;
    public GameObject enemy_attack_prefab;
    public GameObject bait_prefab;
    public GameObject fish_prefab;

    public Transform enemy_group;
    public Transform enemy_attack_group;
    public Transform bait_group;
    public Transform fish_group;

    GameObject[] item;

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

        player_attack = new GameObject[20];

        item = new GameObject[100];

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
