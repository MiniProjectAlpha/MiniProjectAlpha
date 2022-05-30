using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpUI : MonoBehaviour
{
    public static HpUI instance;

    private void Awake()
    {
        instance = this;
    }

    public float maxHP;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
