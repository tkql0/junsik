using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Obj
{
    enemy_,
    enemy_search_,
    enemy_attack_,
    bait_,
    fish_,
    player_attack_
}

public class GameManager : MonoSingleTon<GameManager>
{
    public Player Player;

    public GameObject SeaLevel;
    public GameObject GameStart_Panel;

    public bool isMove = false;
    public bool isSwimming = false;

    public int player_prize = 0;
    public int re_fish = 0;

    public ObjectManager object_manager;

    private void Start()
    {
        re_fish = object_manager.fish.Length;
        // ó�� ������ �� ������ ����ġ ���� ����
        for(int i = 1; i <= re_fish; i++)
            // �־��� �� ��ŭ ����
            spawn_fish();
        re_fish = 0;
        // ��� �۾� �Ϸ� �� �ʱ�ȭ
    }

    private void Update()
    {
        SeaLevelPosDisY();

        if(re_fish > 0)
        { // ������ ����ġ ������ ������ 0���� ���ٸ�
            spawn_fish();
            re_fish++;
            // ���� �� +1
        }

        if(GameStart_Panel.activeSelf == true)
            // ���ӽ��� UI�� �����ִٸ�
            Time.timeScale = 0;
        // ���� �Ͻ� ����
    }

    void SeaLevelPosDisY()
    {
        Vector3 playerPos = Player.transform.position;
        Vector3 SeaLevelPos = SeaLevel.transform.position;

        float DirY = playerPos.y - SeaLevelPos.y;
        // �ؼ���� �÷��̾��� ���� ���
        isSwimming = DirY <= 0 ? true : false;
        // �ؼ��麸�� �����ִٸ� ������ ����
    }

    void spawn_fish()
    {
        int ranX = Random.Range(-69, 70);
        int ranY = Random.Range(-25, -5);
        GameObject fish = object_manager.MakeObj(Obj.fish_);
        fish.transform.position = new Vector3(ranX + Player.transform.position.x, ranY , -1);
        // �÷��̾� �ֺ� ������ ��ҿ� ����ġ ���� ����
    }
}
