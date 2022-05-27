using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Test : MonoBehaviour
{

    public bool open = false;
    public float doorOpenAngle = 90f;
    public float doorCloseAngle = 0f;
    public float smoot = 2f;

    public GameObject ply;
   // public GameObject efft;

    public bool eftOn = false;

    //ParticleSystem particlTest;

    // Start is called before the first frame update
    void Start()
    {
        //particlTest = efft.gameObject.GetComponent<ParticleSystem>();
    }

    public void ChangeDoorState()
    {
        open = !open;
    }

    public void SetDoor()
    {
        if (!open)
        {
            Quaternion targetRotation = Quaternion.Euler(0, doorOpenAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smoot * Time.deltaTime);

        }
        else
        {
            Quaternion targetRotation2 = Quaternion.Euler(0, doorCloseAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation2, smoot * Time.deltaTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetDoor();
    }
}
