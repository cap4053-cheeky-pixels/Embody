using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Posession : MonoBehaviour
{
    private Player player;
    
    void Start()
    {
        player = gameObject.GetComponent<Player>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "DeadDude" && Input.GetKeyDown("space"))
        {
            Enemy deadEnemy = other.gameObject.GetComponent<Enemy>();
            player.MaxHealth = deadEnemy.MaxHealth;
            player.Health = deadEnemy.MaxHealth;
            player.ChangeMaxHealth(0);
            player.ChangeHealth(0);
            player.GetComponent<MeshFilter>().mesh = other.gameObject.GetComponent<MeshFilter>().sharedMesh;
            Destroy(other.gameObject);
        }
    }
}
