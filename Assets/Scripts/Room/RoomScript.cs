using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    // To be assigned via the Unity editor
    public List<GameObject> doors;

    private SpawnScript spawner;
    private List<GameObject> enemies;
    private int numEnemies = 0;

    // TODO somehow keep track of current list of enemies, not just original... how do we know when an enemy dies?

    private void Awake()
    {
        spawner = gameObject.transform.Find("SpawnPoints").gameObject.GetComponent<SpawnScript>();
        enemies = spawner.SpawnEnemies();
        numEnemies = enemies.Count;
        Debug.Log("Number of enemies spawned = " + numEnemies);
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
                foreach (GameObject door in doors)
                {
                    DoorController doorController = door.GetComponent<DoorController>();
                    doorController.Close();
                }
            }
            // No more enemies
            else
            {
                // Unlock all doors behind the player if there are no more enemies
                foreach (GameObject door in doors)
                {
                    DoorController doorController = door.GetComponent<DoorController>();
                    doorController.Open();
                }
            }
        }
    }
}
