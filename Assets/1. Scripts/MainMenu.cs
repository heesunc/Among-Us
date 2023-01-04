using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
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
}
