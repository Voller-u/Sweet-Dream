using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    //collectable_items: �ռ������飬���ڴ��Ԥ�Ƽ�
    public Item[] items;

    //dictionary_items_dict: �ֵ䣬ͨ�����������ӦԤ�Ƽ�
    private Dictionary<string, Item> name_to_item_dict = new Dictionary<string, Item>();

    private void Awake()
    {
        foreach (Item item in items)
        {
            AddItem(item);
        }
    }

    private void AddItem(Item item)
    {
        if (!name_to_item_dict.ContainsKey(item.data.item_name))
        {
            name_to_item_dict.Add(item.data.item_name, item);
        }
    }

    public Item GetItemByName(string key)
    {
        if (name_to_item_dict.ContainsKey(key))
        {
            return name_to_item_dict[key];
        }
        return null;
    }
}
