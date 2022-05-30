using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SteminaUI : MonoBehaviour
{
    public static SteminaUI instance; //�ߺ� ������ ���� ���� ���Ѽ�
    private void Awake() // ���� �Ҷ�
    {
        SteminaUI.instance = this;
    }

    float Stemina; //���� ���׹̳�
    float MaxStemina; //�ִ� ���׹̳�
    public Slider SteminaGUI; //UI����

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
