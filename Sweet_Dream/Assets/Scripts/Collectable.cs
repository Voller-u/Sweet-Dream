using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public CollectableType type;
    


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player_Controller player =  collision.GetComponent<Player_Controller>();

        if (player)
        {
            player.inventory.Add(type);
            Destroy(this.gameObject);
        }
    }
}


public enum CollectableType
{
    NONE, CORN, TOMATO, SWEET_YELLOW_PEPPER, POTATO, WHITE_CARROT, CARROT
}