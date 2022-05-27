using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HM_MagicMon_FSM_FIXED : MonoBehaviour
{
    public enum MagicMonState
    {
        CHASE,
        ATTACK,
        Stun,
    }
    public MagicMonState mmstate;

    GameObject player;
    Transform target;

    public float dis;
     float attack_Dis = 6;

    Animator magic_Anim;
    NavMeshAgent magic_AI;
    Rigidbody rig;

    HM_MagicMon_CTL_FIXED magicmonCTL;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;

        dis = Vector3.Distance(target.position, transform.position);

        magicmonCTL = GetComponent<HM_MagicMon_CTL_FIXED>();

        magic_AI = GetComponent<NavMeshAgent>();
        magic_Anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        magic_AI.SetDestination(target.position);

        mmstate = MagicMonState.CHASE;

    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            dis = Vector3.Distance(target.position, transform.position);


            switch (mmstate)
            {
                case MagicMonState.CHASE:
                    MMChaseUpdate();
                    break;

                case MagicMonState.ATTACK:
                    MMAttackUpdate();
                    break;

                case MagicMonState.Stun:
                    MMStunUpdate();
                    break;
            }
        }
    }

    private void MMChaseUpdate()
    {
        magic_AI.SetDestination(target.position);

        if(dis < attack_Dis)
        {
            CloseToAttack();

            mmstate = MagicMonState.ATTACK;
            magicmonCTL.StartCoroutine("DelayAttack");
        }
        if(magicmonCTL.isKuckBack == true)
        {
            mmstate = MagicMonState.Stun;
            magic_Anim.SetTrigger("IsStun");
        }
    }


    private void MMAttackUpdate()
    {

        if(dis > attack_Dis)
        {
            NotCloseToAttack();

            mmstate = MagicMonState.CHASE;
           
        }
        if (magicmonCTL.isKuckBack == true)
        {
            mmstate = MagicMonState.Stun;
            magic_Anim.SetTrigger("IsStun");
        }
    }

    private void MMStunUpdate()
    {
        if(magicmonCTL.isKuckBack == false)
        {
            if(dis < attack_Dis)
            {
                CloseToAttack();
                mmstate = MagicMonState.ATTACK;
            }
            else
            {
                NotCloseToAttack();
                mmstate = MagicMonState.CHASE;
            }
        }
    }

    private void CloseToAttack()
    {
        transform.LookAt(target);

        magic_Anim.SetBool("IsChase", false);
        magic_Anim.SetBool("IsAttack", true);

        magic_AI.isStopped = true;
    }

    private void NotCloseToAttack()
    {
        magic_Anim.SetBool("IsChase", true);
        magic_Anim.SetBool("IsAttack", false);

        magic_AI.isStopped = false;
    }
}
