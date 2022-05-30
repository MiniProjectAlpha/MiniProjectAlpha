using UnityEngine;
using UnityEngine.UI;

public class HpUI : MonoBehaviour
{
    float nowHP; //현재 체력
    float maxHP; //최대 체력(시작 체력)

    public Slider hpGUI; //GUI 연동 

    public float HP
    {
        get { return nowHP; }
        set
        {
            nowHP = value;
            hpGUI.value = nowHP;
        }
    }

    public float MAXHP
    {
        get { return maxHP; }
        set
        {
            maxHP = value;
            hpGUI.maxValue = maxHP;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        HP = MAXHP;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
