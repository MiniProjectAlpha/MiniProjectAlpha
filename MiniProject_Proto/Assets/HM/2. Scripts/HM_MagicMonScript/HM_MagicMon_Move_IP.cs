using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HM_MagicMon_Move_IP : MonoBehaviour
{
    NavMeshAgent magic_AI;

    GameObject player;

    HM_MagicMon_CTL_IP magic_Ctl;

    float dirToPly;


    void Start()
    {
        magic_AI = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        magic_Ctl = GetComponent<HM_MagicMon_CTL_IP>();

        magic_AI.SetDestination(player.transform.position);
    }


    void Update()
    {
        CheckDirToPly();
    }

    void CheckDirToPly()
    {
        dirToPly = Vector3.Distance(player.transform.position, this.transform.position);

        if (magic_Ctl.is_Live)
        {
            if (dirToPly < 10)
            {
                magic_Ctl.is_Chase = false;
                magic_Ctl.is_Arrange = true;
                magic_Ctl.is_Attack = true;
                magic_AI.isStopped = true;

                this.transform.LookAt(player.transform.position);
            }
            else
            {
                magic_Ctl.is_Chase = true;
                magic_Ctl.is_Arrange = false;
                magic_Ctl.is_Attack = false;
                magic_AI.isStopped = false;
                magic_AI.SetDestination(player.transform.position);
            }
        }
        else
        {
            magic_AI.isStopped = true;
        }
    }
}




