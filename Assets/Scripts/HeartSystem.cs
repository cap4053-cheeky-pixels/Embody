using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartSystem : MonoBehaviour
{
    private Player player;
    private int maxAttainableHearts;
    private int healthPerHeart;
    private int playerMaxHearts;

    public Image[] heartImages;
    public Sprite[] heartSprites;


    private void Start()
    {
        player = gameObject.GetComponent<Player>();
        Player.healthChangedEvent += OnPlayerHealthChanged;

        healthPerHeart = heartSprites.Length - 1;
        maxAttainableHearts = heartImages.Length;

        OnPlayerHealthChanged();
    }

    void SetVisibleHeartContainers()
    {
        // Loop through all hearts
        for(int i = 0; i < maxAttainableHearts; i++)
        {
            // Enable hearts within the current capacity
            if(i < playerMaxHearts)
            {
                heartImages[i].enabled = true;
            }
            // Disable unused heart images
            else
            {
                heartImages[i].enabled = false;
            }
        }
    }
    
    void OnPlayerHealthChanged()
    {
        playerMaxHearts = (player.Health + 1) / healthPerHeart;
        SetVisibleHeartContainers();
        UpdateHearts();
    }

    void UpdateHearts()
    {
        int playerHealth = player.Health;
        bool evenHealth = playerHealth % 2 == 0 && playerHealth != 0;

        // e.g., if Player has 7 health (3.5 hearts), then last index = 3, so loop through [0, 3)
        int lastHeartIndex = (int)Mathf.Max(0, (int)(playerHealth / 2));

        // If health = 7, then loop through 7/2 - 1 = 2 first hearts, then decide the last one
        for(int i = 0; i < lastHeartIndex; i++)
        {
            // Set all hearts up to the last one as full
            heartImages[i].sprite = heartSprites[heartSprites.Length - 1];
        }

        // Then decide what the last heart should be
        if(evenHealth)
        {
            heartImages[lastHeartIndex].sprite = heartSprites[heartSprites.Length - 1];
        }
        else
        {
            heartImages[lastHeartIndex].sprite = heartSprites[1];
        }
    }
}
