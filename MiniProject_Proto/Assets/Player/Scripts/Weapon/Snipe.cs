using UnityEngine;

public class Snipe : MonoBehaviour
{
    // Start is called before the first frame update

    public LayerMask collisionMask;

    float speed = 10f;
    public float damage = 0.5f; // 탄환 공격력

    int limitPiercing = 2; //관통 제한

    void Start()
    {
        Destroy(gameObject, 1f);
    }

    public void SetSpeed(float newSpeed)
    {
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

        if (Physics.Raycast(ray, out hit, moveDistance, collisionMask))
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

            if (limitPiercing > 0) //
            {
                limitPiercing--; //관통 스택 제거
                damage *= 0.9f; //데미지 10% 감소
            }
            else if (limitPiercing == 0)
            {
                Destroy(gameObject);
            }
        }
        else //생명체가 아님
        {
            Destroy(gameObject); //그대로 파괴
        }
    }
}
