using UnityEngine;
using UnityEngine.UI;

public class HpUI : MonoBehaviour
{
    float nowHP; //���� ü��
    float maxHP; //�ִ� ü��(���� ü��)

    public Slider hpGUI; //GUI ���� 

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
