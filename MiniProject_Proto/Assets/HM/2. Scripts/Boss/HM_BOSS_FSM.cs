using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HM_BOSS_FSM : MonoBehaviour
{
    public enum BossState
    {
        CHASE,
        ATTACK,
        Stun,
    }
    public BossState bossState;

    GameObject player;
    Transform target;

    public float dis;

    Animator boss_Anim;
    NavMeshAgent boss_AI;
    Rigidbody rig;

    HM_BOSS_CTL bossCTL;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
        boss_Anim = GetComponentInChildren<Animator>();
        boss_AI = GetComponent<NavMeshAgent>();
        bossCTL = GetComponent<HM_BOSS_CTL>();
        rig = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        boss_AI.SetDestination(target.position);
        bossState = BossState.CHASE;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(player != null)
        {
            if (!bossCTL.isDie)
            {
                transform.LookAt(target.position);

                dis = Vector3.Distance(target.position, transform.position);
            }

            switch (bossState)
            {
                case BossState.CHASE:
                    BossChaseUpdate();
                    break;

                case BossState.ATTACK:
                    BossAttackUpdate();
                    break;

                case BossState.Stun:
                    BossStunUpdate();
                    break;
            }
        }
        else
        {
            boss_Anim.SetTrigger("IsPlayerDie");
            boss_AI.enabled = false;
            rig.isKinematic = true;
            transform.LookAt(Camera.main.transform.position);
        }
    }

    private void BossStunUpdate()
    {
        if (bossCTL.isKuckback == false)
        {
            if (dis < 8)
            {
                CloseToPlayer();
                bossState = BossState.ATTACK;
            }
            else
            {
                NotCloseToPlayer();
                bossState = BossState.CHASE;
            }
        }
    }

    private void BossChaseUpdate()
    {
        boss_AI.SetDestination(target.position);

        if(dis < 8)
        {
            CloseToPlayer();

            StartCoroutine(BossAI());

            bossState = BossState.ATTACK;

        }
        if (bossCTL.isKuckback == true)
        {
            bossState = BossState.Stun;
            boss_Anim.SetTrigger("IsStun");
        }
    }

    private void BossAttackUpdate()
    {
        if(dis > 8)
        {
            NotCloseToPlayer();

            bossState = BossState.CHASE;

            if (bossCTL.isKuckback == true)
            {
                bossState = BossState.Stun;
                boss_Anim.SetTrigger("IsStun");
            }
        }
    }
    void CloseToPlayer()
    {
        boss_Anim.SetBool("IsChase", false);
        boss_Anim.SetBool("IsAttack", true);

        boss_AI.isStopped = true;
    }

    void NotCloseToPlayer()
    {
        boss_Anim.SetBool("IsChase", true);
        boss_Anim.SetBool("IsAttack", false);

        boss_AI.isStopped = false;
    }

    IEnumerator BossAI()
    {
        yield return new WaitForSeconds(0.1f);

        if(bossState == BossState.ATTACK)
        {
            int rannum = Random.Range(0, 11);

            switch (rannum)
            {
                //기본 공격 1 
                case 0:
                case 1:
                    StartCoroutine(DefaultAttack_1());
                    break;


                //기본 공격 2 
                case 2:
                case 3:
                    StartCoroutine(DefaultAttack_2());
                    break;


                //기본 공격 3 
                case 4:
                case 5:
                    StartCoroutine(DefaultAttack_3());
                    break;

                //콤보 공격 1 
                case 6:
                case 7:
                    StartCoroutine(ComboAttack_1());
                    break;

                //콤보 공격 2 
                case 8:
                case 9:
                    StartCoroutine(ComboAttack_2());
                    break;

                //점프 공격 
                case 10:
                    StartCoroutine(JumpAttack());
                    break;
            }
        }
    }

    #region 보스 패턴 설정

    IEnumerator ComboAttack_1()
    {
        boss_Anim.SetTrigger("Combo_1");
        yield return new WaitForSeconds(4.0f);

        StartCoroutine(BossAI());
    }

    IEnumerator ComboAttack_2()
    {
        boss_Anim.SetTrigger("Combo_2");
        yield return new WaitForSeconds(4.0f);

        StartCoroutine(BossAI());
    }

    IEnumerator JumpAttack()
    {
        boss_Anim.SetTrigger("Jump");
        yield return new WaitForSeconds(4.0f);

        StartCoroutine(BossAI());
    }

    IEnumerator DefaultAttack_1()
    {
        boss_Anim.SetTrigger("Attack_1");
        yield return new WaitForSeconds(2.0f);

        StartCoroutine(BossAI());
    }

    IEnumerator DefaultAttack_2()
    {
        boss_Anim.SetTrigger("Attack_2");
        yield return new WaitForSeconds(2.0f);

        StartCoroutine(BossAI());
    }

    IEnumerator DefaultAttack_3()
    {
        boss_Anim.SetTrigger("Attack_3");
        yield return new WaitForSeconds(2.0f);

        StartCoroutine(BossAI());
    }
    #endregion
}
