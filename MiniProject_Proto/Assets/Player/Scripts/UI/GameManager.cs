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

    public int monster_Unit = 0;
    [SerializeField] GameObject[] Gosts;
    [SerializeField] GameObject[] Magics;
    GameObject door;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.PlayGUI.SetActive(true);
        GameManager.instance.PauseUI.SetActive(false);
        GameManager.instance.gameOverUI.SetActive(false);

        Time.timeScale = 1; //시작할 때마다 시간을 흐르게 한다.

        door = GameObject.FindGameObjectWithTag("Door");
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("FindMonster", 3f);
    }

    public void OnClickQuit() 
    {
        SceneManager.LoadScene(0);
    }

    public void OnClickRestart() 
    {
        SceneManager.LoadScene(1);
    }

    //void FindMonsterFunc()
    //{
    //    monsters = GameObject.FindGameObjectsWithTag("Monster");

    //    if(monsters.Length == 0)
    //    {
    //        door.GetComponent<Map_Test>().ChangeDoorState();
    //    }
    //}

    //IEnumerator  FindMonsterList()
    //{
    //    yield return new WaitForSeconds(5f);

    //    monsters = GameObject.FindGameObjectsWithTag("Monster");
     

    //    if (monsters == null)
    //    {
    //        door.GetComponent<Map_Test>().ChangeDoorState();
    //    }
    //}

    void FindMonster()
    {
        Gosts = GameObject.FindGameObjectsWithTag("Gost");
        Magics = GameObject.FindGameObjectsWithTag("MagicMon");

        if (Gosts.Length == 0 && Magics.Length == 0)
        {
            door.GetComponent<Map_Test>().open = false;        }
    }
}
