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
        magic_Pos = this.transform.GetChild(2).gameObject.transform;
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

    public IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(0.1f);

        if(magicmonFsm.mmstate == HM_MagicMon_FSM_FIXED.MagicMonState.ATTACK)
        {
            StartCoroutine("InstantiateMagic");
        }
        
    }

    IEnumerator InstantiateMagic()
    {
        GameObject magic = Instantiate(magic_prefeb);

        magic.transform.position = magic_Pos.position;

        yield return new WaitForSeconds(delay_AttackTime);

        StartCoroutine("DelayAttack");
    }
}
