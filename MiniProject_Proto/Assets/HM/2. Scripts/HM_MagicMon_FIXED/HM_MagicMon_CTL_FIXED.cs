using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HM_MagicMon_CTL_FIXED : LivingEntity
{
    CapsuleCollider col;
    Rigidbody rig;
    NavMeshAgent magic_AI;
    Animator magic_anim;

    HM_MagicMon_FSM_FIXED magicmonFsm;

    public Transform magic_Pos;
    public GameObject magic_prefeb;
    GameObject player;

    public float attack_Timer = 1.5f;
    int attack_Delay = 3;
    public float delay_AttackTime = 4.0f;

    public bool isDie = false;
    public bool isKuckBack = false;

    LivingEntity livingEntity;

    protected override void Start()
    {
        base.Start();
        col = GetComponent<CapsuleCollider>();
        rig = GetComponent<Rigidbody>();
        magic_AI = GetComponent<NavMeshAgent>();

        livingEntity = GameObject.FindGameObjectWithTag("Player").GetComponent<LivingEntity>();

        magic_anim = GetComponent<Animator>();
        magicmonFsm = GetComponent<HM_MagicMon_FSM_FIXED>();
    }

    // Update is called once per frame
    void Update()
    {
        if (base.dead == true)
        {
            MagicMon_Die();
        }
        else
        {
            DelayAttack();
        }
    }

    public override void TakeHit2(float damage)
    {
        base.TakeHit2(damage);

        if (!dead)
        {
            isKuckBack = true;
            magic_AI.enabled = false;
            rig.isKinematic = false;
            Invoke("RestoreAI", 1);
        }
        else
        {
            MagicMon_Die();
        }
    }

    void RestoreAI()
    {
        isKuckBack = false;
        magic_AI.enabled = true;
        rig.isKinematic = true;
    }


    public void MagicMon_Die()
    {
        isDie = true;
        magic_AI.enabled = false;
        rig.isKinematic = true;
        col.enabled = false;
        magic_anim.SetTrigger("IsDie");
    }

    public void MagicMon_AttackPlayer(float damage)
    {
        livingEntity.TakeHit2(damage);
    }

    void DelayAttack()
    {
        if(isDie == false && magicmonFsm.mmstate == HM_MagicMon_FSM_FIXED.MagicMonState.ATTACK)
        {
            attack_Timer += Time.deltaTime;

            if(attack_Timer > attack_Delay)
            {
                GameObject magic = Instantiate(magic_prefeb);

                magic.transform.position = magic_Pos.position;

                attack_Timer = 0;
            }
        }
    }
}
