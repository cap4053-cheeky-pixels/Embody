using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Posession : MonoBehaviour
{
    private Player player;
    private bool possessInput = false;
    private float cooldown = 2;
    private double timer = 2;

    void Start()
    {
        player = gameObject.GetComponent<Player>();
    }

    private void Update()
    {
        // Rinky dink shit to keep the trigger method from being called twice...
        timer += Time.deltaTime;
        if (Input.GetKeyDown("space") && timer > cooldown)
        {
            possessInput = true;
            timer = 0;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "DeadDude" && possessInput)
        {
            other.gameObject.transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material.SetFloat("_Outline", 0);
            possessInput = false;
            Enemy deadEnemy = other.gameObject.GetComponent<Enemy>();
            player.MaxHealth = deadEnemy.MaxHealth;
            player.Health = deadEnemy.MaxHealth;
            player.ChangeMaxHealth(0);
            player.ChangeHealth(0);

            // Possess the enemy model
            GameObject playerModel = transform.Find("Model").gameObject;
            GameObject enemyModel = deadEnemy.transform.Find("Model").gameObject;
            // Clone the enemy model within the player
            GameObject newModel = GameObject.Instantiate(enemyModel, transform);
            newModel.name = "Model";

            // Possess the enemy weapon
            GameObject playerWeapon = player.weapon;
            GameObject enemyWeapon = deadEnemy.weapon;
            // Clone the enemy weapon within the player
            GameObject newWeapon = GameObject.Instantiate(enemyWeapon, transform);
            player.SetWeapon(newWeapon);

            // Cleanup
            Destroy(playerModel);
            Destroy(playerWeapon);
            // Remove the enemy
            Destroy(other.gameObject);

        }
    }
}
