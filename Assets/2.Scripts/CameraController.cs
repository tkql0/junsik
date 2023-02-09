using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector3 CamPos = new Vector3(0, 0, -10);
    [SerializeField]
    GameObject player;

    [SerializeField]
    Vector2 center;
    [SerializeField]
    Vector2 mapSize;

    float speed;

    float height;

    private void Start()
    {
        speed = GameManager.Instance.Player.move_Maxspeed;

        height = Camera.main.orthographicSize;
    }

    private void FixedUpdate()
    {
        LimitCamArea();
    }

    void LimitCamArea()
    {
        transform.position = Vector3.Lerp(transform.position,
            player.transform.position + CamPos, Time.fixedDeltaTime * speed);
        //if(GameManager.Instance.Player.isDie == true)
        //{
            float ly = mapSize.y - height;
            float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

            transform.position = new Vector3(transform.position.x, clampY, -10f);
        //}
    }
}