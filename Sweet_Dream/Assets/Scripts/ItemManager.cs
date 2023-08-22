using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    //collectable_items: 收集物数组，用于存放预制件
    public Collectable[] collectable_items;

    //dictionary_items_dict: 字典，通过种类查找相应预制件
    private Dictionary<CollectableType, Collectable> collectable_items_dict = new Dictionary<CollectableType, Collectable>();

    private void Awake()
    {
        foreach(Collectable item in collectable_items)
        {
            AddItem(item);
        }
    }

    private void AddItem(Collectable item)
    {
        if (!collectable_items_dict.ContainsKey(item.type))
        {
            collectable_items_dict.Add(item.type,item);
        }
    }

    public Collectable GetItemByType(CollectableType type)
    {
        if (collectable_items_dict.ContainsKey(type))
        {
            return collectable_items_dict[type];
        }
        return null;
    }
}
