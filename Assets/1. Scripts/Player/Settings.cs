using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public bool isJoyStick;
    public Image touchBtn, joyStickBtn;
    public Color blue;
    public PlayerCtrl playerCtrl_script;
    
    // 설정 버튼을 누르면 호출
    public void ClickSetting()
    {
        gameObject.SetActive(true);
        playerCtrl_script.isCantMove = true;
    }
    
    // 게임으로 돌아가기 버튼을 누르면 호출
    public void ClickBack()
    {
        gameObject.SetActive(false);
        playerCtrl_script.isCantMove = false;
    }
    
    // 터치이동을 누르면 호출
    public void ClickTouch()
    {
        isJoyStick = false;
        touchBtn.color = blue;
        joyStickBtn.color = Color.white;
    }
    
    // 조이스틱을 누르면 호출
    public void ClickJoyStick()
    {
        isJoyStick = true;
        touchBtn.color = Color.white;;
        joyStickBtn.color = blue;
    }
}
