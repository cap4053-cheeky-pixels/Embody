using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed;
    private Rigidbody body;

    void Start()
    {
        speed = gameObject.GetComponent<Player>().Speed;
        body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(Horizontal, 0.0f, Vertical);

        // set velocity of player
        body.velocity = move * speed;
    }
}
