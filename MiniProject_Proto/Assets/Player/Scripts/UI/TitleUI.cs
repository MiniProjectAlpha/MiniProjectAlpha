using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //화면 이동을 위한 라이브러리 소환

public class TitleUI : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnApplicationQuit()
    {
        Application.Quit();
       //게임 종료
    }
    public void gotoSelctGear() 
    {
        SceneManager.LoadSceneAsync(1);
        //장비 선택 장면(SelectUI 씬으로 이동)
    }
}
