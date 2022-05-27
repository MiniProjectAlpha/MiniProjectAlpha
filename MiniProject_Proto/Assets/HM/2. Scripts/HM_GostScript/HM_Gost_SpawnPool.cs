using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HM_Gost_SpawnPool : MonoBehaviour
{
    [SerializeField]
    int gost_Spawn_num;

    [SerializeField]
    int gost_num_limit;

    [SerializeField]
    int gost_Spawn_time;

    int gost_num;

    int random;

    public GameObject gost_obj;

    [SerializeField]
    Transform[] gost_SpawnPoint;

    float a = 0;

    void Start()
    {
        
    }

    void Update()
    {
        Delay_Time();
    }

    void Spawn()
    {
        random = Random.Range(0, gost_Spawn_num);
        
        GameObject selectGost = gost_obj;
        Transform gostSpawnPoint = gost_SpawnPoint[random];

        GameObject instance = Instantiate(selectGost, gostSpawnPoint);

        instance.transform.position = gostSpawnPoint.transform.position;

        gost_num++;
    }

    void Delay_Time()
    {
        
        a += Time.deltaTime;

        if (a > gost_Spawn_time && (gost_num < gost_num_limit))
        {
            Spawn();
            a = 0;
            print("Spawn");
        }
    }

}
