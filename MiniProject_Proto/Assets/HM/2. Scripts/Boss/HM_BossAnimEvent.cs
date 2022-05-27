using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HM_BossAnimEvent : MonoBehaviour
{
    HM_BOSS_FSM boss_Fsm;
    HM_BOSS_CTL boss_CTL;

    // Start is called before the first frame update
    void Start()
    {
        boss_Fsm = GetComponentInParent<HM_BOSS_FSM>();
        boss_CTL = GetComponentInParent<HM_BOSS_CTL>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDefaultAttackHit()
    {
        if(boss_Fsm.bossState == HM_BOSS_FSM.BossState.ATTACK)
        {
            boss_CTL.Boss_AttackPlayer(20f);
            Debug.Log("DefaultAttack");
        }
    }
    public void OnComBoAttackHit()
    {
        if (boss_Fsm.bossState == HM_BOSS_FSM.BossState.ATTACK)
        {
            boss_CTL.Boss_AttackPlayer(10f);
            Debug.Log("ComBoAttack");
        }
    }
    public void specialAttackHit()
    {
        if (boss_Fsm.bossState == HM_BOSS_FSM.BossState.ATTACK)
        {
            boss_CTL.Boss_AttackPlayer(30f);
            Debug.Log("SpecialAttack");
        }
    }
}
