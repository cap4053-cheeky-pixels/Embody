using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{

    public Vector3 center;
    public GameObject room;

    Collider[] colliders;
    private int enemies;
    private int radius;

    // Returns the number of enemy objects within a set radius

    public int EnemyCount(GameObject room)
    {

        center = room.transform.position;
        radius = 14;
        enemies = 0;


        colliders = Physics.OverlapSphere(center, radius);
        foreach (Collider enemy in colliders)
        {
            if (enemy.gameObject.tag == "Enemy")
            {
                enemies++;
            }
        }
        return enemies;
    }
}
