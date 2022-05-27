using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update

    public LayerMask collisionMask;
    
    float speed = 10f;
    public float damage = 0.5f; // źȯ ���ݷ�

    void Start()
    {
        Destroy(gameObject, 1f);
    }

    public void SetSpeed(float newSpeed) {
        speed = newSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        float moveDistance = speed * Time.deltaTime;
        CheckCollisions(moveDistance);

        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    void CheckCollisions(float moveDistance) 
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, moveDistance, collisionMask, QueryTriggerInteraction.Collide))
        {
            OnHitObject(hit);
        }
    }

    void OnHitObject(RaycastHit hit) 
    {
        IDamageable damageableObject = hit.collider.GetComponent<IDamageable>();


        if (damageableObject != null)
        {
            damageableObject.TakeHit(damage, hit);
        }
        
        GameObject.Destroy(gameObject);
    }

    
}
