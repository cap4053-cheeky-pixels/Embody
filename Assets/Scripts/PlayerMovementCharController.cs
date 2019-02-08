using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementCharController : MonoBehaviour
{

    public float speed;
    private CharacterController cc;

    void Start()
    {
      cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(Horizontal, 0.0f, Vertical);

        // Rotate the player
        if(Horizontal != 0 || Vertical != 0)
            transform.rotation = Quaternion.LookRotation(move);

        // Move the player
        cc.SimpleMove(move * speed);
    }
}
