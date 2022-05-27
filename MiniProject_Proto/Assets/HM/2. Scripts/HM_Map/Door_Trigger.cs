using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Trigger : MonoBehaviour
{
    public GameObject door;
    ScriptableObject door_Script;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            door.GetComponent<Map_Test>().ChangeDoorState();
            print("Trigger ON");
        }
        else
        {
            door.GetComponent<Map_Test>().ChangeDoorState();
        }
    }
}
