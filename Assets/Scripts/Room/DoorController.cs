using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    /* Opens the Door object this script is associated with */
    public void Open()
    {
        ChangeDoorState("Open");
    }

    /* Closes the Door object this script is associated with */
    public void Close()
    {
        ChangeDoorState("Close");
    }

    /* Changes the associated Door object's state via its animator component */
    void ChangeDoorState(string state)
    {
        animator.SetTrigger(state);
    }
}
