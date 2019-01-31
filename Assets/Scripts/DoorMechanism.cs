using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMechanism : MonoBehaviour
{
    Animator animator;
    bool doorOpen;

    void Start()
    {
        doorOpen = false;
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            doorOpen = true;
            ChangeDoorState("Open");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(doorOpen)
        {
            doorOpen = false;
            ChangeDoorState("Close");
        }
    }

    void ChangeDoorState(string state)
    {
        animator.SetTrigger(state);
    }
}
