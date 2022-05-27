using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CoolDownUI : MonoBehaviour
{
    //연동
    public static CoolDownUI instance; //중복 생성을 막기 위한 제한선
    private void Awake() // 시작 할때
    {
        CoolDownUI.instance = this;
    }

    #region 쿨타임 연동

    //쿨타임 
    float coolDownTime;

    public Text coolDownUi;

    public float timer;

    public Image slideIcon;
    public Image blinkIcon;

    public float COOLDOWN
    {
        get { return coolDownTime; }
        set
        {
            coolDownTime = value;
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {       
    }

    IEnumerator CoolDownSlide(float cooltime)
    {
        timer = 0;

        while (true) 
        {
            timer += Time.deltaTime;
            slideIcon.fillAmount = timer / cooltime;

            if (timer >= cooltime) 
            {               
                timer = 0;
                StopCoroutine("CoolDownSlide");
            }

            yield return null;
        }
    }

    IEnumerator CoolDownBlink(float cooltime)
    {
        timer = 0;

        while (true)
        {
            timer += Time.deltaTime;
            blinkIcon.fillAmount = timer / cooltime;

            if (timer >= cooltime)
            {
                timer = 0;
                StopCoroutine("CoolDownBlink");
            }

            yield return null;
        }
    }
}
