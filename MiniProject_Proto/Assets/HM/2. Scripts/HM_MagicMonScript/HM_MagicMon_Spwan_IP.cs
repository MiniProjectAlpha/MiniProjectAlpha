using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HM_MagicMon_Spwan_IP : MonoBehaviour
{
    [SerializeField]
    int magic_Spawn_num;

    [SerializeField]
    int magic_num_limit;

    [SerializeField]
    int magic_Spawn_time;

    int magic_num;

    int random;

    public GameObject magic_obj;

    [SerializeField]
    Transform[] magic_SpawnPoint;

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
        random = Random.Range(0, magic_Spawn_num);

        GameObject selectGost = magic_obj;
        Transform gostSpawnPoint = magic_SpawnPoint[random];

        GameObject instance = Instantiate(selectGost, gostSpawnPoint);

        instance.transform.position = gostSpawnPoint.transform.position;

        magic_num++;
    }

    void Delay_Time()
    {

        a += Time.deltaTime;

        if (a > magic_Spawn_time && (magic_num < magic_num_limit))
        {
            Spawn();
            a = 0;
            print("Spawn");
        }
    }
}
