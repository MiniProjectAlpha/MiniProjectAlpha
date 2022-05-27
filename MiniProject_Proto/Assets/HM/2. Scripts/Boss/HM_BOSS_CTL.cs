using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HM_BOSS_CTL : LivingEntity
{
    // Start is called before the first frame update
    LivingEntity livingEntity;

    Animator boss_Anim;
    NavMeshAgent boss_AI;
    Rigidbody rig;
    Collider col;

    public bool isDie = false;
    public bool isKuckback = false;

    protected override void Start()
    {
        base.Start();

        livingEntity = GameObject.FindGameObjectWithTag("Player").GetComponent<LivingEntity>();

        boss_Anim = GetComponentInChildren<Animator>();
        boss_AI = GetComponent<NavMeshAgent>();
        col = GetComponent<Collider>();
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (base.dead == true)
        {
            Boss_Die();
        }
    }

    public override void TakeHit2(float damage)
    {
        base.TakeHit2(damage);

        if (!dead)
        {
            isKuckback = true;
            boss_AI.enabled = false;
            rig.isKinematic = false;
            Invoke("RestoreAI", 1);
        }
    }

    void RestoreAI()
    {
        isKuckback = false;
        boss_AI.enabled = true;
        rig.isKinematic = true;
    }

    private void Boss_Die()
    {
        isDie = true;
        boss_AI.enabled = false;
        rig.isKinematic = true;
        col.enabled = false;
        boss_Anim.SetTrigger("IsDie");
    }

    public void Boss_AttackPlayer(float damage)
    {
        livingEntity.TakeHit2(damage);
    }
}
