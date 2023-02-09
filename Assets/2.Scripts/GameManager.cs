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
        // 처음 시작할 때 생성될 경험치 몬스터 갯수
        for(int i = 1; i <= re_fish; i++)
            // 주어진 수 만큼 생성
            spawn_fish();
        re_fish = 0;
        // 모든 작업 완료 후 초기화
    }

    GameObject Player_Jump = null;

    public GameObject jump;

    private void Update()
    {
        if (Player_Jump != null)
            Destroy(Player_Jump, 0.8f);

        SeaLevelPosDisY();

        if(re_fish < 0)
        { // 생성될 경험치 몬스터의 갯수가 0보다 적다면
            spawn_fish();
            re_fish++;
            // 생성 후 +1
        }

        if(GameStart_Panel.activeSelf == true)
            // 게임시작 UI가 켜져있다면
            Time.timeScale = 0;
        // 게임 일시 정지
    }

    int jump_count = 0;

    void SeaLevelPosDisY()
    {
        Vector3 playerPos = Player.transform.position;
        Vector3 SeaLevelPos = SeaLevel.transform.position;

        float DirY = playerPos.y - SeaLevelPos.y;
        // 해수면과 플레이어의 높이 계산
        isSwimming = DirY <= 0 ? true : false;
        // 해수면보다 낮게있다면 수영중 판정

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
        // 플레이어 주변 랜덤한 장소에 경험치 몬스터 생성
    }
}
