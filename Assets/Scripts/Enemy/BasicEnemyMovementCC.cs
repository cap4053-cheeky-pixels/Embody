using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyMovementCC : MonoBehaviour
{
    public float speed = 1;
    public float rotationDamping = 0.8f;
    private GameObject player;
    private CharacterController cc;
    void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = player.transform.position - transform.position;

        // No idea what the following does, but it rotates towards the target so...
        target.y = 0;
        Quaternion rotation = Quaternion.LookRotation(target);
        transform.rotation =
            Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);

        cc.SimpleMove(target * speed);
    }
}
