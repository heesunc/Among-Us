using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject missionView;
    // 게임 종료 버튼 누르면 호출
    public void ClickQuit()
    {
        // 유니티 에디터
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        // 안드로이드 에디터
#else
        Application.Quit();
#endif
    }
    
    // 미션 버튼 누르면 호출
    public void ClickMission()
    {
        gameObject.SetActive(false);
        missionView.SetActive(true);

        GameObject player = Instantiate(Resources.Load("Character"), new Vector3(0,-2,0), Quaternion.identity) as GameObject;
        // Quaternion.identity -> 원래 가지고 있는 회전값(Rotation)을 사용하겠다
        // as GameObject -> GameObject 형태로 저장해주겠다
        player.GetComponent<PlayerCtrl>().mainView = gameObject;
        player.GetComponent<PlayerCtrl>().missionView = missionView;
        // 캐릭터를 생성했을때 캐릭터 컨트롤 비어있는 공간에 게임 오브젝트를 넣어줄 수 있는 것
    }
}
