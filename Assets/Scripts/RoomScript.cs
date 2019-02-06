using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    SpawnScript spawner;
    List<GameObject> enemies;
    int numEnemies;

    // TODO somehow keep track of current list of enemies, not just original... how do we know when an enemy dies?

    private void Start()
    {
        spawner = gameObject.transform.Find("SpawnPoints").gameObject.GetComponent<SpawnScript>();
        enemies = spawner.SpawnEnemies();
        numEnemies = enemies.Count;
    }
}
