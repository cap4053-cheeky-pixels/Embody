using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMechanism : MonoBehaviour
{
    Animator animator;
    bool doorOpen;

    // Added array of GameObjects to keep track of number of enemies.
    // enemyCount just to simplify the condition in OnTriggerEnter
  
    public GameObject[] enemies;
    public int enemyCount = 0;

    void Start()
    {
        doorOpen = false;
        animator = GetComponent<Animator>();    
    }

    void OnTriggerEnter(Collider other)
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCount = enemies.Length;
        
        if(other.gameObject.tag == "Player" && enemyCount == 0)
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
