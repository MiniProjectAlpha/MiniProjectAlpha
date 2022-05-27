using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CoolDownUI : MonoBehaviour
{
    //����
    public static CoolDownUI instance; //�ߺ� ������ ���� ���� ���Ѽ�
    private void Awake() // ���� �Ҷ�
    {
        CoolDownUI.instance = this;
    }

    #region ��Ÿ�� ����

    //��Ÿ�� 
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
