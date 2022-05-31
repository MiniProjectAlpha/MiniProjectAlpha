using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    public static WeaponUI instance; //중복 생성을 막기 위한 제한선
    private void Awake() // 시작 할때
    {
        WeaponUI.instance = this;
    }

    #region 주무장
    int remain_main; //주무장 잔여
    int max_main; //주무기의 최대 장탄수
    
    public Text mainUI; //주무장 잔탄 연동
    public bool isReload; //재장전 여부

    public Image HG;
    public Image SMG;
    public Image AR;
    public Image SR;

    public Slider reloadbar; //재장전 바
    float reloadtime; //재장전 시간

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

    #region 부무장
    int remain_sub; //부무장 잔여
    public Text subUI; //부무장 잔탄 연동

    public float delay; //부 무장 딜레이
    public Text subCoolUI; //부무장 쿨다운 연동 
    
    int subMAX; //부무장 스톡수 
    
    public float isCharge;//부 무장 충전여부
    public Text SubCharge; //부무장 충전 연동


    public Image BL;
    public Image SG;
    public Image GL;
    public Image RL;


    public int SUBMAX //부 무장 최대 스톡
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
   
    public float DELAY //부 무장 재사용시간
    {
        get { return delay; }
        set
        {
            delay = value;

            if (delay == 0)
            {
                subCoolUI.text = "보조무기 준비";
            }
            else
            {
                subCoolUI.text = "쿨다운 중 : " + DELAY;
                if (DELAY < 0)
                {
                    DELAY = 0; //마이너스 방지
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
                SubCharge.text = "최대 충전 상태";
            }
            else if ((REMAINSUB < SUBMAX) && (isCharge > 0))
            {
                SubCharge.text = "현재 충전 중";
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

        //장비 이미지 맞추어 나오게 하기 위해 전부 보이지 않게
        HG.gameObject.SetActive(false);
        SMG.gameObject.SetActive(false);
        AR.gameObject.SetActive(false);
        SR.gameObject.SetActive(false);

        BL.gameObject.SetActive(false);
        SG.gameObject.SetActive(false);
        GL.gameObject.SetActive(false);
        RL.gameObject.SetActive(false);

        //재장전 할때만 보이게 하기
        reloadbar.gameObject.SetActive(false);


        //주무기, 보조장비 : 단순 수치화 및 설명
        
        
        
        //보조무기, 회피 : 이미지 삽입
    }

    void Update()
    {
        DELAY -= Time.deltaTime;
        CHARGE -= Time.deltaTime;
    }

}
