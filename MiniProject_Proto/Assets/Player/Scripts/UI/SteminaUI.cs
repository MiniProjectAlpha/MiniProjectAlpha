using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SteminaUI : MonoBehaviour
{
    public static SteminaUI instance; //중복 생성을 막기 위한 제한선
    private void Awake() // 시작 할때
    {
        SteminaUI.instance = this;
    }

    float Stemina; //현재 스테미나
    float MaxStemina; //최대 스테미나
    public Slider SteminaGUI; //UI연동

    public float STEMINA
    {
        get { return Stemina; }
        set
        {
            Stemina = value;         
            SteminaGUI.value = Stemina;
        }
    }

    public float MAXSTEMINA
    {
        get { return MaxStemina; }
        set
        {
            MaxStemina = value;
            SteminaGUI.maxValue = MAXSTEMINA;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        STEMINA = MAXSTEMINA;
    }

    // Update is called once per frame
    void Update()
    {       
        STEMINA += Time.deltaTime;

        if (STEMINA > MAXSTEMINA) 
        {
            STEMINA = MAXSTEMINA;
        }
    }
}
