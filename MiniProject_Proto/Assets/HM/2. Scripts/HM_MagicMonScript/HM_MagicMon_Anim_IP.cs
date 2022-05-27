using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HM_MagicMon_Anim_IP : MonoBehaviour
{
    Animator magic_anim;

    HM_MagicMon_CTL_IP magic_Ctl;

    // Start is called before the first frame update
    void Start()
    {
        magic_anim = GetComponent<Animator>();
        magic_Ctl = GetComponent<HM_MagicMon_CTL_IP>();
    }

    // Update is called once per frame
    void Update()
    {
        if (magic_Ctl.is_Live)
        {
            AnimControl();
        }
        else
        {
            magic_anim.SetTrigger("IsLive");

        }
    }

    void AnimControl()
    {
        if (magic_Ctl.is_Live == true)
        {
            if (magic_Ctl.is_Chase && !magic_Ctl.is_Arrange)
            {
                magic_anim.SetBool("IsChase", true);
                magic_anim.SetBool("IsArrange", false);
                
            }
            else if (!magic_Ctl.is_Chase && magic_Ctl.is_Arrange)
            {
                magic_anim.SetBool("IsChase", false);
                magic_anim.SetBool("IsArrange", true);
               
            }
        }
    }
}
