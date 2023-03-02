using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Obj
{
    enemy_,
    enemy_search_,
    enemy_attack_,
    enemy_exp_,
    fish_,
    player_attack_
}

public class GameManager : MonoSingleTon<GameManager>
{
    public Player Player;

    public GameObject SeaLevel;
    public GameObject GameStart_Panel;
    public RectTransform name_input;

    public bool isSwimming = false;

    public int re_fish = 0;
    int jump_count = 0;

    public ObjectManager object_manager;

    GameObject Player_Jump = null;

    public GameObject jump;

    public Stop_UI stop_;

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
        OnStop();

        if (Player_Jump != null)
            Destroy(Player_Jump, 0.8f);

        SeaLevelPosDisY();

        if(re_fish < 0)
        { // ������ ����ġ ������ ������ 0���� ���ٸ�
            spawn_fish();
            re_fish++;
            // ���� �� +1
        }

        if (GameStart_Panel.activeSelf == true)
        // ���ӽ��� UI�� �����ִٸ�
        {
            Time.timeScale = 0;
        }
        else
        {
            if(!stop_.Click_count)
                Time.timeScale = 1;
        }
    }

    void SeaLevelPosDisY()
    {
        Vector3 playerPos = Player.transform.position;
        Vector3 SeaLevelPos = SeaLevel.transform.position;

        float DirY = playerPos.y - SeaLevelPos.y;
        // �ؼ���� �÷��̾��� ���� ���
        isSwimming = DirY <= 0 ? true : false;
        // �ؼ��麸�� �����ִٸ� ������ ����

        if (isSwimming == false && jump_count == 0)
        {
            jump_count = 1;
            Player_Jump = Instantiate(jump, new Vector3(Player.transform.position.x,
       Player.transform.position.y, Player.transform.position.z), jump.transform.rotation);
        }
        if (isSwimming == true && jump_count == 1)
        {
            jump_count = 0;
            Player_Jump = Instantiate(jump, new Vector3(Player.transform.position.x,
       Player.transform.position.y, Player.transform.position.z), jump.transform.rotation);
        }
    }

    void spawn_fish()
    {
        int ranX = Random.Range(-69, 70);
        int ranY = Random.Range(-25, -5);
        GameObject fish = object_manager.MakeObj(Obj.fish_);
        fish.transform.position = new Vector3(ranX + Player.transform.position.x, ranY , -1);
        // �÷��̾� �ֺ� ������ ��ҿ� ����ġ ���� ����
    }

    public void OnStop()
    {
        if (Player.player_name.Length == 0)
        {
            player_name_ui.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            player_name_ui.SetActive(false);
            if (!stop_.Click_count)
                Time.timeScale = 1;
        }
    }

    public GameObject player_name_ui;
}
