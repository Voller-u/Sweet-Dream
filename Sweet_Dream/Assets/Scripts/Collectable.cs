using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Collectable : MonoBehaviour
{
    public CollectableType type;

    public Sprite icon;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player =  collision.GetComponent<Player>();

        if (player)
        {
            player.inventory.Add(this);
            Destroy(this.gameObject);
        }
    }
}


public enum CollectableType
{
    NONE, CORN_SEED,
    TOMATO,TOMATO_SEED,
    SWEET_YELLOW_PEPPER,SWEET_YELLOW_PEPPER_SEED,
    POTATO,POTATO_SEED,
    WHITE_CARROT,WHITE_CARROT_SEED,
    CARROT,CARROT_SEED
}