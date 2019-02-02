using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartSystem : MonoBehaviour
{
    private Player player;
    private int maxHeartAmount;
    private int healthPerHeart;
    private int numInitialHearts;

    public Image[] heartImages;
    public Sprite[] heartSprites;


    private void Start()
    {
        player = gameObject.GetComponent<Player>();

        healthPerHeart = heartSprites.Length - 1;
        maxHeartAmount = heartImages.Length;
        numInitialHearts = player.Health / healthPerHeart;

        SetVisibleHeartContainers();
        UpdateHearts();
    }

    void SetVisibleHeartContainers()
    {
        // Loop through all hearts
        for(int i = 0; i < maxHeartAmount; i++)
        {
            // Within the current heart capacity
            if(i < numInitialHearts)
            {
                heartImages[i].enabled = true;
            }
            // Disable unused heart slots
            else
            {
                heartImages[i].enabled = false;
            }
        }
    }

    // TODO make this better
    void UpdateHearts()
    {
        bool empty = false;
        int i = 0;

        foreach (Image image in heartImages)
        {
            if(empty)
            {
                image.sprite = heartSprites[0];
            }
            else
            {
                i++;

                if(player.Health >= i * healthPerHeart)
                {
                    image.sprite = heartSprites[heartSprites.Length - 1];
                }
                else
                {
                    int currentHeartHealth = (int)(healthPerHeart - (healthPerHeart * i - player.Health));
                    int spriteIndex = currentHeartHealth / healthPerHeart;
                    image.sprite = heartSprites[spriteIndex];
                    empty = true;
                }
            }
        }
    }
}
