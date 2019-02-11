using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    Player player;
    bool paused;

    private float initTime;
    private float interval;

    void Start()
    {
        paused = false;
        player = GameObject.FindWithTag("Player").GetComponent<Player>();

        interval = 0.5f;
        initTime = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Pause") != 0)
        {
            if((Time.realtimeSinceStartup - initTime) > interval)
            {
                initTime = Time.realtimeSinceStartup;
                if (paused)
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
}
