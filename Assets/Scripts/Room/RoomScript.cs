using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    // To be assigned via the Unity editor
    public List<GameObject> doors;
    public GameObject currentRoom;

    private HashSet<GameObject> spawnedEnemies;
    private EnemyDetection detection;
    private SpawnScript spawner;
    private WaitForSeconds wait;
    private int numEnemies = 0;

    // TODO somehow keep track of current list of enemies, not just original... how do we know when an enemy dies?

    private void Awake()
    {
        spawner = gameObject.transform.Find("SpawnPoints").gameObject.GetComponent<SpawnScript>();
        spawnedEnemies = spawner.SpawnEnemies();
        numEnemies = spawnedEnemies.Count;
        SubscribeToEnemyDeath();
    }

    /* Allows this Room to listen to each enemy it has spawned in order to
     * detect enemy death. This allows it to control when the room doors open.
     */ 
    void SubscribeToEnemyDeath()
    {
        foreach(GameObject enemyObject in spawnedEnemies)
        {
            Enemy enemy = enemyObject.gameObject.GetComponent<Enemy>();

            if(enemy != null)
            {
                enemy.deathEvent += OnEnemyDied;
            }
        }
    }

    /* Called each time an enemy dies in the current room. Used to update the
     * enemy count and to handle the case when all enemies have been slain.
     */ 
    void OnEnemyDied(GameObject enemy)
    {
        spawnedEnemies.Remove(enemy);
        numEnemies--;
        Debug.Log("An enemy died! Current room now has " + numEnemies + " enemies!");

        if(numEnemies == 0)
        {
            UnlockAllDoors();
        }
    }   

    /* Opens all doors for the current room.
     */ 
    void UnlockAllDoors()
    {
        foreach (GameObject door in doors)
        {
            DoorController doorController = door.GetComponent<DoorController>();
            doorController.Open();
        }
    }

    /* Closes all doors for the current room.
     */
    void LockAllDoors()
    {
        foreach (GameObject door in doors)
        {
            DoorController doorController = door.GetComponent<DoorController>();
            doorController.Close();
        }
    }

    /* Called when any other collision object enters this Room. Used to detect when the player
     * enters the room. If there are currently enemies, it will lock all doors.
     */
    private void OnCollisionEnter(Collision collision)
    {
       if (collision.gameObject.tag == "Player")
        {
            // Enemies remaining
            if (numEnemies != 0)
            {
                // Lock all doors behind the player if there are still enemies
                LockAllDoors();
            }
            // No more enemies
            else
            {
                UnlockAllDoors();
            }
        }
    }
}
