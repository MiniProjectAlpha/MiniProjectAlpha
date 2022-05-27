using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HM_MagicMon_CTL_IP : LivingEntity
{
    #region MagicMon ���� { Health, Attack_Stat }

    [SerializeField] int magic_Helath = 80;

    int magic_AttackStat = 20;

    public bool isHit = false;

    public int MAGIC_HELATH // MagicMon ü�� ������Ƽ
    {
        get { return magic_Helath; }
        set { magic_Helath = value; }
    }
    public int MAGIC_ATTACKSTAT
    {
        get { return magic_AttackStat; }
        set { magic_AttackStat = value; }
    }
    #endregion

    #region Bool�� { ����, ����, ��Ÿ�, ������ }
    public bool is_Live = true;            // ����ִ°�?
    public bool is_Chase = true;          // �������ΰ�?
    public bool is_Arrange = false;        // ��Ÿ��� ��°�?
    public bool is_Attack = false;
    public bool is_Kuckback = false;
    #endregion

    public GameObject magic_prefeb;
    public GameObject player;
    Transform magic_Pos;
    CapsuleCollider col;
    Rigidbody rig;
    NavMeshAgent magic_AI;



    float attack_Timer = 1.5f;
    [SerializeField] int attack_Delay = 3;

    protected override void Start()
    {
        base.Start();
        magic_Pos = this.transform.GetChild(2).gameObject.transform;
        col = GetComponent<CapsuleCollider>();
        rig = GetComponent<Rigidbody>();
        magic_AI = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        Check_MagicMonLife();

        DelayAttack();

        IsHit();

    }

    void DelayAttack()
    {
        if (is_Live && is_Arrange)
        {
            attack_Timer += Time.deltaTime;

            if (attack_Timer > attack_Delay)
            {
                is_Attack = true;

                GameObject magic = Instantiate(magic_prefeb);

                magic.transform.position = magic_Pos.position;

                attack_Timer = 0;
            }
            else
            {
                is_Attack = false;
            }
        }
    }

    void Check_MagicMonLife()
    {
        if (magic_Helath <= 0)
        {
            is_Live = false;
            Destroy(col);
            Destroy(rig);
            Destroy(this.gameObject, 3);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            MAGIC_HELATH -= 40;
        }
    }
    public override void TakeHit2(float damage) //����ź�� ���� ������ ���� (�ܼ� ������ �޴� ����)
    {
        base.TakeHit2(damage);
        if (!dead)
        {
            base.HEALTH -= damage;
            is_Kuckback = true;
            magic_AI.enabled = false;
            rig.isKinematic = false;
            Invoke("RestoreAI", 1);
        }
    }

    void RestoreAI()
    {
        magic_AI.enabled = true;
        rig.isKinematic = true;
        is_Kuckback = false;
    }


    void IsHit()
    {
        if (isHit == true)
        {
            magic_Helath -= 40;
            isHit = false;
        }


    }
}
