using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject enemyPrefab = null;

    // To be used for customizing each spawned enemy
    public int maxHealth = 2;
    public int speed = 1;
    public float fireRate = 0.5f;
    public float yRotation = 0;

    private void Awake()
    {
        if(enemyPrefab != null)
        {
            Enemy script = enemyPrefab.GetComponent<Enemy>();
            script.MaxHealth = maxHealth;
            script.Health = maxHealth;
            script.Speed = speed;
            script.attemptFirerate = fireRate;
        }
    }
}
