using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string itemName;

    public Sprite itemImage;

    [TextArea]
    public string itemDescription;

    public int itemNum = 1;

    public Item(Item item){
        this.itemDescription = item.itemDescription;
        this.itemName = item.itemName;
        this.itemImage = item.itemImage;
        this.itemNum = item.itemNum;
    }

    public Item(){
        itemName = "";
        itemImage  = null;
        itemDescription = "";
        itemNum = 0;
    }
}
