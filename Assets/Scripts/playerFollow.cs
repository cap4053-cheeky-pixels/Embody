using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerFollow : MonoBehaviour
{

    public Transform player;
    public Vector3 cameraOffset;
    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + cameraOffset;
    }
}
