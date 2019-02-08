using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    // To be assigned via the Unity editor
    public List<GameObject> doors;

    private HashSet<GameObject> spawnedEnemies;
    private EnemyDetection detection;
    private SpawnScript spawner;
    private WaitForSeconds wait;
    private int numEnemies = 0;

    /* Sets up the room by spawning enemies and subscribing to all their death events.
     */ 
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
        enemy.GetComponent<Enemy>().SetMovement(false);
        spawnedEnemies.Remove(enemy);
        numEnemies--;

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

    /* Loops through all live enemies in the room and dispatches them to go follow
     * the player.
     */ 
    void DispatchEnemiesToFollowPlayer()
    {
        foreach (GameObject enemyObject in spawnedEnemies)
        {
            Enemy enemy = enemyObject.gameObject.GetComponent<Enemy>();

            if (enemy != null)
            {
                Debug.Log("Enabling an enemy's movement!");
                // The enemy's update loop condition will then be true
                enemy.SetMovement(true);
            }
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
                LockAllDoors();
                DispatchEnemiesToFollowPlayer();
            }
            // No more enemies
            else
            {
                UnlockAllDoors();
            }
        }
    }
}
