using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    public GameObject PauseUI;
    public GameObject gameOverUI;
    public GameObject PlayGUI;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.PlayGUI.SetActive(true);
        GameManager.instance.PauseUI.SetActive(false);
        GameManager.instance.gameOverUI.SetActive(false);

        Time.timeScale = 1; //시작할 때마다 시간을 흐르게 한다.
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickQuit() 
    {
        SceneManager.LoadScene(0);
    }

    public void OnClickRestart() 
    {
        SceneManager.LoadScene(1);
    }
}
