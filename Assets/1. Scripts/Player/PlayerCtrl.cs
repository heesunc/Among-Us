using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public GameObject joyStick;
    Animator anim;
    public float speed;
    // 인스펙터 창에서 설정하기 위해 public으로 작성
    public bool isJoyStick;
    
    private void Start()
    {
        anim = GetComponent<Animator>();
        Camera.main.transform.parent = transform;
        // 메인 카메라의 부모를 지정. 캐릭터가 메인 카메라의 부모가 되는 것. 이 transform은 캐릭터꺼
        Camera.main.transform.localPosition = new Vector3(0, 0, -10);
        // 캐릭터가 어떤 위치에서 생겨도 카메라 0으로 설정해주어야함
        // 2D는 z값이 필요 없지만, 카메라는 기본 z축이 -10임
    }

    private void Update()
    {
        Move();
    }

    // 캐릭터 움직임 관리
    void Move()
    {
        if (isJoyStick)
        {
            joyStick.SetActive(true);
        }
        else
        {
            joyStick.SetActive(false);
            // 클릭했는지 판단
            if (Input.GetMouseButton(0))
            {
                Vector3 dir = (Input.mousePosition - new Vector3(Screen.width * 0.5f, Screen.height * 0.5f)).normalized;
                // 클릭한 값 - 스크린의 가운데 -> 어느 방향으로 터치를 했는지 알 수 있음
                // .normalized로 정규화. 방향 벡터로 바뀌어서 방향을 알 수 있음. dir에 저장
                transform.position += dir * speed * Time.deltaTime;
                // transform에 계속해서 추가. 기기마다 속도가 다를 수 있기 때문에 Time.deltaTime(실제시간)을 곱해줌
                
                anim.SetBool("isWalk",true);
                
                // 왼쪽으로 이동
                if (dir.x < 0)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                } 
                // 오른쪽으로 이동
                else
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
            }
            // 클릭하지 않는다면
            else
            {
                anim.SetBool("isWalk",false);
            }
        }
        
    }
}
