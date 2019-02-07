using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Posession : MonoBehaviour
{
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.name == "DeadDude" && Input.GetKeyDown("space"))
        {
            player.ChangeMaxHealth(4);
            Destroy(GameObject.Find("DeadDude"));
        }
    }
}
