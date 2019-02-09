using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    Player player;
    bool paused;

    void Start()
    {
        paused = false;
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(paused)
            {
                Time.timeScale = 1;
                player.SetEnabled(true);
            }
            else
            {
                Time.timeScale = 0;
                player.SetEnabled(false);
            }
            
            paused = !paused;
        }
    }
}
