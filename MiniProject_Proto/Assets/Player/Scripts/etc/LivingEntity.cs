using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public float StartingHealth; //���� ü��(�ִ� ü��)
    public float health; //���� ü��   
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
    } //ü�� ����


    protected bool dead; //��� ����

    // Start is called before the first frame update
    protected virtual void Start()
    {
        HEALTH = StartingHealth;
        
        //ü�� ����
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

    public virtual void TakeHit2(float damage) //����ź�� ���� ������ ���� (�ܼ� ������ �޴� ����)
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
