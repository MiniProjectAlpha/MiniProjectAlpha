using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    public static WeaponUI instance; //�ߺ� ������ ���� ���� ���Ѽ�
    private void Awake() // ���� �Ҷ�
    {
        WeaponUI.instance = this;
    }

    #region �ֹ���
    int remain_main; //�ֹ��� �ܿ�
    int max_main; //�ֹ����� �ִ� ��ź��
    
    public Text mainUI; //�ֹ��� ��ź ����
    public bool isReload; //������ ����

    public Image HG;
    public Image SMG;
    public Image AR;
    public Image SR;

    public Slider reloadbar; //������ ��
    float reloadtime; //������ �ð�

    float reloadtimer;

    public int MAXMAIN 
    {
        get { return max_main; }
        set 
        {
            max_main = value;
        }
    }

    public int REMAINMAIN 
    {
        get { return remain_main; }
        set
        {
            remain_main = value;
            mainUI.text = remain_main + " / " + MAXMAIN;
        }
    }
    public bool ISRELOAD 
    {
        get { return isReload; }
        set 
        {
            isReload = value;

            if (isReload == true)
            {
                reloadbar.gameObject.SetActive(true);
                mainUI.text = "Reloading...";
            }
            else if (isReload == false) 
            {
                reloadbar.gameObject.SetActive(false);
            }
        }
    }

    public float RELOAD 
    {
        get { return reloadtime; }
        set 
        {
            reloadtime = value;
            
            reloadbar.maxValue = reloadtime;
        }
    }

    public float TIMER
    {
        get { return reloadtimer; }
        set
        {
            reloadtimer = value;
            reloadbar.value = reloadtimer;
        }
    }
    #endregion

    #region �ι���
    int remain_sub; //�ι��� �ܿ�
    public Text subUI; //�ι��� ��ź ����

    public float delay; //�� ���� ������
    public Text subCoolUI; //�ι��� ��ٿ� ���� 
    
    int subMAX; //�ι��� ����� 
    
    public float isCharge;//�� ���� ��������
    public Text SubCharge; //�ι��� ���� ����


    public Image BL;
    public Image SG;
    public Image GL;
    public Image RL;


    public int SUBMAX //�� ���� �ִ� ����
    {
        get { return subMAX; }
        set
        {
            subMAX = value;
        }
    }
    public int REMAINSUB
    {
        get { return remain_sub; }
        set
        {
            remain_sub = value;
            subUI.text = remain_sub + " / " + SUBMAX;
        }
    }
   
    public float DELAY //�� ���� ����ð�
    {
        get { return delay; }
        set
        {
            delay = value;

            if (delay == 0)
            {
                subCoolUI.text = "�������� �غ�";
            }
            else
            {
                subCoolUI.text = "��ٿ� �� : " + DELAY;
                if (DELAY < 0)
                {
                    DELAY = 0; //���̳ʽ� ����
                }
            }
        }
    }

    public float CHARGE 
    {
        get { return isCharge; }
        set 
        {
            isCharge = value;

            if ((REMAINSUB == SUBMAX))
            {
                SubCharge.text = "�ִ� ���� ����";
            }
            else if ((REMAINSUB < SUBMAX) && (isCharge > 0))
            {
                SubCharge.text = "���� ���� ��";
            }
        }
    }

    public float CHARGETIME 
    {
        get { return isCharge; }
        set
        {
            isCharge = value;
        }
    }

    #endregion
    private void Start()
    {
        DELAY = 0;

        //��� �̹��� ���߾� ������ �ϱ� ���� ���� ������ �ʰ�
        HG.gameObject.SetActive(false);
        SMG.gameObject.SetActive(false);
        AR.gameObject.SetActive(false);
        SR.gameObject.SetActive(false);

        BL.gameObject.SetActive(false);
        SG.gameObject.SetActive(false);
        GL.gameObject.SetActive(false);
        RL.gameObject.SetActive(false);

        //������ �Ҷ��� ���̰� �ϱ�
        reloadbar.gameObject.SetActive(false);


        //�ֹ���, ������� : �ܼ� ��ġȭ �� ����
        
        
        
        //��������, ȸ�� : �̹��� ����
    }

    void Update()
    {
        DELAY -= Time.deltaTime;
        CHARGE -= Time.deltaTime;
    }

}
