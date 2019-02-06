using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    /* Loops through all children SpawnPoint objects and spawns enemies at those points.
     * Returns a list of all spawned enemies.
     */ 
    public List<GameObject> SpawnEnemies()
    {
        List<GameObject> enemiesSpawned = new List<GameObject>();

        // Loop through each spawn point
        foreach(Transform child in transform)
        {
            SpawnPoint spawnPoint = child.gameObject.GetComponent<SpawnPoint>();
            GameObject enemyToSpawn = spawnPoint.enemyPrefab;

            // If an enemy is set to spawn at that point, go ahead and instantiate it
            if (enemyToSpawn != null)
            {
                enemyToSpawn.tag = "Enemy";
                Instantiate(enemyToSpawn, spawnPoint.transform.position, Quaternion.identity);
                enemiesSpawned.Add(enemyToSpawn);
            }
        }

        return enemiesSpawned;
    }
}
