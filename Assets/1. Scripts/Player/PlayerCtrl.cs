using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerCtrl : MonoBehaviour
{
    public GameObject joyStick, mainView, playView;
    public Settings settings_script;
    public Button btn;
    public Sprite use, kill;
    public Text text_cool;
    
    Animator anim;
    GameObject coll; // 미션 아이템 저장
    
    public float speed;

    public bool isCantMove, isMission;

    float timer;
    bool isCool;
    private void Start()
    {
        anim = GetComponent<Animator>();
        Camera.main.transform.parent = transform;
        // 메인 카메라의 부모를 지정. 캐릭터가 메인 카메라의 부모가 되는 것. 이 transform은 캐릭터꺼
        Camera.main.transform.localPosition = new Vector3(0, 0, -10);
        // 캐릭터가 어떤 위치에서 생겨도 카메라 0으로 설정해주어야함
        // 2D는 z값이 필요 없지만, 카메라는 기본 z축이 -10임
        
        // 미션이라면
        if (isMission)
        {
            btn.GetComponent<Image>().sprite = use;
            text_cool.text = ""; // kill의 쿨타임 초가 보이지 않도록
        }
        // 킬 퀘스트라면
        else
        {
            btn.GetComponent<Image>().sprite = kill;

            timer = 5;
            isCool = true;
        }
    }

    private void Update()
    {
        // 킬 쿨타임
        if (isCool)
        {
            timer -= Time.deltaTime;
            text_cool.text = Mathf.Ceil(timer).ToString(); // 소수점을 올림해줌

            if (text_cool.text == "0")
            {
                text_cool.text = "";
                isCool = false;
            }
            // text가 0이 되었을 때 비도록 해주고 isCool을 false로 바꿔줌
        }
        if (isCantMove)
        {
            joyStick.SetActive(false);
        }
        else
        {
            Move();    
        }
    }

    // 캐릭터 움직임 관리
    void Move()
    {
        if (settings_script.isJoyStick)
        {
            joyStick.SetActive(true);
        }
        else
        {
            joyStick.SetActive(false);
            // 클릭했는지 판단
            if (Input.GetMouseButton(0))
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    Vector3 dir = (Input.mousePosition - new Vector3(Screen.width * 0.5f, Screen.height * 0.5f))
                        .normalized;
                    // 클릭한 값 - 스크린의 가운데 -> 어느 방향으로 터치를 했는지 알 수 있음
                    // .normalized로 정규화. 방향 벡터로 바뀌어서 방향을 알 수 있음. dir에 저장
                    transform.position += dir * speed * Time.deltaTime;
                    // transform에 계속해서 추가. 기기마다 속도가 다를 수 있기 때문에 Time.deltaTime(실제시간)을 곱해줌

                    anim.SetBool("isWalk", true);

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
            }
            // 클릭하지 않는다면
            else
            {
                anim.SetBool("isWalk", false);
            }
        }
    }
    
    // 캐릭터 삭제
    public void DestroyPlayer()
    {
        Camera.main.transform.parent = null;
        // 카메라 없어지게 null 값 넣어줌
        Destroy(gameObject);
    }

    // 원래 유니티에 있는 함수, Is Trigger가 체크되어있는 Collider와 닿았을 때 호출되는 함수
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Mission" && isMission)
        {
            coll = col.gameObject;
            btn.interactable = true;
        }
        
        if (col.tag == "NPC" && !isMission && !isCool)
        {
            coll = col.gameObject;
            btn.interactable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Mission")
        {
            coll = null;
            btn.interactable = false;
        }
        if (col.tag == "NPC" && !isMission)
        {
            coll = null;
            btn.interactable = false;
        }
    }
    
    // 버튼 누르면 호출
    public void ClickButton()
    {
        // MissionStart를 호출
        coll.SendMessage("MissionStart");

        isCantMove = true;
        btn.interactable = false;
    }
    
    // 미션 종료하면 호출
    public void MissionEnd()
    {
        isCantMove = false; 
    }
}
