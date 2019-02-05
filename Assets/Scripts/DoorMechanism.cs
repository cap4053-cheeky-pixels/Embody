using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMechanism : MonoBehaviour
{
    Animator animator;
    bool doorOpen;

    // Added array of GameObjects to keep track of number of enemies.
    // enemyCount just to simplify the condition in OnTriggerEnter
    public GameObject player;
    public GameObject[] enemies;
    public int enemyCount = 0;

    void Start()
    {
        doorOpen = false;
        animator = GetComponent<Animator>();

        // initializes enemies array and sets enemyCount 
        // for number of enemies in the scene
        if (enemies != null)
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            enemyCount += 1;
        }

    }

    void Update()
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy == null)
            {   // if an enemy is destroyed, count is updated
                // and the array is recycled
                enemyCount -= 1;
                System.Collections.Generic.List<GameObject> list = new System.Collections.Generic.List<GameObject>(enemies);
                list.Remove(enemy);
                enemies = list.ToArray();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
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
