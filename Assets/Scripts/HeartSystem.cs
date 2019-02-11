using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartSystem : MonoBehaviour
{
    private Player player;
    private int maxAttainableHearts;
    private int healthPerHeart;
    private int maxPlayerHeartContainers;

    public Image[] heartImages;
    public Sprite[] heartSprites;


    private void Start()
    {
        healthPerHeart = heartSprites.Length - 1;
        maxAttainableHearts = heartImages.Length;

        AssociateWith(GameObject.FindWithTag("Player").GetComponent<Player>());
    }

    void AssociateWith(Player player)
    {
        this.player = player;
        player.healthChangedEvent += OnPlayerHealthChanged;
        OnPlayerHealthChanged();
    }

    void SetVisibleHeartContainers()
    {
        // Loop through all hearts
        for (int i = 0; i < maxAttainableHearts; i++)
        {
            // Enable hearts within the current capacity
            if(i < maxPlayerHeartContainers)
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
        maxPlayerHeartContainers = (player.MaxHealth + 1) / healthPerHeart;
        SetVisibleHeartContainers();
        UpdateHearts();
    }

    void UpdateHearts()
    {
        int indexOfLastNonemptyContainer = (int)Mathf.Max(0, (int)((player.Health + 1) / healthPerHeart - 1));
        bool evenHealth = player.Health % 2 == 0 && player.Health != 0;

        // Full red hearts
        for (int i = 0; i < indexOfLastNonemptyContainer; i++)
        {
            heartImages[i].sprite = heartSprites[heartSprites.Length - 1];
        }

        // Last heart full
        if (evenHealth)
        {
            heartImages[indexOfLastNonemptyContainer].sprite = heartSprites[heartSprites.Length - 1];
        }
        // Last heart empty (edge case of death)
        else if(player.Health == 0)
        {
            heartImages[0].sprite = heartSprites[0];
        }
        // Last heart half
        else
        {
            heartImages[indexOfLastNonemptyContainer].sprite = heartSprites[1];
        }

        // All others beyond that are empty
        for (int i = indexOfLastNonemptyContainer + 1; i < maxPlayerHeartContainers; i++)
        {
            heartImages[i].sprite = heartSprites[0];
        }
    }
}
