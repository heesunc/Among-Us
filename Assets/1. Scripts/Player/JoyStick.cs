using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1. 스틱 드래그 + 제한
// 2. 드래그한만큼 캐릭터 이동
public class JoyStick : MonoBehaviour
{
    public RectTransform stick, backGround;
    PlayerCtrl PlayerCtrl_script;
    
    bool isDrag;
    float limit;

    private void Start()
    {
        PlayerCtrl_script = GetComponent<PlayerCtrl>();
        limit = backGround.rect.width * 0.5f;
    }

    private void Update()
    {
        // 드래그하는 동안
        if (isDrag)
        {
            Vector2 vec = Input.mousePosition - backGround.position;
            stick.localPosition = Vector2.ClampMagnitude(vec, limit);

            Vector3 dir = (stick.position - backGround.position).normalized; // 정규화
            transform.position += dir * PlayerCtrl_script.speed * Time.deltaTime;
            
            // 드래그 끝나면
            if (Input.GetMouseButtonUp(0))
            {
                stick.localPosition = new Vector3(0, 0, 0);
                isDrag = false; // 다시 원위치로
            }
        }
    }

    // 스틱을 누르면 호출
    public void ClickStick()
    {
        isDrag = true;
    }
}
