using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Camera maincamera; //카메라 변수

    public Player player; //쫓아갈 플레이어
    // Start is called before the first frame update
    void Start()
    {
        maincamera.GetComponent<Camera>(); //카메라 컴포넌트 가져오기
        player.GetComponent<Player>(); //플레이어 컴포넌트 가져오기
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null) 
        {
            maincamera.transform.position = player.transform.position + new Vector3(0, 10, -10);
        }
    }
}
