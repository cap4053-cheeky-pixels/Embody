using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    // To be assigned via the Unity editor
    public List<GameObject> doors;
    public GameObject currentRoom;

    private EnemyDetection detection;
    private SpawnScript spawner;
    private List<GameObject> enemies;
    private WaitForSeconds wait;
    private int numEnemies = 0;

    // TODO somehow keep track of current list of enemies, not just original... how do we know when an enemy dies?

    private void Awake()
    {
        spawner = gameObject.transform.Find("SpawnPoints").gameObject.GetComponent<SpawnScript>();
        enemies = spawner.SpawnEnemies();
        numEnemies = enemies.Count;
        Debug.Log("Number of enemies spawned = " + numEnemies);
    }

    void Start()
    {
        wait = new WaitForSeconds(2.0f);
        detection = currentRoom.GetComponent<EnemyDetection>();
        numEnemies = detection.EnemyCount(currentRoom);
    }



    /* Called when any other collision object enters this Room. Used to detect when the player
     * enters the room. If there are currently enemies, it will lock all doors.
     */
    private IEnumerator OnCollisionEnter(Collision collision)
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
                /* Updates numEnemies every 'wait' seconds */
                while (numEnemies != 0)
                {
                    numEnemies = detection.EnemyCount(currentRoom);
                    yield return wait;
                }
                foreach (GameObject door in doors)
                {
                    DoorController doorController = door.GetComponent<DoorController>();
                    doorController.Open();
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
