using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HM_MagicMon_Ball_FIXED : MonoBehaviour
{
    HM_MagicMon_CTL_FIXED magicmonCTL;
    GameObject player;
    Vector3 dir;

    public int magic_Speed;
    float destroy_timer = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.LookAt(player.transform);

        magicmonCTL = GameObject.FindGameObjectWithTag("MagicMon").GetComponent<HM_MagicMon_CTL_FIXED>();

      
        dir = player.transform.position - transform.position;

        dir.Normalize(); 
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += dir * magic_Speed * Time.deltaTime;

        Destroy(this.gameObject, destroy_timer);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            magicmonCTL.MagicMon_AttackPlayer(15);
        }
            Destroy(this.gameObject);
    }
}
