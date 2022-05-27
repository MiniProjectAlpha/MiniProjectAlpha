using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public GameObject explosionEffect; //���� ȿ��   
    public LayerMask collisionMask; //�ε�ħ ����.

    float speed = 10f; //���ư��� �ӵ�

    public float explodeForce = 10f; //���� ����
    public float radius = 2.0f; //���߹ݰ�

    public float directdamage = 3f; //���ݽ� �ִ� ���ط�
    public float explodedamage = 1.5f; //���� ���ط�
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float moveDistance = speed * Time.deltaTime;
        CheckCollisions(moveDistance);

        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    #region źȯ �κ�
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    void CheckCollisions(float moveDistance)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, moveDistance, collisionMask, QueryTriggerInteraction.Collide))
        { 
            OnHitObject(hit);
            explode();
        }
    }

    void OnHitObject(RaycastHit hit)
    {
        IDamageable damageableObject = hit.collider.GetComponent<IDamageable>();


        if (damageableObject != null)
        {
            damageableObject.TakeHit(directdamage, hit);
        }

        GameObject.Destroy(gameObject);
    }
    #endregion ����ź �κ�

    #region ���� �κ�
    void explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius); //���� �������κ��� �ݰ泻 �־��� ���ӿ�����Ʈ üũ

        foreach (Collider near in colliders)
        {
            Rigidbody rig = near.GetComponent<Rigidbody>();
            LivingEntity livingEntity = near.GetComponent<LivingEntity>();

            if (rig != null)
            {
                rig.AddExplosionForce(explodeForce, transform.position, radius, 1f, ForceMode.Impulse); //�ݰ泻 ��ƼƼ �з���.
            }

            if (livingEntity != null)
            {
                livingEntity.TakeHit2(explodedamage); //�ݰ泻 ��ƼƼ���� ������      
            }

        }
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    #endregion 
}
