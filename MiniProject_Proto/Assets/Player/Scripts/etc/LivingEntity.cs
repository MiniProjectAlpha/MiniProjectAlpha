using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public float StartingHealth; //시작 체력(최대 체력)
    public float health; //현재 체력   
    public HpUI hpUI;

    public float HEALTH
    {
        get { return health; }

        set
        {
            health = value;
            if (hpUI)
            {
                hpUI.HP = health;
            }
        }
    } //체력 변형


    protected bool dead; //사망 여부

    // Start is called before the first frame update
    protected virtual void Start()
    {
        HEALTH = StartingHealth;
        
        //체력 연동
        hpUI.MAXHP = StartingHealth;
        hpUI.HP = health;
    }

    public void TakeHit(float damage, RaycastHit hit)
    {
        HEALTH -= damage;

        if (health <= 0 && !dead)
        {
            Dead();
        }
    }

    public virtual void TakeHit2(float damage) //수류탄을 위한 데미지 계산식 (단순 데미지 받는 공식)
    {
        HEALTH -= damage;

        if (health <= 0 && !dead)
        {
            Dead();
        }
    }

    public void Dead()
    {
        dead = true;
        Destroy(gameObject, 2f);
    }


    // Update is called once per frame
    void Update()
    {

    }
}
